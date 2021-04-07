using System;

namespace ConvertingValuta
{
    public static class ValutaConverter
    {
        public static double TilSvenskeKroner(double danskeKroner)
        {
            double result = danskeKroner / 0.7041;
            return result;

            //ovenstående to linjer kan også skrives således:
            //return danskeKroner / 0.7041;
        }

        public static double TilDanskeKroner(double svenskeKroner)
        {
            double result = svenskeKroner * 0.7041;
            return result;

            //ovenstående to linjer kan også skrives således:
            //return svenskeKroner * 0.7041;
        }
    }
}
