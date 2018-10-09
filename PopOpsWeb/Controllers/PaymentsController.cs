using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Options;
using PopOps.Model;

namespace PopOpsWeb.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private static DocumentClient _client;

        public PaymentsController(IOptions<DocumentConfig> documentConfig)
        {
            _client = new DocumentClient(documentConfig.Value.ResourceLocation, documentConfig.Value.AuthKey);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Payment>> Get()
        {
            var query = _client.CreateDocumentQuery<Payment>(UriFactory.CreateDocumentCollectionUri("popops", "paymentdetails"));
            return Ok(query);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Payment>> Get(string id)
        {
            var payment = await _client.ReadDocumentAsync<Payment>(UriFactory.CreateDocumentUri("popops", "paymentdetails", id));
            if (payment == null) return NotFound();
            return Ok(payment.Document);
        }
    }
}
