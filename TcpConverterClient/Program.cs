using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace TcpConverterClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Awaiting connection");

            Console.ReadKey(); // Vi laver en Console Readkey, fordi vi vil gerne have, at den først connecter til TcpConverter-serveren, når serveren er oppe at køre.

            TcpClient client = new TcpClient(IPAddress.Loopback.ToString(), 7000); // her ville man også kunne bruge "localhost". Vi bruger port 7000 da det er den samme serveren kører på. Her laver vi en ny Tcp Client.

            Stream ns = client.GetStream(); // den skal også have en stream, så vi tager fat i den Tcp Client vi har lavet og siger "GetStream".
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true;

            while (true)
            {
                string message = sr.ReadLine(); // vi ved at det færste der kommer ud fra vores server er "You are connected"

                Console.WriteLine(message); // vi vil gerne have beskeden fra serveren ud i konsollen

                string request = Console.ReadLine(); // vi vil gerne skrive til TcpServeren - fx "tildanske 50" eller "tilsvenske 50"

                sw.WriteLine(request);
            }

            // det er hvad der skal til for at få en tcp klient op at køre.
            // Når de skal startes, skal begge projekter startes. 
            //Højreklik på Solution - filen og vælg ”Properties” (i bunden).
            //Vælg ”Startup Project” og ”Multiple startup projects” og vælg “Start” ved client og server.
            //I TcpConverterClient kan man nu skrive i konsollen ”tildanske 50” fx


        }
    }
}
