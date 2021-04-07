using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UdpClientConverter
{
    class Program // Her laver vi en UdpClient - det er ikke en del af eksamensopgaven, det er for at øve!
    {
        static void Main(string[] args)
        {
            UdpClient udpClient = new UdpClient(IPAddress.Loopback.ToString(), 9000); // Broadcast, fordi så sender vi det til alle modtagere, der ligger på den her port

            IPAddress ip = IPAddress.Loopback; //Loopback - lyt på "localhost". Hvilken IP-adresse den skal køre på (Localhost)
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(ip, 9000); // Hvilken IP-adresse den skal modtage data fra.

            while (true)
            {
                string message = Console.ReadLine(); // Fordi den vil vi gerne vise

                // Så vil vi gerne have et ByteArray, fordi den skal konverteres til bytes
                Byte[] sendBytes = Encoding.ASCII.GetBytes(message);

                udpClient.Send(sendBytes, sendBytes.Length);

                // Vi skal så have et svar tilbage fra serveren - og stjæler noget fra vores UdpServerConverter-program - indsat på linje 14-15, samt herunder:

                Byte[] receivedBytes = udpClient.Receive(ref RemoteIpEndPoint); // ReceivedBytes vil vi gerne modtage fra den her modtager (RemoteIpEndPoint - fra ovenover)

                string receivedData = Encoding.ASCII.GetString(receivedBytes); //Det har noget at gøre med den måde Udp sender på, der bliver man nødt til at konvertere tingene, fordi det ikke underforstået nødvendigvis er i bestemt format.

                Console.WriteLine(receivedData);




            }


        }
    }
}
