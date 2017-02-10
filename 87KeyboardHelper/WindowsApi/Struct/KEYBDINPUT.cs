using System;
using System.Runtime.InteropServices;

namespace _87KeyboardHelper.WindowsApi
{
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
}