namespace nControls
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct Element
    {
        public string key;
        public int index;
        public string value;
    }
}

