using Microsoft.AspNetCore.Mvc;
using System.Text;
using Tesseract;

namespace OcrTesseract.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OcrController : Controller
    {
        private string tessDataPath = @"C:\Users\T60164\Desktop\OcrTesseract\OcrTesseract\OcrTesseract\bin\Debug\net6.0\tessdata";
        
        [HttpGet("extract-text")]
        public ActionResult<string> ExtractTextFromFixedPath()
        {
            
            PdfConvertor pdfConvertor = new PdfConvertor();

            string pdfFilePath=pdfConvertor.pdfFilePath;
            pdfFilePath = @"C:\Users\T60164\Desktop\34 CEL 376  EVRAKLAR 2.pdf";
            string outputFolder = @"C:\Users\T60164\Desktop\yeni";
            List<string> outputFiles = new List<string>();

            pdfConvertor.CreateOutputPngFolder(pdfFilePath, outputFolder);
            //pdfConvertor.pdfFilePath();

            outputFiles= pdfConvertor.ConvertPdfToPng(pdfFilePath, outputFolder);
            // Sabit dosya yolu
            //string imgFilePath = @"C:\Users\T60164\Desktop\34 CEL 376  EVRAKLAR 2_page-0008.jpg";
            try
            {
                // Tesseract motorunu belirtilen tessdata dizini ile başlat
                using var engine = new TesseractEngine(tessDataPath, "spa+ita+tur+eng", EngineMode.Default);

                var sb =new  StringBuilder();

                foreach (var path in outputFiles)
                {
                    // Görüntüyü OCR işlemine tabi tutar
                    using var img = Pix.LoadFromFile(path);
                    using var page = engine.Process(img);

                    // OCR sonucunu alır ve metni döndürür
                   var text =page.GetText() ;
                    sb.AppendLine(text+"\n -----------------------------------------------------------------------------------------");
                    
                    
                    
                }
                string fullText = sb.ToString();



                return Ok(fullText);
            }
            catch (Exception ex)
            {
                // Herhangi bir hata durumunda 500 durumu ile hata mesajı döndürür
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
