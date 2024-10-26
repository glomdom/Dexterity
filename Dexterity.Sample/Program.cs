using System.ComponentModel;
using System.Runtime.InteropServices;
using static Dexterity.NativeMethods;

namespace Dexterity.Sample;

internal static class Program {
    private static IntPtr WindowProc(IntPtr hWnd, uint uMsg, UIntPtr wParam, IntPtr lParam) {
        switch (uMsg) {
            case WM_DESTROY:
                PostQuitMessage(0);

                return 0;
        }

        return DefWindowProcA(hWnd, uMsg, wParam, lParam);
    }

    public static void Main() {
        var hInstance = Marshal.GetHINSTANCE(typeof(Program).Module);
        var className = "DexterityMainWindow";

        var wc = new WNDCLASSEXA {
            cbSize = (uint)Marshal.SizeOf<WNDCLASSEXA>(),
            lpfnWndProc = WindowProc,
            hInstance = hInstance,
            lpszClassName = className,
        };

        RegisterClassExA(ref wc);

        var hwnd = CreateWindowExA(
            0,
            className,
            "Dexterity",
            WS_OVERLAPPEDWINDOW,
            CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT, CW_USEDEFAULT,
            IntPtr.Zero, IntPtr.Zero, hInstance, IntPtr.Zero
        );

        if (hwnd == IntPtr.Zero) {
            throw new Win32Exception(Marshal.GetLastWin32Error(), "Failed to create window");
        }

        ShowWindow(hwnd, 1);

        var msg = new MSG();
        while (GetMessage(ref msg, IntPtr.Zero, 0, 0) != false) {
            TranslateMessage(ref msg);
            DispatchMessage(ref msg);
        }
    }
}