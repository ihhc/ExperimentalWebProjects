using DocumentsCore;
using Microsoft.AspNetCore.Mvc;

namespace DocumentsWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DocumentsController : ControllerBase
    {
        private readonly IDocumentsServices _documentServices;
        public DocumentsController(IDocumentsServices documentServices)
        {
            _documentServices = documentServices;
        }

        [HttpGet]
        public IActionResult GetDocuments()
        {
            return Ok(_documentServices.GetDocuments());
        }

        [HttpGet("{id}", Name ="GetDocuments")]
        public IActionResult GetDocument(string id)
        {
            return Ok(_documentServices.GetDocument(id));
        }

        [HttpPost]
        public IActionResult AddDocument(Document document)
        {

            _documentServices.AddDocument(document);
            return CreatedAtRoute("GetDocument", new { id = document.Id }, document);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteDocument(string id)
        {
            _documentServices.DeleteDocument(id);
            return NoContent();
        }

        [HttpPut]
        public IActionResult UpdateDocument(Document document)
        {
            return Ok(_documentServices.UpdateDocument(document));
        }
    }
}
