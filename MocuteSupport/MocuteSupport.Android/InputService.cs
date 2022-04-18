using Android;
using Android.AccessibilityServices;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Path = Android.Graphics.Path;
using InputMethodService = Android.InputMethodServices.InputMethodService;

namespace MocuteSupport.Droid
{
    [Service(Label = "MocuteSupport", Permission = Manifest.Permission.BindInputMethod)]
    [IntentFilter(new[] { "android.view.InputMethod" })]
    [MetaData("android.view.im", Resource = "@xml/input")]
    class InputService : InputMethodService
    {
        private const string AllowedInputSettings = "* Dllowed buttons for configuration (eg. BUTTON_A 100 100): " +
            "\nBUTTON_A 1 1" +
            "\nBUTTON_B 1 1" +
            "\nBUTTON_Y 1 1" +
            "\nBUTTON_X 1 1" +
            "\nBUTTON_L1 1 1" +
            "\nBUTTON_L2 1 1" +
            "\nBUTTON_R1 1 1" +
            "\nBUTTON_R2 1 1" +
            "\nBUTTON_START 1 1" +
            "\nBUTTON_SELECT 1 1" +
            "\nBUTTON_THUMBL 1 1" +
            "\nBUTTON_THUMBR 1 1" +
            "\nDPAD_LEFT 1 1" +
            "\nDPAD_UP 1 1" +
            "\nDPAD_RIGHT 1 1" +
            "\nDPAD_DOWN 1 1" +
            "\nLSTICK 1 1" +
            "\nRSTICK 1 1";

        private const Keycode L2ButtonKeyCode = Keycode.ButtonThumbl;
        private const Keycode R2ButtonKeyCode = Keycode.ButtonThumbr;

        private static float[] L2Button = new float[12]
        {
            0, 0,
            1, -1,
            0, 0,
            0, 0,
            0, 0,
            0, 0,
        };

        private static float[] R2Button = new float[12]
        {
            0, 0,
            -1, 1,
            0, 0,
            0, 0,
            0, 0,
            0, 0,
        };

        private string ConfigFile;
        private bool L2ButtonPresssed = false;
        private bool R2ButtonPresssed = false;
        private int[] buttons = new int[ButtonMapping.Size];
        private float[] axis = new float[AxisMapping.Size];

        private float? ldx;
        private float? ldy;
        private float? rdx;
        private float? rdy;

        private Dictionary<Keycode, Point> KeyPosition = new Dictionary<Keycode, Point>();
        private Point LStickPosition;
        private Point RStickPosition;
        private bool LStickDeltaUsing = false;
        private bool RStickDeltaUsing = false;

        public override void OnCreate()
        {
            Logger.Log("InputService");
            try
            {
                var config = LoadConfig();
                InitControls(config);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message + "\n" + ex.StackTrace);
            }
            base.OnCreate();
        }

        private string LoadConfig()
        {
            ConfigFile = Application.Context.GetExternalFilesDir("").AbsolutePath + @"/MSConfig.txt";

            if (!File.Exists(ConfigFile))
            {
                File.WriteAllText(ConfigFile, AllowedInputSettings);
            }
            else
            {
                return File.ReadAllText(ConfigFile);
            }
            return "";
        }

        private void InitControls(string config)
        {
            if (string.IsNullOrEmpty(config))
            {
                return;
            }

            using (StringReader sr = new StringReader(config))
            {
                string s = sr.ReadLine();
                while (!string.IsNullOrEmpty(s))
                {
                    s = sr.ReadLine();
                    if (string.IsNullOrEmpty(s) || s.StartsWith("*"))
                    {
                        continue;
                    }

                    var configData = s.Split(' ');
                    var name = configData[0];
                    var x = int.Parse(configData[1]);
                    var y = int.Parse(configData[2]);

                    if (name == "LSTICK")
                    {
                        LStickPosition = new Point(x, y);
                        continue;
                    }
                    if (name == "RSTICK")
                    {
                        RStickPosition = new Point(x, y);
                        continue;
                    }

                    if (ButtonMapping.GetButtonByName(name) != null)
                    {
                        KeyPosition[ButtonMapping.GetButtonByName(name).Value] = new Point(x, y);
                    }
                }
            }
        }

        public override bool OnGenericMotionEvent(MotionEvent e)
        {
            InputDevice device = e.Device;
            if (IsGamepad(device))
            {
                for (int i = 0; i < AxisMapping.Size; i++)
                {
                    axis[i] = GetCenteredAxis(e, device, AxisMapping.OrdinalValueAxis(i));
                }
                Console.WriteLine(JsonConvert.SerializeObject(axis));
                if(HandleAxis())
                {
                    return true;
                }
            }
            return base.OnGenericMotionEvent(e);
        }

        public override bool OnKeyDown(Keycode keyCode, KeyEvent e)
        {
            Logger.Log((int)keyCode);
            InputDevice device = e?.Device;
            if (e == null || IsGamepad(device))
            {
                int index = ButtonMapping.OrdinalValue(keyCode);
                if (index >= 0)
                {
                    buttons[index] = 1;
                    HandleButtons();
                }
                return true;
            }
            return base.OnKeyDown(keyCode, e);
        }

        public override bool OnKeyUp(Keycode keyCode, KeyEvent e)
        {
            Logger.Log("UP " + (int)keyCode);
            int index = ButtonMapping.OrdinalValue(keyCode);
            if (index >= 0)
            {
                buttons[index] = 0;
            }
            return true;
        }

