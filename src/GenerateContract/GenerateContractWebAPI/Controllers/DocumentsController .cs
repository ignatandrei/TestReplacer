using GenerateContractObjects;
using Microsoft.AspNetCore.Mvc;

namespace GenerateContractWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class DocumentsController : ControllerBase
    {

        private readonly ILogger<DocumentsController> _logger;
        private readonly DocumentList dl;

        public DocumentsController(ILogger<DocumentsController> logger, DocumentList dl)
        {
            _logger = logger;
            this.dl = dl;
        }

        [HttpGet]
        public string[]? ContractNames()
        {
            return dl.FindDocuments();
        }

    }
}