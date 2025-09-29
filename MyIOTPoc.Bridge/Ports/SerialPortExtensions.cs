using System.IO.Ports;

namespace DeviceBridge.Ports;

/// <summary>
/// Extension methods for <see cref="SerialPort"/>
/// </summary>
public static class SerialPortExtensions
{
    /// <summary>
    /// Opens a SerialPort if it has not yet been opened.
    /// </summary>
    /// <param name="port"></param>
    /// <returns></returns>
    public static SerialPort EnsureOpened(this SerialPort port)
    {
        if (port.IsOpen)
            return port;
        try
        {
            port.Open();
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Error opening serial port: {ex.Message}");
            throw;
        }
        return port;
    }

    public static SerialPortMock EnsureOpened(this SerialPortMock port)
    {
        if (port.IsOpen)
            return port;
        try
        {
            port.Open();
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Error opening serial port: {ex.Message}");
            throw;
        }
        return port;
    }

}