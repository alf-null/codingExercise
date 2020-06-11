using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Serialization;

namespace restAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Data : ControllerBase
    {
        readonly string XMLDataPath = Path.Combine(Environment.CurrentDirectory, "data.xml");
        private readonly List<Item> SourceData;

        public Data()
        {
            Console.WriteLine("Loading XMLFile");
            XmlSerializer deserializer = new XmlSerializer(typeof(List<Item>), new XmlRootAttribute("items"));
            TextReader reader = new StreamReader(XMLDataPath);
            SourceData = (List<Item>)deserializer.Deserialize(reader);
        }

        [HttpPost]
        public IActionResult Post([FromBody] RequestData vs)
        {

            var GuidsList = SearchGuid(SourceData, vs);
            if (GuidsList.Count < 1)
            {
                Console.WriteLine("Not found in data file, 404 returned to the client");
                return NotFound("The ID's does not match");
            }
            Console.WriteLine("Ok reponse to the client");
            return Ok(GuidsList);
        }

        private List<string> SearchGuid(List<Item> Items, RequestData vs)
        {
            Console.WriteLine("Searching for ID");
            var ReturnList = Items.Where(item => vs.Contains((int)item.Id));
            var Guids = ReturnList.Select(item => item.Guid);
            return Guids.ToList();
        }
    }
}
