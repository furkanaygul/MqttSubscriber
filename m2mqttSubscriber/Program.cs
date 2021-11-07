using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Net;
using System.Threading;

namespace m2mqttSubscriber
{
    class Program
    {
        static MqttClient mqttClient;
        static string clientId;
        static string ip;
        static string[] topic;
        static int port;
        static void connect()
        {
            try
            {
                ip = "192.168.20.128";
                port = 1883;
                mqttClient = new MqttClient(IPAddress.Parse(ip),port , false, null, null, MqttSslProtocols.TLSv1_2);
                mqttClient.MqttMsgPublishReceived += client_MqttMsgPublishReceived;
                mqttClient.ConnectionClosed += MqttClient_ConnectionClosed;
                clientId = "subscriber";
                topic = new string[] { "temprature"};
                mqttClient.Subscribe(topic, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });
                mqttClient.Connect(clientId, "furkan", "101294",false,(ushort)60);
                //mqttClient.Connect(clientId);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("error: " + ex.Message);
                connect();
            }
        }

        private static void MqttClient_ConnectionClosed(object sender, EventArgs e)
        {
            Console.WriteLine("no connection!");
            connect();
        }

        static void Main(string[] args)
        {
            connect();
            
        }
        static void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            Console.WriteLine("topic : " + e.Topic + " Qos lvl : " + e.QosLevel + " Message : " + Encoding.UTF8.GetString(e.Message));
        }
        
    }
}
