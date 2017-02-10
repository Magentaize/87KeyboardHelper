using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace _87KeyboardHelper.WindowsApi
{
    public class MouseHook : Hook
    {
        /// <summary>
        /// Function to be called when defined even occurs
        /// </summary>
        /// <param name="mouseStruct">MSLLHOOKSTRUCT mouse structure</param>
        public delegate void MouseHookCallback(MOUSEINPUT mouseStruct);

        #region Events

        public event MouseHookCallback LeftButtonDown;
        public event MouseHookCallback LeftButtonUp;
        public event MouseHookCallback RightButtonDown;
        public event MouseHookCallback RightButtonUp;
        public event MouseHookCallback MouseMove;
        public event MouseHookCallback MouseWheel;
        public event MouseHookCallback DoubleClick;
        public event MouseHookCallback MiddleButtonDown;
        public event MouseHookCallback MiddleButtonUp;

        #endregion

        /// <summary>
        /// Sets hook and assigns its ID for tracking
        /// </summary>
        /// <param name="proc">Internal callback function</param>
        /// <returns>Hook ID</returns>
        protected override IntPtr SetHook(HOOKPROC proc)
        {
            using (var module = Process.GetCurrentProcess().MainModule)
                return SetWindowsHookEx(HOOKTYPE.WH_MOUSE_LL, proc, GetModuleHandle(module.ModuleName), 0);
        }

        /// <summary>
        /// Callback function
        /// </summary>
        protected override IntPtr HookFunc(int nCode, IntPtr wParam, IntPtr lParam)
        {
            // parse system messages
            if (nCode >= 0)
            {
                switch ((WM_MESSAGE) wParam)
                {
                    case WM_MESSAGE.WM_LBUTTONDOWN:
                        LeftButtonDown?.Invoke((MOUSEINPUT)Marshal.PtrToStructure(lParam, typeof(MOUSEINPUT)));
                        break;
                    case WM_MESSAGE.WM_LBUTTONUP:
                        LeftButtonUp?.Invoke((MOUSEINPUT)Marshal.PtrToStructure(lParam, typeof(MOUSEINPUT)));
                        break;
                    case WM_MESSAGE.WM_RBUTTONDOWN:
                        RightButtonDown?.Invoke((MOUSEINPUT)Marshal.PtrToStructure(lParam, typeof(MOUSEINPUT)));
                        break;
                    case WM_MESSAGE.WM_RBUTTONUP:
                        RightButtonUp?.Invoke((MOUSEINPUT)Marshal.PtrToStructure(lParam, typeof(MOUSEINPUT)));
                        break;
                    case WM_MESSAGE.WM_MOUSEMOVE:
                        MouseMove?.Invoke((MOUSEINPUT)Marshal.PtrToStructure(lParam, typeof(MOUSEINPUT)));
                        break;
                    case WM_MESSAGE.WM_MOUSEWHEEL:
                        MouseWheel?.Invoke((MOUSEINPUT)Marshal.PtrToStructure(lParam, typeof(MOUSEINPUT)));
                        break;
                    case WM_MESSAGE.WM_LBUTTONDBLCLK:
                        DoubleClick?.Invoke((MOUSEINPUT)Marshal.PtrToStructure(lParam, typeof(MOUSEINPUT)));
                        break;
                    case WM_MESSAGE.WM_MBUTTONDOWN:
                        MiddleButtonDown?.Invoke((MOUSEINPUT)Marshal.PtrToStructure(lParam, typeof(MOUSEINPUT)));
                        break;
                    case WM_MESSAGE.WM_MBUTTONUP:
                        MiddleButtonUp?.Invoke((MOUSEINPUT)Marshal.PtrToStructure(lParam, typeof(MOUSEINPUT)));
                        break;
                }
            }
            return CallNextHookEx(hookID, nCode, wParam, lParam);
        }
    }
}