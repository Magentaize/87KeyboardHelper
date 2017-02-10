using System;
using System.Runtime.InteropServices;

namespace _87KeyboardHelper.WindowsApi
{
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
}