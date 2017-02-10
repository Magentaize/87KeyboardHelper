using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using _87KeyboardHelper.WindowsApi;

namespace _87KeyboardHelper
{
    class Program
    {
        private static INPUT[] VolumeUpData = INPUT.Creat(VKeys.VOLUME_UP);
        private static INPUT[] VolumeDownData = INPUT.Creat(VKeys.VOLUME_DOWN);
        private static INPUT[] MediaPrevTrackData = INPUT.Creat(VKeys.MEDIA_PREV_TRACK);
        private static INPUT[] MediaNextTrackData = INPUT.Creat(VKeys.MEDIA_NEXT_TRACK);
        private static INPUT[] MediaPlayPauseData = INPUT.Creat(VKeys.MEDIA_PLAY_PAUSE);
        private static INPUT[] UpData = INPUT.CreatArrow(VKeys.UP);
        private static INPUT[] DownData = INPUT.CreatArrow(VKeys.DOWN);
        private static INPUT[] LeftData = INPUT.CreatArrow(VKeys.LEFT);
        private static INPUT[] RightData = INPUT.CreatArrow(VKeys.RIGHT);

        static void Main(string[] args)
        {
            var keyHook = new KeyboardHook();
            keyHook.KeyDown += keyHook_KeyDown;
            keyHook.KeyUp += keyHook_KeyUp;
            keyHook.Install();
            
            AppDomain.CurrentDomain.ProcessExit += (_, __) => keyHook.Uninstall();
            Application.Run();
        }

        private static uint SendInput(INPUT[] data)
        {
            return Hook.SendInput((uint) data.Length, data, Marshal.SizeOf(typeof(INPUT)));
        }

        private static bool LMenuKeyDown = false;

        static void keyHook_KeyDown(VKeys vKeys)
        {
#if DEBUG
            Console.WriteLine("Down: " + vKeys);
#endif
            if (vKeys == VKeys.LMENU)
                LMenuKeyDown = true;
            else if (LMenuKeyDown)
            {
                switch (vKeys)
                {
                    case VKeys.OEM_1:
                        SendInput(VolumeDownData);
                        break;
                    case VKeys.OEM_7:
                        SendInput(VolumeUpData);
                        break;
                    case VKeys.KEY_1:
                        SendInput(MediaPrevTrackData);
                        break;
                    case VKeys.KEY_2:
                        SendInput(MediaNextTrackData);
                        break;
                    case VKeys.KEY_3:
                        SendInput(MediaPlayPauseData);
                        break;
                    case VKeys.KEY_W:
                        SendInput(UpData);
                        break;
                    case VKeys.KEY_S:
                        SendInput(DownData);
                        break;
                    case VKeys.KEY_A:
                        SendInput(LeftData);
                        break;
                    case VKeys.KEY_D:
                        SendInput(RightData);
                        break;
                }
            }
        }

        private static bool DefinedKeyUp = false;
        static void keyHook_KeyUp(VKeys vKeys)
        {
#if DEBUG
            Console.WriteLine("Up: " + vKeys);
#endif
            if (LMenuKeyDown)
            {
                switch (vKeys)
                {
                    case VKeys.OEM_1:
                    case VKeys.OEM_7:
                    case VKeys.KEY_1:
                    case VKeys.KEY_2:
                    case VKeys.KEY_3:
                    case VKeys.KEY_W:
                    case VKeys.KEY_S:
                    case VKeys.KEY_A:
                    case VKeys.KEY_D:
                        break;
                    default:
                        LMenuKeyDown = false;
                        break;
                }
            }
        }
    }
}
