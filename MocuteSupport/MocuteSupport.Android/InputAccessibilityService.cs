using Android;
using Android.AccessibilityServices;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Views.Accessibility;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MocuteSupport.Droid
{
    [Service(Label = "MocuteSupport", Permission = Manifest.Permission.BindAccessibilityService)]
    [IntentFilter(new[] { "android.accessibilityservice.AccessibilityService" })]
    [MetaData("android.accessibilityservice", Resource = "@xml/config")]
    public class InputAccessibilityService : AccessibilityService
    {
        private static AccessibilityService _service;

        public InputAccessibilityService()
        {
            _service = this;
        }

        public static void DispatchGesture(GestureDescription gestureDescription)
        {
            try
            {
                _service.DispatchGesture(gestureDescription, null, null);
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message + "\n" + ex.StackTrace);
            }
        }

        protected override void OnServiceConnected()
        {
            Logger.Log("OnServiceConnected");
            _service = this;
            base.OnServiceConnected();
        }

        public override void OnAccessibilityEvent(AccessibilityEvent e)
        {

        }

        public override void OnInterrupt()
        {

        }
    }
}