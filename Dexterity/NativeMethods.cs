using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace Dexterity {
    public static class NativeMethods {
        public delegate IntPtr WNDPROC(IntPtr hWnd, uint uMsg, UIntPtr wParam, IntPtr lParam);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct WNDCLASSA {
            public uint style;
            public WNDPROC lpfnWndProc;
            public int cbClsExtra;
            public int cbWndExtra;
            public IntPtr hInstance;
            public IntPtr hIcon;
            public IntPtr hCursor;
            public IntPtr hbrBackground;
            public string lpszMenuName;
            public string lpszClassName;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct WNDCLASSEXA {
            public uint cbSize;
            public uint style;
            public WNDPROC lpfnWndProc;
            public int cbClsExtra;
            public int cbWndExtra;
            public IntPtr hInstance;
            public IntPtr hIcon;
            public IntPtr hCursor;
            public IntPtr hbrBackground;
            public string lpszMenuName;
            public string lpszClassName;
            public IntPtr hIconSm;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct POINT {
            public long x;
            public long y;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        public struct MSG {
            public IntPtr hwnd;
            public uint message;
            public UIntPtr wParam;
            public IntPtr lParam;
            public uint time;
            public POINT pt;
            public uint lPrivate;
        }

        [DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern ushort RegisterClassA([In] ref WNDCLASSA windowClass);

        [DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern ushort RegisterClassExA([In] ref WNDCLASSEXA windowClass);

        [DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern IntPtr DefWindowProcA(
            [In] IntPtr hWnd,
            [In] uint Msg,
            [In] UIntPtr wParam,
            [In] IntPtr lParam
        );

        [DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern IntPtr CreateWindowExA(
            [In] uint dwExStyle,
            [In] [Optional] string lpClassName,
            [In] [Optional] string lpWindowName,
            [In] uint dwStyle,
            [In] int X,
            [In] int Y,
            [In] int nWidth,
            [In] int nHeight,
            [In] [Optional] IntPtr hWndParent,
            [In] [Optional] IntPtr hMenu,
            [In] [Optional] IntPtr hInstance,
            [In] [Optional] IntPtr lpParam
        );

        [DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern bool ShowWindow([In] IntPtr hWnd, int nCmdShow);

        [DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern void PostQuitMessage([In] int nExitCode);

        [DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern bool GetMessage(ref MSG lpMsg, [In] [Optional] IntPtr hWnd, [In] uint wMsgFilterMin, [In] uint wMsgFilterMax);

        [DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern bool TranslateMessage([In] ref MSG lpMsg);

        [DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true)]
        public static extern IntPtr DispatchMessage([In] ref MSG lpMsg);

        public const ushort WM_DESTROY = 0x0002;

        public const int WS_OVERLAPPED = 0x00000000;
        public const int WS_CAPTION = 0x00C00000;
        public const int WS_SYSMENU = 0x00080000;
        public const int WS_THICKFRAME = 0x00040000;
        public const int WS_MAXIMIZEBOX = 0x00010000;
        public const int WS_MINIMIZEBOX = 0x00020000;
        public const int WS_OVERLAPPEDWINDOW = (WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_THICKFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX);

        public const int CW_USEDEFAULT = unchecked((int)0x80000000);
    }
}