using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

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
