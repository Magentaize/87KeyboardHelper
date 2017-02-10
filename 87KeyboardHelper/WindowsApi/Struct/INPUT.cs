using System;
using System.Runtime.InteropServices;

namespace _87KeyboardHelper.WindowsApi
{
    [StructLayout(LayoutKind.Sequential)]
    public struct INPUT
    {
        public INPUT_TYPE type;
        public INPUT_U u;

        public static INPUT[] Creat(VKeys vKeys)
        {
            return new[]
            {
                new INPUT()
                {
                    type = INPUT_TYPE.KEYBOARD,
                    u = new INPUT_U
                    {
                        ki = new KEYBDINPUT
                        {
                            wVk = Convert.ToUInt16(vKeys),
                            wScan = 0,
                            dwFlags = 0,
                        }
                    }
                }
            };
        }

        public static INPUT[] CreatArrow(VKeys vKeys)
        {
            return new[]
            {
                new INPUT()
                {
                    type = INPUT_TYPE.KEYBOARD,
                    u = new INPUT_U
                    {
                        ki = new KEYBDINPUT
                        {
                            wVk = Convert.ToUInt16(VKeys.LMENU),
                            wScan = 0,
                            dwFlags = Convert.ToUInt32(KEYEVENTF.KEYUP),
                            dwExtraInfo = IntPtr.Zero
                        }
                    }
                },
                                new INPUT()
                {
                    type = INPUT_TYPE.KEYBOARD,
                    u = new INPUT_U
                    {
                        ki = new KEYBDINPUT
                        {
                            wVk = Convert.ToUInt16(VKeys.KEY_A),
                            wScan = 0,
                            dwFlags = Convert.ToUInt32(KEYEVENTF.KEYUP),
                            dwExtraInfo = IntPtr.Zero
                        }
                    }
                },
                new INPUT()
                {
                    type = INPUT_TYPE.KEYBOARD,
                    u = new INPUT_U
                    {
                        ki = new KEYBDINPUT
                        {
                            wVk = Convert.ToUInt16(vKeys),
                            wScan = 0,
                            dwFlags = 0|Convert.ToUInt32(KEYEVENTF.EXTENDEDKEY),
                            dwExtraInfo = IntPtr.Zero
                        }
                    }
                },
                new INPUT()
                {
                    type = INPUT_TYPE.KEYBOARD,
                    u = new INPUT_U
                    {
                        ki = new KEYBDINPUT
                        {
                            wVk = Convert.ToUInt16(vKeys),
                            wScan = 0,
                            dwFlags = 0|Convert.ToUInt32(KEYEVENTF.KEYUP),
                            dwExtraInfo = IntPtr.Zero
                        }
                    }
                },
                //new INPUT()
                //{
                //    type = INPUT_TYPE.KEYBOARD,
                //    u = new INPUT_U
                //    {
                //        ki = new KEYBDINPUT
                //        {
                //            wVk = Convert.ToUInt16(VKeys.LMENU),
                //            wScan = 0,
                //            dwFlags = 0,
                //            dwExtraInfo = IntPtr.Zero
                //        }
                //    }
                //},
            };
        }

        public static INPUT VirtualKeyDown(VKeys vKeys)
        {
            var input = new INPUT { type = INPUT_TYPE.KEYBOARD};
            input.u.ki = new KEYBDINPUT { wVk = Convert.ToUInt16(vKeys) };

            return input;
        }

        public static INPUT VirtualKeyUp(VKeys vKeys)
        {
            var input = new INPUT { type = INPUT_TYPE.KEYBOARD };
            input.u.ki = new KEYBDINPUT
            {
                wVk = Convert.ToUInt16(vKeys),
                dwFlags = Convert.ToUInt32(KEYEVENTF.KEYUP)
            };

            return input;
        }
    }

    [StructLayout(LayoutKind.Explicit)]
    public struct INPUT_U
    {
        [FieldOffset(0)]
        public KEYBDINPUT ki;
        [FieldOffset(0)]
        public MOUSEINPUT mi;
        [FieldOffset(0)]
        public HARDWAREINPUT hi;
    }
}