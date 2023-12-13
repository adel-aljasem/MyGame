﻿using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;
using Microsoft.Extensions.Logging;

namespace AdilGame.System
{
    public class NetworkSystem
    {
        public HubConnection hubConnection { get; private set; }

        public NetworkSystem(string url)
        {
 
            hubConnection = new HubConnectionBuilder().WithUrl(url, opt => { opt.Transports = Microsoft.AspNetCore.Http.Connections.HttpTransportType.WebSockets; }).AddMessagePackProtocol().Build();
            StartConnection();
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

    }

}