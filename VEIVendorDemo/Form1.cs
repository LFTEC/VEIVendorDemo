using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Channels;

namespace VEIVendorDemo
{
    public partial class Form1 : Form
    {

        private RabbitMQConnector _connector;
        private readonly ICallApiService _apiService;

        public Form1(ICallApiService apiService)
        {
            InitializeComponent();
            _apiService = apiService;
        }

        private async Task BuildConnection()
        {
            try
            {
                _connector = await RabbitMQConnector.GetInstance();
                _connector.MessageReceived += (msg) =>
                {
                    this.Invoke(new Action(() =>
                    {
                        bindingSource1.ResetBindings(false);
                        bindingSource2.ResetBindings(false);
                        StringBuilder sb = new StringBuilder();
                        sb.Append(richTextBox1.Text);
                        sb.AppendLine();
                        sb.AppendLine("================================");
                        sb.AppendLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        sb.AppendLine(msg);
                        sb.AppendLine("================================");
                        richTextBox1.Text = sb.ToString();
                    }));
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

            try
            {
                await _connector.BasicConsumerAsync(textBox1.Text);
                MessageBox.Show("成功绑定供应商队列.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"绑定供应商队列时发生错误: {ex.Message}");
            }
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            await BuildConnection();
            bindingSource1.DataSource = _connector.StockList;
            bindingSource2.DataSource = _connector.MessageLogs;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.EndEdit();

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
        public string? msgId { get; set; }
        public string? msgType { get; set; }
        public string? source { get; set; }
        public string? version { get; set; }
        public string? traceId { get; set; }
        public string? body { get; set; }
        public DateTime timestamp { get; set; }
        public string? routingKey { get; set; }

    }
}
