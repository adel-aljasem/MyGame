using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading;

namespace AdilGame.System
{
    public class NetworkSystem
    {
        public HubConnection hubConnection { get; private set; }
        private Timer pingTimer;
        public long Ping;

        public NetworkSystem(string url)
        {
 
            hubConnection = new HubConnectionBuilder().WithUrl(url, opt => { opt.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransportType.WebSockets; }).AddMessagePackProtocol().Build();
            StartConnection();
            pingTimer = new Timer(PingTimerCallback, null, 0, 333);

        }


        private void StartConnection()
        {
            // Start the SignalR connection.
            _ = hubConnection.StartAsync()
                .ContinueWith(task =>
                {
                    if (task.IsFaulted)
                    {
                        Console.WriteLine("Error starting SignalR connection: ");
                    }
                    else
                    {
                        Console.WriteLine("SignalR connection started successfully.");
                    }
                });

        }
        private void PingTimerCallback(object state)
        {
            // Asynchronously call MeasurePingAsync.
            // Note: This doesn't await the task. Errors and return values should be handled within MeasurePingAsync.
            var _ = MeasurePingAsync();
        }


        public async Task<long> MeasurePingAsync()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            try
            {
                // Assuming 'SendPing' is the method name on the server-side SignalR Hub.
                await hubConnection.InvokeAsync("SendPing");

                stopwatch.Stop();
                Console.WriteLine($"Ping: {stopwatch.ElapsedMilliseconds} ms");
                Ping = stopwatch.ElapsedMilliseconds;
                return stopwatch.ElapsedMilliseconds;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error measuring ping: {ex.Message}");
                return -1; // Indicates an error
            }
        }


    }

}
