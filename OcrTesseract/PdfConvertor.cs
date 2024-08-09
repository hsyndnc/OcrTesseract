using Tesseract;
using System.Drawing;
using System.Drawing.Imaging;
using PdfiumViewer;


namespace OcrTesseract
{
    public class PdfConvertor
    {
        public string outputFilePath { get; set; }
        public void CreateOutputPngFolder(string pdfFilePath, string outputFolder)
        {
            // PDF dosyasının adını ve uzantısını ayırıyoruz
            string pdfFileName = Path.GetFileNameWithoutExtension(pdfFilePath);
            string pdfFileExtension = Path.GetExtension(pdfFilePath);

            //Geçerli saat ve tarih bilgilerini al
            string dateTimeStamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
            
           // 
            string pngFolderName = $"{pdfFileName}_{dateTimeStamp}{pdfFileExtension}";

            //Tam dosya yolu
            string outputFilePath = Path.Combine(outputFolder, pngFolderName);

            Directory.CreateDirectory(outputFilePath);


        }

        

        public String pdfFilePath  { get; set; }
        public String outputFolder { get; set; }

        // PDF dosyasını PNG'ye dönüştüren metot
        //String listesi dönen bir metot
        public List<string> ConvertPdfToPng(string pdfFilePath, string outputFolder)
        {
            List<string> imagePaths = new List<string>();
            //pdfFilePath = @"\C:\\Users\\T60164\\Desktop\\34 CEL 376  EVRAKLAR 2.pdf\";

            // Çıktı klasörünün var olup olmadığını kontrol et, yoksa oluştur
            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }
            
            using var document = PdfDocument.Load(pdfFilePath);

            // Her sayfa için işlem yap
            for (int i = 0; i < document.PageCount; i++)
            {
                // PDF sayfasını bitmap olarak render et
                using var image = document.Render(i, 300, 300, PdfRenderFlags.CorrectFromDpi);

                // PNG dosyasının yolunu oluştur
                //Path.Combine ise verilen yol parçalarını birleştirerek geçerli dosya yolu oluşturur.
                //Path.GetFileNameWithoutExtension(pdfFilePath) bu metot file path uzantısız olarak dosya adını ayıklamaya yarar.
                //c:Users\\.......\\....\\example.pdf pathini alarak sadece example döndürmeye yarar.

                string imagePath = Path.Combine(outputFilePath, $"{Path.GetFileNameWithoutExtension(pdfFilePath)}_Page_{i + 1}.png");

                
               
                // Görüntüyü PNG dosyasına kaydet
                image.Save(imagePath, System.Drawing.Imaging.ImageFormat.Png);

                // Kaydedilen dosyanın yolunu listeye ekle
                imagePaths.Add(imagePath);
            }

            // Kaydedilen PNG dosyalarının yollarını döndür
            return imagePaths;
        }
    }
}

