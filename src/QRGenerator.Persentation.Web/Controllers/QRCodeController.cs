using Microsoft.AspNetCore.Mvc;
using QRCoder;
using QRGenerator.Persentation.Web.Models.QRCode;
using System.Drawing;
using System.Drawing.Imaging;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Security.Claims;
using QRGenerator.Persentation.Web.Services;

namespace QRGenerator.Persentation.Web.Controllers
{
    [Route("[controller]")]
    public class QRCodeController : Controller
    {
        private readonly QRGenerateService _service;

        public QRCodeController(QRGenerateService service)
        {
            _service = service;
        }

        [HttpGet("Generate")]
        public IActionResult Generate()
        {
            var obj = new GenerateViewModel();
            return View("Generate",obj);
        }

        [HttpPost("Generate")]
        public IActionResult Generate(GenerateViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("Generate", vm);
                }
                else
                {
                    vm.Username = User.FindFirst("Username")?.Value;
                    var response = _service.SaveBarcode(vm);
                    return View("Generate", response);
                }
            }
            catch (Exception ex) 
            {
                ViewBag.ErrorMessage = ex.Message;
                return View("Generate",vm);
            }
           
        }



        public IActionResult DownloadPDF(string imagePath)
        {
            try
            {
                var document = _service.DownloadPdf(imagePath);

                using (var stream = new MemoryStream())
                {
                    document.Save(stream);
                    var pdfBytes = stream.ToArray();

                    return File(pdfBytes, "application/pdf", "Barcode.pdf");
                }
            }
            catch (FileNotFoundException ex)
            {
                return BadRequest(new { message = $"File tidak ditemukan: {ex.Message}" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Terjadi kesalahan: {ex.Message}" });
            }
        }

        [HttpGet("Scan")]
        public IActionResult Scan()
        {
            return View("Scan");

        }

        [HttpPost("SaveScan")]
        public IActionResult SaveScan([FromBody] ReaderViewModel vm)
        {
            try
            {
                _service.AddScanner(vm);
                return Json(new {Status = 1});
            }   
            catch (Exception ex)
            {
                return Json(new { Status = 0 });
            }
           
        }
    }
}
