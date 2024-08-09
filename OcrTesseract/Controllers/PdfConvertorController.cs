using Microsoft.AspNetCore.Mvc;

namespace OcrTesseract.Controllers
{
    public class PdfConvertorController : Controller
    {
        public IActionResult Index()
        {

            return View();
        }
    }
}
