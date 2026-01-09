using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace VEIVendorDemo
{
    public interface IRabbitMQConnector
    {
        public void Build(Action<string> messageReceived);
        public Task BasicConsumerAsync(string queueName);
    }
    public class RabbitMQConnector: IRabbitMQConnector, IHostedService
    {
        private ConnectionFactory? factory;
        private IConnection? connection;
        private IChannel? channel;
        private AsyncEventingBasicConsumer? consumer;
        private string consumerTag = "";

        private List<Stock> stockList = new List<Stock>();
        private readonly RabbitMQSettings options;

        internal Action<string>? MessageReceived;

        public List<Stock> StockList { get=> stockList; set=> stockList=value; }
        public List<Log> MessageLogs { get; } = new List<Log>();

        public RabbitMQConnector(IOptions<RabbitMQSettings> options)
        {
            this.options = options.Value;
            
        }
        internal async Task InitialObjectAsync()
        {

            factory = new ConnectionFactory()
            {
                HostName = options.HostName!,
                VirtualHost = options.VirtualHost!,
                Port = options.Port,
                CredentialsProvider = new BasicCredentialsProvider(options.UserName, options.UserName!, options.Password!),
                Ssl = new SslOption
                {
                    Enabled = true,
                    CertificateValidationCallback = (a, b, c, d) => true,
                    Version = System.Security.Authentication.SslProtocols.Tls12
                }
            };
            connection = await factory.CreateConnectionAsync();
            channel = await connection.CreateChannelAsync();

            consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                var node = JsonNode.Parse(message);
                var routingKey = ea.RoutingKey;

                var objMessage = node.Deserialize<MessageBody>();

                if (objMessage != null)
                {
                    MessageLogs.Add(new Log
                    {
                        msgId = objMessage.msgId,
                        body = objMessage.body.ToJsonString(new JsonSerializerOptions { WriteIndented = true, Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping}),
                        msgType = objMessage.msgType,
                        routingKey = routingKey,
                        source = objMessage.source,
                        timestamp = objMessage.timestamp,
                        traceId = objMessage.traceId,
                        version = objMessage.version

                    });
                }
                

                if (node != null && !String.IsNullOrEmpty(node["msgId"]?.ToString()))
                {
                    var msgType = node["msgType"]?.ToString();
                    if (msgType == "master-data-sync")
                    {
                        var data = node["body"].Deserialize<MasterData>();
                        if(data != null)
                        {
                            await ReceiveMasterData(data);
                        }
                    }
                    else if (msgType == "material-stock-sync")
                    {
                        var data = node["body"].Deserialize<StockData>();
                        if(data!=null)
                        {
                            await ReceiveStockData(data);
                        }
                    }
                    else if (msgType == "material-stock-lock")
                    {
                        var data = node["body"].Deserialize<StockLockData>();
                        if (data != null)
                        {
                            await ReceiveStockLockData(data);
                        }
                    }
                }

                MessageReceived?.Invoke(message);

                await channel.BasicAckAsync(deliveryTag: ea.DeliveryTag, multiple: false);
                return;
            };

        }

        internal async Task ReceiveMasterData(MasterData data)
        {
            if (stockList.Any(s=>s.material == data.material))
            {
                var stock = stockList.First(s => s.material == data.material);
                stock.materialDesc = data.materialDesc;
                stock.longText = data.longText;
                stock.purLongText = data.purLongText;
                stock.deleted = data.deleted;
                stock.unit = data.unit;
                stock.matType = data.matType;
           
            }
            else
            {
                stockList.Add(new Stock
                {
                    material = data.material,
                    materialDesc = data.materialDesc,
                    longText = data.longText,
                    purLongText = data.purLongText,
                    matType = data.matType,
                    unit = data.unit,
                    deleted = data.deleted,
                });
            }
        }

        internal async Task ReceiveStockData(StockData data)
        {
            if (stockList.Any(s => s.material == data.material))
            {
                var stock = stockList.First(s => s.material == data.material);
                stock.quantity = data.quantity;
                stock.lockQty = data.lockQuantity;
            }
        }

        internal async Task ReceiveStockLockData(StockLockData data)
        {
            if (stockList.Any(s => s.material == data.material))
            {
                var stock = stockList.First(s => s.material == data.material);
                if(data.behavior == "lock")
                {
                    stock.lockQty += data.quantity;
                }
                else
                {
                    stock.lockQty -= data.quantity;
                }
            }
        }

        public async Task BasicConsumerAsync(string queueName)
        {
            if (channel != null && channel.IsOpen && !string.IsNullOrEmpty(consumerTag))
            {

                // **关键步骤 2: 调用 BasicCancelAsync**
                await channel.BasicCancelAsync(consumerTag);

                // 清空标签，防止重复取消
                consumerTag = "";

            }

            consumerTag = await channel!.BasicConsumeAsync(queueName.Trim(), autoAck: false, consumer: consumer!);
        }



        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await InitialObjectAsync();
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            if (channel != null)
            {
                await channel.CloseAsync();
                channel.Dispose();
            }

            if(connection != null)
            {
                await connection.CloseAsync();
                connection.Dispose();
            }
        }

        public void Build(Action<string> messageReceived)
        {
            this.MessageReceived = messageReceived;
        }

        public Task SendFullStock()
        {
            throw new NotImplementedException();
        }
    }

    internal record MasterData(string material, string materialDesc, string longText, string purLongText, string unit, string matType, bool deleted, bool fullInventory)
    {

    }

    internal record MessageBody(string msgId, string msgType, DateTime timestamp, string source, string version, string traceId, JsonNode body)
    {

    }

    internal record StockData(string material, string type, decimal quantity, string unit, decimal validQuantity, decimal lockQuantity);

    internal record StockLockData(string material, string materialDesc, string behavior, decimal quantity, string unit);

    public class StockMoveData
    {
        public string Vendor { get; set; } = string.Empty;
        public string olUuid { get; set; } = string.Empty; 
        public List<StockMoveItem> Items { get; private set; } = new List<StockMoveItem>();
    }

    public record StockMoveItem(string material, string matType, string moveType, decimal quantity, string unit, string text);

}
