using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace DroidExplorer.ShellExtension {
  [ComImport ( ), ComVisible ( true ),
    InterfaceType ( ComInterfaceType.InterfaceIsIUnknown ),
    GuidAttribute ( "0000010b-0000-0000-C000-000000000046" )]
  public interface IPersistFile {
    [PreserveSig]
    uint GetClassID ( out Guid pClassID );

    [PreserveSig]
    uint IsDirty ( );

    [PreserveSig]
    uint Load ( [In, MarshalAs ( UnmanagedType.LPWStr )] string pszFileName, [In] uint dwMode );

    [PreserveSig]
    uint Save ( [In, MarshalAs ( UnmanagedType.LPWStr )] string pszFileName, [In] bool fRemember );

    [PreserveSig]
    uint SaveCompleted ( [In, MarshalAs ( UnmanagedType.LPWStr )] string pszFileName );

    [PreserveSig]
    uint GetCurFile ( [MarshalAs ( UnmanagedType.LPWStr )] out string ppszFileName );
  }

}
