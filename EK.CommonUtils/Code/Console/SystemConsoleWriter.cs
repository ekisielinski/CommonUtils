namespace EK.CommonUtils.Console;

public sealed class SystemConsoleWriter : IConsoleWriter
{
    public void Write(string? value)
    {
        System.Console.Write(value);
    }

    public void WriteLine(string? value)
    {
        System.Console.WriteLine(value);
    }
}
