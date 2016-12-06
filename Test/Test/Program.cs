using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace MulticastApp
{
    class Program
    {
        static IPAddress address; 
        const int Port = 8001;
        static string localAdress;

        static void Main(string[] args)
        {
            try
            {
                localAdress = GetLocAdress();
                Match mch = Regex.Match(localAdress, @"(?<firstPart>(\d+)\.(\d+)\.)(\d+)\.(\d+)");

                if (!mch.Success)
                    Console.WriteLine("Ошибочный адрес");

                address = IPAddress.Parse(mch.Groups["firstPart"].Value + "255.255");
                new Thread(SendMsg).Start();
                new Thread(ReceiveMsg).Start();                
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Исключение", e);
            }
        }


        private static string GetLocAdress()
        {
            var adressIp = "";
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    adressIp = ip.ToString();
                    break;
                }
            }
            return adressIp;
        }

        private static void SendMsg()
        {
            var sender = new UdpClient(); 
            try
            {
                while (true)
                {
                    var msgIpAdress = String.Format("{0}", localAdress);  
                    var dataMsg = Encoding.Unicode.GetBytes(msgIpAdress);
                    sender.Send(dataMsg, dataMsg.Length, new IPEndPoint(address, Port));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Исключение", e);
                sender.Close();
            }
        }


        private static void ReceiveMsg()
        {
            var receiver = new UdpClient(Port); 
            receiver.JoinMulticastGroup(address, 20);
            IPEndPoint remoteIp = null;
            try
            {
                while (true)
                {
                    var recerveIp = receiver.Receive(ref remoteIp); 

                    if (remoteIp.Address.ToString().Equals(localAdress))
                        continue;

                    var msg = Encoding.Unicode.GetString(recerveIp);
                    Console.WriteLine(msg);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} Исключение", e);
                receiver.Close();
            }
        }

    }
}