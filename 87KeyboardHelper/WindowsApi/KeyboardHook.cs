using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace _87KeyboardHelper.WindowsApi
{
    /// <summary>
    /// Class for intercepting low level keyboard hooks
    /// </summary>
    public class KeyboardHook : Hook
    {
        /// <summary>
        /// Function that will be called when defined events occur
        /// </summary>
        /// <param name="key">VKeys</param>
        public delegate void KeyboardHookCallback(VKeys key);

        #region Events
        public event KeyboardHookCallback KeyDown;
        public event KeyboardHookCallback KeyUp;
        #endregion

        /// <summary>
        /// Registers hook with Windows API
        /// </summary>
        /// <param name="proc">Callback function</param>
        /// <returns>Hook ID</returns>
        protected override IntPtr SetHook(HOOKPROC proc)
        {
            using (var module = Process.GetCurrentProcess().MainModule)
                return SetWindowsHookEx(HOOKTYPE.WH_KEYBOARD_LL, proc, GetModuleHandle(module.ModuleName), 0);
        }

        /// <summary>
        /// Default hook call, which analyses pressed keys
        /// </summary>
        protected override IntPtr HookFunc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                //var replacementKey = (KBDLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(KBDLLHOOKSTRUCT));
                //replacementKey.vkCode = Convert.ToUInt32(VKeys.VOLUME_UP);
                //Marshal.StructureToPtr(replacementKey, lParam, true);

                var iwParam = (WM_MESSAGE) wParam.ToInt32();
                if (iwParam == WM_MESSAGE.WM_KEYDOWN || iwParam == WM_MESSAGE.WM_SYSKEYDOWN)
                    KeyDown?.Invoke((VKeys) Marshal.ReadInt32(lParam));
                if (iwParam == WM_MESSAGE.WM_KEYUP || iwParam == WM_MESSAGE.WM_SYSKEYUP)
                    KeyUp?.Invoke((VKeys) Marshal.ReadInt32(lParam));
            }
            return CallNextHookEx(hookID, nCode, wParam, lParam);
        }
    }
}