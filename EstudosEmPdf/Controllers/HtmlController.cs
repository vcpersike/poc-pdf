using Microsoft.AspNetCore.Mvc;
using EstudosEmPdf.Services;

namespace EstudosEmPdf.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HtmlController : ControllerBase
    {
        private readonly HtmlService _htmlService;
        private readonly IWebHostEnvironment _env;

        public HtmlController(HtmlService htmlService, IWebHostEnvironment env)
        {
            _htmlService = htmlService;
            _env = env;
        }

        // Endpoint para visualizar com CSS externo
        [HttpGet("styled/{id}")]
        public ContentResult RenderStyledHtml(string id)
        {
            var html = _htmlService.RenderHtmlWithExternalCss(id);
            return Content(html, "text/html");
        }

        // Endpoint para gerar PDF a partir do HTML renderizado
        [HttpGet("gerar-pdf/{id}")]
        public IActionResult GerarPdf(string id)
        {
            var html = _htmlService.RenderHtmlWithExternalCss(id);
            _htmlService.GerarPdfViaNode(html);

            var pdfPath = Path.Combine(_env.WebRootPath, "output", "saida.pdf");
            var fileBytes = System.IO.File.ReadAllBytes(pdfPath);
            return File(fileBytes, "application/pdf", "proposta.pdf");
        }
    }
}
