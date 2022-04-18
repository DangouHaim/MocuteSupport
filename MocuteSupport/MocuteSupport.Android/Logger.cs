using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MocuteSupport.Droid
{
    public static class Logger
    {
        public static void Log(object message)
        {
            //File.AppendAllText(@"/mnt/sdcard/MSL.txt", message.ToString() + "\n\n");
        }
    }
}