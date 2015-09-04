namespace nControls
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct nLocation
    {
        public int rowID;
        public string rowKey;
        public int columnID;
        public string columnKey;
    }
}

