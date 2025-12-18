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

        public Form1(ICallApiService apiService, IRabbitMQConnector connector)
        {
            InitializeComponent();
            _apiService = apiService;
            _connector = (RabbitMQConnector)connector;

            _connector.Build((msg) =>
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
            });
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
            bindingSource1.DataSource = _connector.StockList;
            bindingSource2.DataSource = _connector.MessageLogs;
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            dataGridView1.EndEdit();
            try
            {
                var stockMoveData = new StockMoveData()
                {
                    Vendor = textBox1.Text,
                };
                stockMoveData.Items.AddRange(_connector.StockList.Select(s =>
                {
                    return new StockMoveItem(s.material, s.matType, "full", s.quantity, s.unit, "库存全量同步");

                }));

                var response = await _apiService.SendStockInfoAsync(stockMoveData);
                if (string.IsNullOrEmpty(response))
                {
                    MessageBox.Show("库存调整请求发送成功.");
                }
                else
                {
                    MessageBox.Show($"库存调整请求发送失败: {response}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"发送库存调整请求时发生错误: {ex.Message}");
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            dataGridView1.EndEdit();
            try
            {
                var stockMoveData = new StockMoveData()
                {
                    Vendor = textBox1.Text,
                };
                stockMoveData.Items.AddRange(_connector.StockList.Where(s=>s.offset != 0m).Select(s =>
                {
                    return new StockMoveItem(s.material, s.matType, s.offset > 0 ? "increase": "decrease", System.Math.Abs(s.offset), s.unit, "库存调整");

                }));

                var response = await _apiService.SendStockInfoAsync(stockMoveData);
                if (string.IsNullOrEmpty(response))
                {
                    MessageBox.Show("库存调整请求发送成功.");
                }
                else
                {
                    MessageBox.Show($"库存调整请求发送失败: {response}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"发送库存调整请求时发生错误: {ex.Message}");
            }
        }
    }

    public class Stock
    {
        public string material { get; set; } = string.Empty;
        public string materialDesc { get; set; } = string.Empty;
        public string longText { get; set; } = string.Empty;
        public string purLongText { get; set; } = string.Empty;
        public bool deleted { get; set; }
        public string unit { get; set; } = string.Empty;
        public string matType { get; set; } = string.Empty;

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
