using System;
using System.Runtime.InteropServices;

namespace _87KeyboardHelper.WindowsApi
{
    public abstract class Hook
    {
        #region WinAPI

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        protected static extern IntPtr SetWindowsHookEx(HOOKTYPE idHook, HOOKPROC lpfn, IntPtr hMod, uint dwThreadId);

        /// <summary>
        /// Internal callback processing function
        /// </summary>
        public delegate IntPtr HOOKPROC(int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        protected static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        protected static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        protected static extern IntPtr GetModuleHandle(string lpModuleName);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

        #endregion

        #region Variables

        private HOOKPROC hookHandler;
        protected IntPtr hookID = IntPtr.Zero;

        #endregion

        #region Expection

        private class UnhookWindowsHookExExpection : Exception
        {
            public IntPtr HookID { get; set; }

            public UnhookWindowsHookExExpection(IntPtr id) : base("UnhookWindowsHookEx failed.")
            {
                HookID = id;
            }
        }

        #endregion

        /// <summary>
        /// Install hook
        /// </summary>
        public void Install()
        {
            hookHandler = HookFunc;
            hookID = SetHook(hookHandler);
        }

        /// <summary>
        /// Remove hook
        /// </summary>
        public void Uninstall()
        {
            if (hookID != IntPtr.Zero)
            {
                var isSucessful = UnhookWindowsHookEx(hookID);
                if (!isSucessful)
                    throw new UnhookWindowsHookExExpection(hookID);
                hookID = IntPtr.Zero;
            }
        }

        /// <summary>
        /// Destructor. Unhook current hook
        /// </summary>
        ~Hook()
        {
            Uninstall();
        }

        /// <summary>
        /// Registers hook with Windows API
        /// </summary>
        /// <param name="proc">Callback function</param>
        /// <returns>Hook ID</returns>
        protected virtual IntPtr SetHook(HOOKPROC proc)
        {
            throw new NotImplementedException();
        }

        protected virtual IntPtr HookFunc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            throw new NotImplementedException();
        }
    }
}