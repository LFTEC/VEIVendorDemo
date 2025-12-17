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
    internal class RabbitMQConnector
    {
        private ConnectionFactory? factory;
        private IConnection? connection;
        private IChannel? channel;
        private AsyncEventingBasicConsumer? consumer;
        private string consumerTag = "";

        private List<Stock> stockList = new List<Stock>();
        private HttpMessageHandler messageHandler = new HttpClientHandler { SslProtocols = System.Security.Authentication.SslProtocols.Tls12};

        internal Action<string>? MessageReceived;

        public List<Stock> StockList { get=> stockList; set=> stockList=value; }
        public List<Log> MessageLogs { get; } = new List<Log>();

        internal static async Task<RabbitMQConnector> GetInstance()
        {
            var instance = new RabbitMQConnector();
            await instance.InitialObjectAsync();
            return instance;
        }

        internal async Task InitialObjectAsync()
        {

            factory = new ConnectionFactory()
            {
                HostName = "121.36.58.218",
                VirtualHost = "vei",
                Port = 5671,
                CredentialsProvider = new BasicCredentialsProvider("root", "root", "Xiangaimima@1"),
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
                            //处理锁库消息
                            //此处省略具体业务逻辑代码
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

        internal async Task BasicConsumerAsync(string queueName)
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

        internal async Task SendFullStock()
        {

        }

        private async Task CallAPI()
        {
            HttpClient client = new HttpClient(messageHandler);

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

}
