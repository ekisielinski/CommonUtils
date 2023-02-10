﻿using System.Diagnostics.CodeAnalysis;

namespace EK.CommonUtils.Console;

[ExcludeFromCodeCoverage]
public sealed class NullConsoleWriter : IConsoleWriter
{
    private NullConsoleWriter() { }

    //====== IConsoleWriter

    public void Write(string? value)
    {
        // nop
    }

    public void WriteLine(string? value)
    {
        // nop
    }

    //====== public static properties

    public static NullConsoleWriter Instance { get; } = new();
}
