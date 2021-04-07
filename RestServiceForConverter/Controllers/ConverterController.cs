using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConvertingValuta;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestServiceForConverter.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ConverterController : ControllerBase
    { // opgaven lyder på, at lave to metoder svarende til de to metoder vi har i vores class library - derfor vælger vi at lave to Get-metoder, som kan konvertere valuta. Det giver ikke mening med put, post, delete.
        // GET: api/<ConverterController>
        [HttpGet]
        [Route("/TilDanskeKroner/{svenskeKroner}")] // vigtigt vi har styr på vores routing. Dvs den måde vores service er bygget op på. Det er vigtigt vi differentierer mellem de to forskellige services vi har. Den ligger som en "under-route" til vores Converter - det er en del af URL'en.
        public double TilDanskeKroner(double svenskeKroner) // vi ved at returtypen kommer til at være en double. Vi kalder den TilDanske (tidl. Get())
        {
            return ValutaConverter.TilDanskeKroner(svenskeKroner); // vi sender de svenske kroner
        }

        // GET api/<ConverterController>/5
        [HttpGet]
        [Route("/TilSvenskeKroner/{danskeKroner}")] // grunde til at vi skriver {} i Route er, at Frameworket bagved så forstår at det er dynamisk - altså er den kan skifte værdi, så vi kan kalde forskellige værdier fra vores view
        public double Get(double danskeKroner)
        {
            return ValutaConverter.TilSvenskeKroner(danskeKroner);
        }

        // POST api/<ConverterController>
        

        // PUT api/<ConverterController>/5
        

        // DELETE api/<ConverterController>/5
        
    }
}
