using Microsoft.VisualStudio.TestTools.UnitTesting;
using ConvertingValuta;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConvertingValuta.Tests
{
    [TestClass()]
    public class ValutaConverterTests
    {
        [TestMethod()]
        public void TilDanskeKronerTest()
        {
            //arrange - Data vi vil teste på: 
            double svenskeKroner = 10; // Vi tager 10 svenske kroner vi vil konvertere til danske kroner

            //act - Vi refererer til class'en vi vil teste på - her: ValutaConverter.
            double result = ValutaConverter.TilDanskeKroner(svenskeKroner); //  Vi gemmer den i en variabel "result", så vi kan måle om den er korrekt.

            //assert - hvordan skal tingene agere i din kode, hvad er resultatet:
            Assert.AreEqual(7.041, result, 0.001);// Vi tester om de to er ens - vores forventede resultat (de 10 svenske til danske) og det faktiske resultat.
        }

        [TestMethod()]
        public void TilSvenskeKronerTest()
        {
            // arrange
            double danskeKroner = 10;

            //act - vores resultat
            double result = ValutaConverter.TilSvenskeKroner(danskeKroner);

            //assert
            Assert.AreEqual(14.202, result, 0.001); // vi bruger delta til at vide hvor mange betydende cifte (efter komma) vi vil have
        }
    }
}