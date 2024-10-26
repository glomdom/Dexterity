using System.Runtime.InteropServices;

namespace Dexterity.UnmanagedTypes;

public sealed class ScopedFunctionPointer<T> : IDisposable where T : Delegate {
    public nint Ptr { get; }

    public ScopedFunctionPointer(T del) {
        Ptr = Marshal.GetFunctionPointerForDelegate(del);
    }

    /// <summary>
    /// Does nothing as function pointers don't need to be EXPLICITLY freed.
    /// </summary>
    public void Dispose() { }

    public static implicit operator nint(ScopedFunctionPointer<T> scopedFunctionPointer) => scopedFunctionPointer.Ptr;
}