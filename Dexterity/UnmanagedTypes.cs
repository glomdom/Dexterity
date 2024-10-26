using System.Runtime.InteropServices;

namespace Dexterity.UnmanagedTypes;

public sealed class UnmanagedString : IDisposable {
    public nint Ptr { get; }

    public UnmanagedString(string str) {
        Ptr = Marshal.StringToHGlobalAnsi(str);
    }

    public void Dispose() {
        if (Ptr != IntPtr.Zero) {
            Marshal.FreeHGlobal(Ptr);
        }
    }

    public static implicit operator nint(UnmanagedString unmanagedString) => unmanagedString.Ptr;
}

public sealed class ScopedFunctionPointer<T> : IDisposable where T : Delegate {
    public nint Ptr { get; }

    public ScopedFunctionPointer(T del) {
        Ptr = Marshal.GetFunctionPointerForDelegate(del);
    }

    /// <summary>
    /// Does nothing as function pointers don't need to be EXPLICITLY freed.
    /// </summary>
    public void Dispose() {}

    public static implicit operator nint(ScopedFunctionPointer<T> scopedFunctionPointer) => scopedFunctionPointer.Ptr;
}