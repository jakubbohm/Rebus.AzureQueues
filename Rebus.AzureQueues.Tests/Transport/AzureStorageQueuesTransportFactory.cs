﻿using System;
using System.Collections.Concurrent;
using Rebus.AzureQueues.Transport;
using Rebus.Config;
using Rebus.Logging;
using Rebus.Tests.Contracts.Transports;
using Rebus.Threading.SystemThreadingTimer;
using Rebus.Time;
using Rebus.Transport;

namespace Rebus.AzureQueues.Tests.Transport;

public class AzureStorageQueuesTransportFactory : ITransportFactory
{
    readonly ConcurrentDictionary<string, AzureStorageQueuesTransport> _transports = new(StringComparer.OrdinalIgnoreCase);

    public ITransport CreateOneWayClient()
    {
        return Create(null);
    }

    public ITransport Create(string inputQueueAddress)
    {
        if (inputQueueAddress == null)
        {
            var transport = new AzureStorageQueuesTransport(new ConnectionStringQueueClientFactory(AzureConfig.ConnectionString), null, new ConsoleLoggerFactory(false), new AzureStorageQueuesTransportOptions(), new DefaultRebusTime(),new SystemThreadingTimerAsyncTaskFactory(new ConsoleLoggerFactory(false)));

            transport.Initialize();

            return transport;
        }

        return _transports.GetOrAdd(inputQueueAddress, address =>
        {
            var transport = new AzureStorageQueuesTransport(new ConnectionStringQueueClientFactory(AzureConfig.ConnectionString), inputQueueAddress, new ConsoleLoggerFactory(false), new AzureStorageQueuesTransportOptions(), new DefaultRebusTime(), new SystemThreadingTimerAsyncTaskFactory(new ConsoleLoggerFactory(false)));

            transport.PurgeInputQueue();

            transport.Initialize();

            return transport;
        });
    }

    public void CleanUp()
    {
    }
}