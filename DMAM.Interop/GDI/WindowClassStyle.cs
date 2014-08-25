﻿using System;

namespace DMAM.Interop.GDI
{
    [Flags]
    public enum WindowClassStyle : uint
    {
        CS_NONE = 0x0000,
        CS_VREDRAW = 0x0001,
        CS_HREDRAW = 0x0002,
        CS_DBLCLKS = 0x0008,
        CS_OWNDC = 0x0020,
        CS_CLASSDC = 0x0040,
        CS_PARENTDC = 0x0080,
        CS_NOCLOSE = 0x0200,
        CS_SAVEBITS = 0x0800,
        CS_BYTEALIGNCLIENT = 0x1000,
        CS_BYTEALIGNWINDOW = 0x2000,
        CS_GLOBALCLASS = 0x4000,
        CS_IME = 0x00010000,
        CS_DROPSHADOW = 0x00020000,
    }
}
