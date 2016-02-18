﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RemoteWork
{
    //испольльзуется для подгрузки более проиводительной версии richtextbox
    class Win32
    {
        [DllImport("kernel32.dll", EntryPoint = "LoadLibraryW", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern IntPtr LoadLibraryW(string s_File);

        public static IntPtr LoadLibrary(string s_File)
        {
            IntPtr h_Module = LoadLibraryW(s_File);
            if (h_Module != IntPtr.Zero)
                return h_Module;

            int s32_Error = Marshal.GetLastWin32Error();
            throw new Win32Exception(s32_Error);
        }
    }
}
