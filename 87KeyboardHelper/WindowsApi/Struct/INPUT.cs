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
                            wVk = Convert.ToUInt16(vKeys),
                            wScan = 0,
                            dwFlags = Convert.ToUInt32(0),
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
                            dwFlags = Convert.ToUInt32(KEYEVENTF.EXTENDEDKEY | KEYEVENTF.KEYUP),
                            dwExtraInfo = IntPtr.Zero
                        }
                    }
                }
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

    [StructLayout(LayoutKind.Sequential)]
    public struct MOUSEINPUT
    {
        public int dx;
        public int dy;
        public uint mouseData;
        /// <summary>
        /// Defined by the combinations of <see cref="KEYEVENTF"/>
        /// </summary>
        public uint dwFlags;
        public uint time;
        public IntPtr dwExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct KEYBDINPUT
    {
        public ushort wVk;
        public ushort wScan;
        /// <summary>
        /// Defined by the combinations of <see cref="KEYEVENTF"/>
        /// </summary>
        public uint dwFlags;
        public uint time;
        public IntPtr dwExtraInfo;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct HARDWAREINPUT
    {
        public uint uMsg;
        public ushort wParamL;
        public ushort wParamH;
    }
}