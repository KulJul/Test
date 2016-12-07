using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Linq;
using System.Collections.Generic;

namespace MulticastApp
{
    class Program
    {
        static IPAddress address;
        static Random rand = new Random();
        static int Port = rand.Next(7500, 8500);
        static string localAdress;
        static Dictionary<int, string> dicIpNeighbors = new Dictionary<int, string>();

        static void Main(string[] args)
        {
            try
            {
                localAdress = GetLocAdress();
                address = IPAddress.Parse("235.5.5.11");
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
                    var msgIpAdress = String.Format("{0}/{1}", localAdress, localAdress.GetHashCode());  
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

                   // if (remoteIp.Address.ToString().Equals(localAdress))
                      //  continue;

                    var msg = Encoding.Unicode.GetString(recerveIp);

                    dicIpNeighbors.Add(1, msg);

                    foreach (var c in dicIpNeighbors)
                        Console.WriteLine(c);
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