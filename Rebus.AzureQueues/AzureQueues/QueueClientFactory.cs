using System;
using System.Threading.Tasks;
using Azure;
using Azure.Core;
using Azure.Storage;
using Azure.Storage.Queues;

namespace Rebus.AzureQueues;
/// <summary>
/// A factory providing instances of <see cref="QueueClient"/>
/// </summary>
public class QueueClientFactory : QueueServiceClient, IQueueClientFactory {
  /// <summary>
  /// Initializes a new instance of the <see cref="QueueClientFactory"/>
  /// class for mocking.
  /// </summary>
  public QueueClientFactory() : base() { }

  /// <summary>
  /// Initializes a new instance of the <see cref="QueueClientFactory"/>
  /// class.
  /// </summary>
  /// <param name="connectionString">
  /// A connection string includes the authentication information
  /// required for your application to access data in an Azure Storage
  /// account at runtime.
  ///
  /// For more information, see
  /// <see href="https://docs.microsoft.com/azure/storage/common/storage-configure-connection-string">
  /// Configure Azure Storage connection strings</see>.
  /// </param>
  public QueueClientFactory(string connectionString) : base(connectionString) { }

  /// <summary>
  /// Initializes a new instance of the <see cref="QueueClientFactory"/>
  /// class.
  /// </summary>
  /// <param name="connectionString">
  /// A connection string includes the authentication information
  /// required for your application to access data in an Azure Storage
  /// account at runtime.
  ///
  /// For more information, see
  /// <see href="https://docs.microsoft.com/azure/storage/common/storage-configure-connection-string">
  /// Configure Azure Storage connection strings</see>.
  /// </param>
  /// <param name="options">
  /// Optional client options that define the transport pipeline
  /// policies for authentication, retries, etc., that are applied to
  /// every request.
  /// </param>
  public QueueClientFactory(string connectionString, QueueClientOptions options) : base(connectionString, options) { }

  /// <summary>
  /// Initializes a new instance of the <see cref="QueueClientFactory"/>
  /// class.
  /// </summary>
  /// <param name="serviceUri">
  /// A <see cref="Uri"/> referencing the queue that includes the
  /// name of the account, the name of the queue, and a SAS token.
  /// This is likely to be similar to "https://{account_name}.queue.core.windows.net/{queue_name}?{sas_token}".
  /// </param>
  /// <param name="options">
  /// Optional client options that define the transport pipeline
  /// policies for authentication, retries, etc., that are applied to
  /// every request.
  /// </param>
  /// <seealso href="https://docs.microsoft.com/azure/storage/common/storage-sas-overview">Storage SAS Token Overview</seealso>
  public QueueClientFactory(Uri serviceUri, QueueClientOptions options = null) : base(serviceUri, options) { }

  /// <summary>
  /// Initializes a new instance of the <see cref="QueueClientFactory"/>
  /// class.
  /// </summary>
  /// <param name="serviceUri">
  /// A <see cref="Uri"/> referencing the queue service.
  /// This is likely to be similar to "https://{account_name}.queue.core.windows.net".
  /// </param>
  /// <param name="credential">
  /// The shared key credential used to sign requests.
  /// </param>
  /// <param name="options">
  /// Optional client options that define the transport pipeline
  /// policies for authentication, retries, etc., that are applied to
  /// every request.
  /// </param>
  public QueueClientFactory(Uri serviceUri, StorageSharedKeyCredential credential, QueueClientOptions options = null) : base(serviceUri, credential, options) { }

  /// <summary>
  /// Initializes a new instance of the <see cref="QueueClientFactory"/>
  /// class.
  /// </summary>
  /// <param name="serviceUri">
  /// A <see cref="Uri"/> referencing the queue service.
  /// This is likely to be similar to "https://{account_name}.queue.core.windows.net".
  /// Must not contain shared access signature, which should be passed in the second parameter.
  /// </param>
  /// <param name="credential">
  /// The shared access signature credential used to sign requests.
  /// </param>
  /// <param name="options">
  /// Optional client options that define the transport pipeline
  /// policies for authentication, retries, etc., that are applied to
  /// every request.
  /// </param>
  /// <remarks>
  /// This constructor should only be used when shared access signature needs to be updated during lifespan of this client.
  /// </remarks>
  public QueueClientFactory(Uri serviceUri, AzureSasCredential credential, QueueClientOptions options = null) : base(serviceUri, credential, options) { }

  /// <summary>
  /// Initializes a new instance of the <see cref="QueueClientFactory"/>
  /// class.
  /// </summary>
  /// <param name="serviceUri">
  /// A <see cref="Uri"/> referencing the queue service.
  /// This is likely to be similar to "https://{account_name}.queue.core.windows.net".
  /// </param>
  /// <param name="credential">
  /// The token credential used to sign requests.
  /// </param>
  /// <param name="options">
  /// Optional client options that define the transport pipeline
  /// policies for authentication, retries, etc., that are applied to
  /// every request.
  /// </param>
  public QueueClientFactory(Uri serviceUri, TokenCredential credential, QueueClientOptions options = null) : base(serviceUri, credential, options) { }

  /// <summary>
  /// Retrieve an instance of <see cref="QueueClient"/> targeting the given queue
  /// </summary>
  /// <param name="queueName">The queue to retrieve a <see cref="QueueClient"/> for</param>
  public async Task<QueueClient> GetQueue(string queueName) => await Task.FromResult(GetQueueClient(queueName));
}
