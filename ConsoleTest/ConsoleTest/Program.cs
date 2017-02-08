using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using GlobalLowLevelHooks;

namespace ConsoleTest
{
    public class Program
    {
        public static readonly KeyboardHook.INPUT[] VolumeUpData = KeyboardHook.INPUT.Creat(KeyboardHook.VKeys.VOLUME_UP);

        public static readonly KeyboardHook.INPUT[] VolumeDownData = KeyboardHook.INPUT.Creat(KeyboardHook.VKeys.VOLUME_DOWN);

        private static bool LMenuHasPressedDown = false;

        public static void Main()
        {
            var mouseHook = new MouseHook();
            mouseHook.MouseWheel += (vKeys => Console.WriteLine("Wheeling..."));
            mouseHook.Install();
            var keyboardHook = new KeyboardHook();
            keyboardHook.KeyDown += keyboardHook_KeyDown;
            keyboardHook.Install();
            Application.Run();
        }

        private static void keyboardHook_KeyDown(KeyboardHook.VKeys vKeys)
        {
            Console.WriteLine(vKeys.ToString());
            if (vKeys == KeyboardHook.VKeys.LMENU)
            {
                LMenuHasPressedDown = !LMenuHasPressedDown;
            }
            else
            {
                if(LMenuHasPressedDown)
                switch (vKeys)
                {
                    case KeyboardHook.VKeys.OEM_1:
                        SendInput(VolumeDownData);
                        break;
                    case KeyboardHook.VKeys.OEM_7:
                        SendInput(VolumeUpData);
                        break;
                }
                LMenuHasPressedDown = false;
            }
        }

        public static uint SendInput(KeyboardHook.INPUT[] data)
        {
            return KeyboardHook.SendInput((uint)data.Length, data, Marshal.SizeOf(typeof(KeyboardHook.INPUT)));
        }
    }
}
