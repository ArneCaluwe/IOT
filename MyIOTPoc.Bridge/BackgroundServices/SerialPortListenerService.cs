using System.Diagnostics;
using System.IO.Ports;
using DeviceBridge.Commands.Dispatcher;
using DeviceBridge.Ports;

namespace MyIOTPoc.Bridge.BackgroundServices;

public class SerialPortListenerService(CommandDispatcher dispatcher, ILogger<SerialPortListenerService> logger, ActivitySource activitySource) : BackgroundService
{
    private SerialPort? _serialPort;
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {

        Log.SerialListenerStarted(logger, GetType().Name);


        // Start the serial listener on a separate task
        return Task.Run(() =>
        {
            int baudRate = 9600;

            // find your port by running `ls /dev/tty.*` in the terminal
            string portName = "/dev/tty.usbmodem13101";
            _serialPort = new SerialPort(portName, baudRate);
            _serialPort.DataReceived += (s, e) =>
            {
                using var activity = activitySource.StartActivity(ActivityKind.Consumer);
                try
                {

                    activity?.SetTag("BackgroundService", nameof(SerialPortListenerService));
                    string line = _serialPort.ReadLine().Replace("\\", string.Empty);
                    dispatcher.Dispatch(line);
                    logger.LogInformation($"[Serial] {line}");
                    activity?.SetStatus(ActivityStatusCode.Ok);
                    activity?.Stop();

                    // TODO: Process with your command dispatcher here
                }
                catch (Exception ex)
                {
                    logger.LogError(ex, "Error reading serial");
                    activity?.SetStatus(ActivityStatusCode.Ok);
                    activity?.Stop();
                }
            };

            _serialPort.Open();

            // Keep alive until shutdown
            stoppingToken.WaitHandle.WaitOne();
        }, stoppingToken);
    }

    public override void Dispose()
    {
        _serialPort?.Close();
        Log.SerialListenerDisposed(logger, nameof(SerialPortListenerService));
        base.Dispose();
    }
}


public partial class Log
{

    /// <summary>
    /// Logs information starting a new serial port listener
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="handler"></param>
    [LoggerMessage(
        EventId = 0,
        Level = LogLevel.Information,
        Message = "Serial listener {handler} started as a background service")]
    public static partial void SerialListenerStarted(ILogger logger, string handler);

    /// <summary>
    /// Logs information on serial port being disposed.
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="handler"></param>
    [LoggerMessage(
        EventId = 1,
        Level = LogLevel.Information,
        Message = "Serial Listener {handler} disposed"
    )]
    public static partial void SerialListenerDisposed(ILogger logger, string handler);
}