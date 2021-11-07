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
        static void Main(string[] args)
        {

            // MqttClient client = new MqttClient("mqtt.eclipseprojects.io",8883,true,null,null,MqttSslProtocols.TLSv1_2);
            mqttClient = new MqttClient(IPAddress.Parse("127.0.0.1"), 1883, false, null, null, MqttSslProtocols.TLSv1_2);

            // register to message received 
            mqttClient.MqttMsgPublishReceived += client_MqttMsgPublishReceived;

            string clientId = Guid.NewGuid().ToString();
            //string username = "";
            //string password = "";
            //username-pass gerekli ise

            // subscribe to the topic "/home/temperature" with QoS 2 
            mqttClient.Subscribe(new string[] { "home" }, new byte[] { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE });

            //mqttClient.Connect(clientId, username, password);
            mqttClient.Connect(clientId);


        }
        static void client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            //int i = BitConverter.ToInt32(e.Topic);
            Console.WriteLine("topic : "+ e.Topic+" Qos lvl : "+e.QosLevel+" Message : "+Encoding.UTF8.GetString(e.Message));
        }
    }
}
