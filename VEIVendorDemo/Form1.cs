using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Channels;

namespace VEIVendorDemo
{
    public partial class Form1 : Form
    {

        private ConnectionFactory? factory;
        private IConnection? connection;
        private IChannel? channel;
        private AsyncEventingBasicConsumer? consumer;
        public List<Stock> stocks = new List<Stock>();
        private string consumerTag = "";



        public Form1()
        {
            InitializeComponent();

            //BuildConnection().Wait();
            bindingSource1.DataSource = stocks;

        }

        

    private async Task BuildConnection()
        {
            try
            {
                factory = new ConnectionFactory() { HostName = "localhost", RequestedConnectionTimeout = TimeSpan.FromSeconds(10) };
                connection = await factory.CreateConnectionAsync();
                channel = await connection.CreateChannelAsync();

                consumer = new AsyncEventingBasicConsumer(channel);
                consumer.ReceivedAsync += async (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    var node = JsonNode.Parse(message);
                    
                    var routingKey = ea.RoutingKey;
                    Console.WriteLine($" [x] Received '{routingKey}':'{message}'");
                    return ;
                };
            }
            catch (Exception ex)
            {
                MessageBox.Show("连接RabbitMQ服务器失败: " + ex.Message);
                Application.Exit();
            }

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("请输入供应商号.");
                return;
            }

            if (channel != null && channel.IsOpen && !string.IsNullOrEmpty(consumerTag))
            {
                try
                {
                    // **关键步骤 2: 调用 BasicCancelAsync**
                    await channel.BasicCancelAsync(consumerTag);

                    // 清空标签，防止重复取消
                    consumerTag = "";
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"停止消费者时发生错误: {ex.Message}");
                }
            }

            consumerTag = await channel!.BasicConsumeAsync(textBox1.Text.Trim(), autoAck: false, consumer: consumer!);
        }
    }

    public class Stock
    {
        public string? material { get; set; }
        public string? materialDesc { get; set; }
        public string? longText { get; set; }
        public string? purLongText { get; set; }
        public bool deleted { get; set; }
        public string? unit { get; set; }
        public string? matType { get; set; }

        public decimal quantity { get; set; }
        public decimal offset { get; set; }
        public decimal lockQty { get; set; }
    }

    public class Log
    { 
        
    }
}
