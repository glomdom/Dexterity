using System.Runtime.InteropServices;
using static Dexterity.NativeMethods;

namespace Dexterity.UnmanagedTypes;

public sealed class UnmanagedString : IDisposable {
    public nint Ptr { get; }

    public UnmanagedString(string str) {
        Ptr = Marshal.StringToHGlobalAnsi(str);
    }

    public void Dispose() {
        if (Ptr != NULL) {
            Marshal.FreeHGlobal(Ptr);
        }
    }

    public static implicit operator nint(UnmanagedString unmanagedString) => unmanagedString.Ptr;
}
