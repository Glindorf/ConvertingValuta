using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ConvertingValuta;

namespace TcpConverter
{
    class Server
    {
        public static void Start() // håndterer starten af en TcpServer og kommer til at håndtere, når der kommer en ny klient og siger "jeg vil gerne kommunikere med dig"
        {
            // det er vigtigt at have try/catch metoder, man kan godt risikere, at der er noget kommunikation mellem én klient og serveren der crasher, og det skal ikke gå ud over de andre

            try
            {
                TcpListener server;

                int port = 7000;

                server = new TcpListener(IPAddress.Loopback, port); // serveren sættes op - ipaddress.loopback refererer tilbage til localhost - dvs. det er min lokale adresse

                server.Start(); // serveren startes

                Console.WriteLine("Waiting for connection........");

                //fordi ved ved, at den skal være concurrent:

                while (true) // så looper den - venter på en ny Client
                {
                    // "du skal vente på, at der kommer en klient"
                    TcpClient client = server.AcceptTcpClient(); // vi har en variabel der siger, at serveren skal acceptere en TcpClient (vi gemmer den i en variabel, så vi kan sende den ned til vores DoClient)
                    Console.WriteLine("Connected");

                    Task.Run(
                        () => // grunden til at vi sætter en task op er, at den sætter den ud i en ny tråd på processoren
                        {
                            TcpClient tempSocket = client;
                            DoClient(tempSocket);
                        });
                } // så nu har vi lavet en concurrent TcpServer - vi gemmer i en variabel, forespørger på en connection, og sætter task i gang, og hopper tilbage i loopet
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        // Når Client er færdig med ovenstående, ryger den herned:

        public static void DoClient(TcpClient socket) // modtager TcpClient vi kalder socket
        {
            // serverens måde at håndtere kommunikation frem og tilbage fra TcpClient og TcpServer

            Stream ns = socket.GetStream(); // vi tager fat i vores TcpClient og siger "vi skal have fat i din stream" og gemmer det i en variabel, så vi kan tilgå den
            StreamReader sr = new StreamReader(ns); // vi sender vores stream med den (ns)
            StreamWriter sw = new StreamWriter(ns); // 
            sw.AutoFlush = true;

            sw.WriteLine("You are connected"); // vi skriver til StreamWriteren, så vi kan teste, at vi er connected

            while (true) // vi vil gerne holde forbindelsen åben så længe vi har noget at spørge om
            {
                string message = sr.ReadLine(); // vi skal have vores server til at vente på en besked fra TcpClient'en. En ny variabel (message) skal være lig med den besked vi venter at få fra klienten.

                string metode = message.Split(" ")[0];// vi tager fat i den besked vi har fået fra klienten og splitter den på mellemrum, så får vi en liste af strenge ud, der er sepereret på mellemrum. 

                double vaerdi = Convert.ToDouble(message.Split(" ")[1]);

                switch (metode.ToLower()) // vi laver en switch på metoden
                {
                    case "tildanske":
                        double tilDKK = ValutaConverter.TilDanskeKroner(vaerdi);// resultatet vi skal give til klienten gemmer vi i en variabel
                        sw.WriteLine($"{tilDKK} DKK"); // det vi skriver tilbage til klienten
                        break;

                    case "tilsvenske":
                        double tilSEK = ValutaConverter.TilSvenskeKroner(vaerdi);// resultatet vi skal give til klienten gemmer vi i en variabel
                        sw.WriteLine($"{tilSEK} SEK"); // det vi skriver tilbage til klienten
                        break;

                        // i sockettest.exe programmet skriver man metoden efterfulgt af værdien, fx. tildanske 10
                        
                }
            }

        }
    }
}
