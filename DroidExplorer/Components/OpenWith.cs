using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;

namespace DroidExplorer.Components {
  class OpenWith {
    [Serializable]
    private struct ShellExecuteInfo {
      public int Size;
      public uint Mask;
      public IntPtr hwnd;
      public string Verb;
      public string File;
      public string Parameters;
      public string Directory;
      public uint Show;
      public IntPtr InstApp;
      public IntPtr IDList;
      public string Class;
      public IntPtr hkeyClass;
      public uint HotKey;
      public IntPtr Icon;
      public IntPtr Monitor;
    }

    // Code For OpenWithDialog Box

    [DllImport ( "shell32.dll", SetLastError = true )]
    private extern static bool ShellExecuteEx ( ref ShellExecuteInfo lpExecInfo );

    private const uint SW_NORMAL = 1;
    private const uint SEE_MASK_NOCLOSEPROCESS = 64;
    private const string VERB_OPENAS = "openas";

    public static bool ShowDialog ( Icon windowIcon, string file ) {
      ShellExecuteInfo sei = new ShellExecuteInfo ( );
      sei.Size = Marshal.SizeOf ( sei );
      sei.Verb = VERB_OPENAS;
      sei.Icon = windowIcon.Handle;
      sei.Mask = SEE_MASK_NOCLOSEPROCESS;
      sei.File = file;
      sei.Show = SW_NORMAL;
      return ShellExecuteEx ( ref sei );
    }
  }
}
