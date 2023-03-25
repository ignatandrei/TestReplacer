using GenerateContractObjects;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GenerateContractWebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class DocReplacer : ControllerBase
    {
        private readonly ILogger<DocReplacer> _logger;
        private readonly DocumentList dl;

        public DocReplacer(ILogger<DocReplacer> logger, DocumentList dl)
        {
            _logger = logger;
            this.dl = dl;
        }

        [HttpGet("{fileName}")]
        public async Task<string[]> ReplacerNames(string fileName)
        {
            var doc = dl.FindDocument(fileName);
            if(doc == null || doc.Length != 1)
                throw new ArgumentException("more than 1");

            var selected = doc.First();
            await selected.Initialize();
            return selected.Replacements();
        }
        [HttpPost("")]
        public async Task<IActionResult> ReplaceData([FromQuery]string fileName, [FromBody]Tuple<string, string>[]? values)
        {
            if (values == null ||  values.Length == 0)
            {
                return null;
            }
            var doc = dl.FindDocument(fileName);
            if (doc == null || doc.Length != 1)
                throw new ArgumentException("more than 1");

            var selected = doc.First();
            await selected.Initialize();
            foreach (var item in values)
            {
                selected.AddTextToReplace(item.Item1, item.Item2);
            }
            var ms=await selected.Replace();
            return File(ms, "application/octet-stream", selected.Location.Substring(dl.location.Length+1));

            
        }
    }
}
