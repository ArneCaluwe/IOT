using System.IO.Ports;
using System.Text.Json;
using DeviceBridge.Commands.Devices;
using MyIOTPoc.Bridge.Commands.Base;

namespace DeviceBridge.Ports;

/// <summary>
/// A mock for a Serial Port which does not require external devices.
/// <see cref="SerialPort"/>
/// </summary>
/// <param name="portName"></param>
/// <param name="Baudrate"></param>
public class SerialPortMock(string portName, int baudRate) : SerialPort(portName, baudRate)
{
    public new event EventHandler<string>? DataReceived;

    private DateTime? openedDate;
    private string? lastMessage;
    /// <summary>
    /// Mocks SerialPort.Open
    /// <see cref="SerialPort.Open()"/>
    /// </summary>
    new public void Open()
    {
        openedDate = DateTime.UtcNow;
        DataReceived!.Invoke(this, ReadLine());
    }

    new public bool IsOpen => openedDate == null;


    /// <summary>
    /// Mocks SerialPort.Writeline
    /// <see cref="SerialPort.WriteLine(string)"/> 
    /// </summary>
    /// <param name="message">the message to write to serial</param>
    new public void WriteLine(string message)
    {
        lastMessage = message;
    }

    /// <summary>
    /// Mocks SerialPort.ReadLine
    /// </summary>
    /// <returns>a mock serialized DataMetricsDto</returns>
    new public string ReadLine()
    {
        CommandEnvelope envelope = new(
            nameof(RegisterDeviceCommand),
            new RegisterDeviceCommand("Arduino Uno Mock", ["Temperature", "Humidity"], "Mock Desk", "1.0.0")
            );

        return lastMessage switch
        {
            "Initialize Connection" => JsonSerializer.Serialize(envelope),
            _ => JsonSerializer.Serialize(envelope),
        };
    }
}