        private bool HandleAxis()
        {
            var builder = new GestureDescription.Builder();

            if (LStickPosition != null)
            {
                builder = HandleLeftStick(LStickPosition.X, LStickPosition.Y, builder, LStickDeltaUsing);
            }
            if(RStickPosition != null)
            {
                builder = HandleRightStick(RStickPosition.X, RStickPosition.Y, builder, RStickDeltaUsing);
            }

            DispatchGesture(builder.Build());

            return HandleAxisButtons() || AnyStickUsed();
        }

        private bool AnyStickUsed()
        {
            return axis[0] != 0 || axis[1] != 0 || axis[10] != 0 || axis[11] != 0;
        }

        private void HandleButtons()
        {
            var builder = new GestureDescription.Builder();

            for (int i = 0; i < buttons.Length; i++)
            {
                if (buttons[i] == 1)
                {
                    if (KeyPosition.ContainsKey(ButtonMapping.OrdinalValueButton(i)))
                    {
                        var position = KeyPosition[ButtonMapping.OrdinalValueButton(i)];
                        builder = CreateTouch(position.X, position.Y, builder);
                    }
                }
            }

            DispatchGesture(builder.Build());
        }

        private GestureDescription.Builder CreateTouch(float x, float y, GestureDescription.Builder builder)
        {
            builder.AddStroke(CreateClick(x, y));
            return builder;
        }

        //Get the centered position for the joystick axis
        private float GetCenteredAxis(MotionEvent e, InputDevice device, Axis axis)
        {
            InputDevice.MotionRange range = device.GetMotionRange(axis, e.Source);
            if (range != null)
            {
                float flat = range.Flat;
                float value = e.GetAxisValue(axis);
                if (System.Math.Abs(value) > flat)
                    return value;
            }

            return 0;

        }

        private GestureDescription.Builder HandleLeftStick(float x, float y, GestureDescription.Builder builder, bool usingDelta = false)
        {
            if (!ldx.HasValue || !ldy.HasValue)
            {
                ldx = x;
                ldy = y;
            }

            builder.AddStroke(CreateSwipe(x, y, ldx.Value, ldy.Value, axis[0], axis[1]));

            if (usingDelta)
            {
                ldx = CalculateDelta(x, axis[0]);
                ldy = CalculateDelta(y, axis[1]);
            }
            return builder;
        }

        private GestureDescription.Builder HandleRightStick(float x, float y, GestureDescription.Builder builder, bool usingDelta = false)
        {
            if (!rdx.HasValue || !rdy.HasValue)
            {
                rdx = x;
                rdy = y;
            }

            builder.AddStroke(CreateSwipe(x, y, rdx.Value, rdy.Value, axis[10], axis[11]));

            if (usingDelta)
            {
                rdx = CalculateDelta(x, axis[10]);
                rdy = CalculateDelta(y, axis[11]);
            }

            return builder;
        }

        private bool HandleAxisButtons()
        {
            bool buttonsWasPressed = false;

            if (axis[2] == L2Button[2] && axis[3] == L2Button[3])
            {
                Logger.Log("L2Button");
                L2ButtonPresssed = true;
                buttonsWasPressed = true;
                OnKeyDown(L2ButtonKeyCode, null);
            }
            else
            {
                if (L2ButtonPresssed)
                {
                    L2ButtonPresssed = false;
                    OnKeyUp(L2ButtonKeyCode, null);
                }
            }
            if (axis[2] == R2Button[2] && axis[3] == R2Button[3])
            {
                Logger.Log("R2Button");
                R2ButtonPresssed = true;
                buttonsWasPressed = true;
                OnKeyDown(R2ButtonKeyCode, null);
            }
            else
            {
                if (R2ButtonPresssed)
                {
                    R2ButtonPresssed = false;
                    OnKeyUp(R2ButtonKeyCode, null);
                }
            }

            return buttonsWasPressed;
        }

        private bool IsGamepad(InputDevice device)
        {
            if ((device.Sources & InputSourceType.Gamepad) == InputSourceType.Gamepad ||
               (device.Sources & InputSourceType.ClassJoystick) == InputSourceType.Joystick)
            {
                return true;
            }
            return false;
        }

        private static float CalculateDelta(float x, float direction)
        {
            return x + direction * 100;
        }

        private static GestureDescription.StrokeDescription CreateClick(float x, float y)
        {
            Path clickPath = new Path();
            int duration = 1;

            clickPath.MoveTo(x, y);

            var clickStroke = new GestureDescription.StrokeDescription(clickPath, 0, duration, true);

            return clickStroke;
        }

        private static GestureDescription.StrokeDescription CreateSwipe(float x, float y, float lx, float ly, float moveToX, float moveToY)
        {
            var dragPath = new Path();
            var dragDuration = 1;
            var dx = CalculateDelta(x, moveToX);
            var dy = CalculateDelta(y, moveToY);

            dragPath.MoveTo(lx, ly);
            dragPath.LineTo(dx, dy);

            var drag = new GestureDescription.StrokeDescription(dragPath, 0L, dragDuration, true);

            return drag;
        }

        private void DispatchGesture(GestureDescription gestureDescription)
        {
            InputAccessibilityService.DispatchGesture(gestureDescription);
        }
    }
}