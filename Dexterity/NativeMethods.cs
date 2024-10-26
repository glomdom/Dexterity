using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using System.Security.Cryptography.X509Certificates;
using static Dexterity.NativeMethods;

namespace Dexterity {
    public static partial class NativeMethods {
        public delegate IntPtr WNDPROC(IntPtr hWnd, uint uMsg, UIntPtr wParam, IntPtr lParam);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi)]
        [CustomMarshaller(typeof(WNDCLASSA), MarshalMode.ManagedToUnmanagedIn, typeof(WNDCLASSAMarshaller))]
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
        [CustomMarshaller(typeof(WNDCLASSEXA), MarshalMode.ManagedToUnmanagedIn, typeof(WNDCLASSEXAMarshaller))]
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

        [LibraryImport("user32.dll", StringMarshalling = StringMarshalling.Utf8, SetLastError = true)]
        public static partial ushort RegisterClassA(nint windowClass);

        [LibraryImport("user32.dll", StringMarshalling = StringMarshalling.Utf8, SetLastError = true)]
        public static partial ushort RegisterClassExA(nint windowClass);

        [LibraryImport("user32.dll", StringMarshalling = StringMarshalling.Utf8, SetLastError = true)]
        public static partial IntPtr DefWindowProcA(IntPtr hWnd, uint Msg, UIntPtr wParam, IntPtr lParam);

        [LibraryImport("user32.dll", StringMarshalling = StringMarshalling.Utf8, SetLastError = true)]
        public static partial IntPtr CreateWindowExA(
            uint dwExStyle,
            [Optional] string lpClassName,
            [Optional] string lpWindowName,
            uint dwStyle,
            int X,
            int Y,
            int nWidth,
            int nHeight,
            [Optional] IntPtr hWndParent,
            [Optional] IntPtr hMenu,
            [Optional] IntPtr hInstance,
            [Optional] IntPtr lpParam
        );

        [LibraryImport("user32.dll", StringMarshalling = StringMarshalling.Utf8, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static partial bool ShowWindow(IntPtr hWnd, int nCmdShow);

        [LibraryImport("user32.dll", StringMarshalling = StringMarshalling.Utf8, SetLastError = true)]
        public static partial void PostQuitMessage(int nExitCode);

        [LibraryImport("user32.dll", EntryPoint = "GetMessageA", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static partial bool GetMessage(ref MSG lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax);

        [LibraryImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static partial bool TranslateMessage(ref MSG lpMsg);

        [LibraryImport("user32.dll", EntryPoint = "DispatchMessageA", SetLastError = true)]
        public static partial IntPtr DispatchMessage(ref MSG lpMsg);

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

    public static class WNDCLASSAMarshaller {
        public static nint ConvertToUnmanaged(WNDCLASSA managed) {
            var unmanaged = Marshal.AllocHGlobal(Marshal.SizeOf<WNDCLASSA>());
            Marshal.StructureToPtr(managed, unmanaged, false);
            return unmanaged;
        }

        public static void Free(nint unmanaged) {
            Marshal.FreeHGlobal(unmanaged);
        }
    }

    public static class WNDCLASSEXAMarshaller {
        public static nint ConvertToUnmanaged(WNDCLASSEXA managed) {
            var unmanaged = Marshal.AllocHGlobal(Marshal.SizeOf<WNDCLASSEXA>());
            Marshal.StructureToPtr(managed, unmanaged, false);
            return unmanaged;
        }

        public static void Free(nint unmanaged) {
            Marshal.FreeHGlobal(unmanaged);
        }
    }

    public static class HelperMethods {
        public static ushort RegisterClassExAHelper(WNDCLASSEXA windowClass) {
            nint unmanaged = WNDCLASSEXAMarshaller.ConvertToUnmanaged(windowClass);
            try {
                return RegisterClassExA(unmanaged);
            } finally {
                WNDCLASSEXAMarshaller.Free(unmanaged);
            }
        }

        public static ushort RegisterClassAHelper(WNDCLASSA windowClass) {
            nint unmanaged = WNDCLASSAMarshaller.ConvertToUnmanaged(windowClass);
            try {
                return RegisterClassA(unmanaged);
            } finally {
                WNDCLASSAMarshaller.Free(unmanaged);
            }
        }
    }
}
