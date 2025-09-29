using System;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;
using DeviceBridge.Commands.Dispatcher;
using DeviceBridge.Ports;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MyIOTPoc.Bridge.BackgroundServices
{
    public class SerialPortListenerService(CommandDispatcher dispatcher, ILogger<SerialPortListenerService> logger) : BackgroundService
    {
        private readonly ILogger<SerialPortListenerService> _logger = logger;
        private SerialPort? _serialPort;
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // Start the serial listener on a separate task
            return Task.Run(() =>
            {
                int baudRate = 9600;

                // find your port by running `ls /dev/tty.*` in the terminal
                string portName = "/dev/tty.usbmodem1401";
                using var serialPort = new SerialPortMock(portName, 9600)
                                            .EnsureOpened();
                _serialPort = new SerialPort(portName, baudRate);
                _serialPort.DataReceived += (s, e) =>
                {
                    try
                    {
                        string line = _serialPort.ReadLine();
                        dispatcher.Dispatch(line);
                        _logger.LogInformation($"[Serial] {line}");

                        // TODO: Process with your command dispatcher here
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error reading serial");
                    }
                };

                _serialPort.Open();
                _logger.LogInformation("Serial listener started.");

                // Keep alive until shutdown
                stoppingToken.WaitHandle.WaitOne();
            }, stoppingToken);
        }

        public override void Dispose()
        {
            _serialPort?.Close();
            base.Dispose();
        }
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
        Message = "Handling a new request in {handler}")]
    public static partial void HandlingRequest(ILogger logger, string handler);

}