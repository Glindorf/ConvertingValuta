using ConvertingValuta;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UdpServerConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            // Det vi gør her, det er, at vi sætter noget server op - siger vi skal have en UdpClient på den her server, vi skal have fat i ting fra det her EndPoint. Altså: Vi sætter UDP-serveren op.

            // Creates an udp Client for reading incoming data
            UdpClient udpServer = new UdpClient(9000);

            //Creates an IPEndPoint to record the IP Address and port number of the sender
            IPAddress ip = IPAddress.Loopback; //Loopback - lyt på "localhost". Hvilken IP-adresse den skal køre på (Localhost)
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(ip, 9000); // Hvilken IP-adresse den skal modtage data fra.
            //IPEndPoint object will allow us to read datagrams sent from another source.

            //Vi bruger en Upp-Client til at sende, emulere en udp-server kan man kalde det, fordi der ikke er noget der hedder en udp-server som sådan, altså der er ikke nogen class i C#, der hedder det. 
            try
            {
                Console.WriteLine("Server is ready"); //Så ved vi at serveren er nået dertil hvor den er klar til at tage imod data

                while (true) //Fordi vi gerne vil have, at man kan blive ved med at kommunikere til den / modtage data
                {
                    //Og så siger vi til den - tag imod de her bytes, på den her port, som vi har sat ovenover.
                    Byte[] receivedBytes = udpServer.Receive(ref RemoteIpEndPoint); // ReceivedBytes vil vi gerne modtage fra den her modtager (RemoteIpEndPoint - fra ovenover)

                    //Så siger vi: "De her bytes vi har modtaget, dem vil vi gerne have oversat til en string.
                    string receivedData = Encoding.ASCII.GetString(receivedBytes); //Det har noget at gøre med den måde Udp sender på, der bliver man nødt til at konvertere tingene, fordi det ikke underforstået nødvendigvis er i bestemt format.

                    Console.WriteLine(receivedData);

                    //I TcpConverter'en har jeg lavet noget, der minder lidt om. Kig i Server.cs.- metode, værdi, switch. Det bruges nærmest ens her:

                    string metode = receivedData.Split(" ")[0]; // split på mellemrum på 0 (1. plads)

                    double vaerdi = Convert.ToDouble(receivedData.Split(" ")[1]); // split på mellemrum på 1 (2. plads)

                    //Vi skal have encoded det tilbage til bytes, så vi kan sende det igen - så vi gøre sådan her:

                    string message = "";

                    switch (metode.ToLower()) // vi laver en switch på metoden
                    {
                        case "tildanske":
                            double tilDKK = ValutaConverter.TilDanskeKroner(vaerdi);

                            message = tilDKK.ToString() + " DKK";
                            break;

                        case "tilsvenske":
                            double tilSEK = ValutaConverter.TilSvenskeKroner(vaerdi);

                            message = tilSEK.ToString() + " SEK";
                            break;

                    }

                    //Så skal vi have encoded det til bytes, så vi siger:

                    Byte[] sendBytes = Encoding.ASCII.GetBytes(message);

                    udpServer.Send(sendBytes, sendBytes.Length, RemoteIpEndPoint);

                    //Ovenstående konvetrer vi "message" tilbage til at være nogle bytes, så fordi når man sender med udp, skal man have det man sender; Bytes, hvad er længden på det man sender, så modtageren ved hvornår alt er modtaget, samt hvem modtageren er. Det er egentlig det der skal til, for at lave Upp-serveren.

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }


        }
    }
}
