namespace EK.CommonUtils.ConsoleWriter;

public sealed class SystemConsoleWriter : IConsoleWriter
{
    public void Write(string? value)
    {
        Console.Write(value);
    }

    public void WriteLine(string? value)
    {
        Console.WriteLine(value);
    }
}
