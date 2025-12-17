// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Threading.Channels;

var factory = new ConnectionFactory()
{
    HostName = "localhost",
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
var connection = await factory.CreateConnectionAsync();
var channel = await connection.CreateChannelAsync();

var consumer = new AsyncEventingBasicConsumer(channel);
