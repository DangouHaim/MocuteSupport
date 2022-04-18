using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MocuteSupport.Droid
{
	public class ButtonMapping
	{
		public static Keycode BUTTON_A = Keycode.ButtonA;
		public static Keycode BUTTON_B = Keycode.ButtonB;
		public static Keycode BUTTON_Y = Keycode.ButtonX;
		public static Keycode BUTTON_X = Keycode.ButtonC;
		public static Keycode BUTTON_L1 = Keycode.ButtonY;
		public static Keycode BUTTON_L2 = Keycode.ButtonThumbl;
		public static Keycode BUTTON_R1 = Keycode.ButtonZ;
		public static Keycode BUTTON_R2 = Keycode.ButtonThumbr;
		public static Keycode BUTTON_START = Keycode.ButtonR1;
		public static Keycode BUTTON_SELECT = Keycode.ButtonL1;
		public static Keycode BUTTON_THUMBL = Keycode.ButtonL2;
		public static Keycode BUTTON_THUMBR = Keycode.ButtonR2;
		public static Keycode DPAD_LEFT = Keycode.DpadLeft;
		public static Keycode DPAD_UP = Keycode.DpadUp;
		public static Keycode DPAD_RIGHT = Keycode.DpadRight;
		public static Keycode DPAD_DOWN = Keycode.DpadDown;

		public static Keycode BACK = Keycode.Back;
		public static Keycode POWER = Keycode.ButtonMode;
		public static int Size = 18;
		private static int key_code;

		public ButtonMapping(int keyCode)
		{
			key_code = keyCode;
		}

		public static string GetButtonName(Keycode key)
        {
			if (key == BUTTON_A) return "BUTTON_A";
			if (key == BUTTON_B) return "BUTTON_B";
			if (key == BUTTON_Y) return "BUTTON_Y";
			if (key == BUTTON_X) return "BUTTON_X";
			if (key == BUTTON_L1) return "BUTTON_L1";
			if (key == BUTTON_SELECT) return "BUTTON_SELECT";
			if (key == BUTTON_START) return "BUTTON_START";
			if (key == BUTTON_THUMBL) return "BUTTON_THUMBL";
			if (key == BUTTON_THUMBR) return "BUTTON_THUMBR";
			if (key == BUTTON_R1) return "BUTTON_R1";
			if (key == BUTTON_L2) return "BUTTON_L2";
			if (key == BUTTON_R2) return "BUTTON_R2";
			if (key == DPAD_LEFT) return "DPAD_LEFT";
			if (key == DPAD_UP) return "DPAD_UP";
			if (key == DPAD_RIGHT) return "DPAD_RIGHT";
			if (key == DPAD_DOWN) return "DPAD_DOWN";
			return "";
		}

		public static Keycode? GetButtonByName(string name)
		{
			if (name == "BUTTON_A") return BUTTON_A;
			if (name == "BUTTON_B") return BUTTON_B;
			if (name == "BUTTON_Y") return BUTTON_Y;
			if (name == "BUTTON_X") return BUTTON_X;
			if (name == "BUTTON_L1") return BUTTON_L1;
			if (name == "BUTTON_SELECT") return BUTTON_SELECT;
			if (name == "BUTTON_START") return BUTTON_START;
			if (name == "BUTTON_THUMBL") return BUTTON_THUMBL;
			if (name == "BUTTON_THUMBR") return BUTTON_THUMBR;
			if (name == "BUTTON_R1") return BUTTON_R1;
			if (name == "BUTTON_L2") return BUTTON_L2;
			if (name == "BUTTON_R2") return BUTTON_R2;
			if (name == "DPAD_LEFT") return DPAD_LEFT;
			if (name == "DPAD_UP") return DPAD_UP;
			if (name == "DPAD_RIGHT") return DPAD_RIGHT;
			if (name == "DPAD_DOWN") return DPAD_DOWN;
			return null;
		}

		public static int getKeyCode()
		{
			return key_code;
		}

		public static int OrdinalValue(Keycode key)
		{
			if (key == BUTTON_A)
			{
				return 0;
			}
			else if (key == BUTTON_B)
			{
				return 1;
			}
			else if (key == BUTTON_X)
			{
				return 2;
			}
			else if (key == BUTTON_Y)
			{
				return 3;
			}
			else if (key == BUTTON_L1)
			{
				return 4;
			}
			else if (key == BUTTON_R1)
			{
				return 5;
			}
			else if (key == BUTTON_L2)
			{
				return 6;
			}
			else if (key == BUTTON_R2)
			{
				return 7;
			}
			else if (key == BUTTON_SELECT)
			{
				return 8;
			}
			else if (key == BUTTON_START)
			{
				return 9;
			}
			else if (key == BUTTON_THUMBL)
			{
				return 10;
			}
			else if (key == BUTTON_THUMBR)
			{
				return 11;
			}
			else if (key == BACK)
			{
				return 12;
			}
			else if (key == POWER)
			{
				return 13;
			}
			else if (key == DPAD_LEFT)
			{
				return 14;
			}
			else if (key == DPAD_UP)
			{
				return 15;
			}
			else if (key == DPAD_DOWN)
			{
				return 16;
			}
			else if (key == DPAD_RIGHT)
			{
				return 17;
			}
			else
			{
				return -1;
			}

		}

		public static Keycode OrdinalValueButton(int val)
		{
			switch (val)
			{
				case 0:
					return BUTTON_A;
				case 1:
					return BUTTON_B;
				case 2:
					return BUTTON_X;
				case 3:
					return BUTTON_Y;
				case 4:
					return BUTTON_L1;
				case 5:
					return BUTTON_R1;
				case 6:
					return BUTTON_L2;
				case 7:
					return BUTTON_R2;
				case 8:
					return BUTTON_SELECT;
				case 9:
					return BUTTON_START;
				case 10:
					return BUTTON_THUMBL;
				case 11:
					return BUTTON_THUMBR;
				case 12:
					return BACK;
				case 13:
					return POWER;
				case 14:
					return DPAD_LEFT;
				case 15:
					return DPAD_UP;
				case 16:
					return DPAD_RIGHT;
				case 17:
					return DPAD_DOWN;
			}
			return new Keycode();
		}
	}
}