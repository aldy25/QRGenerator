using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using QRGenerator.Persentation.Web.Models.QRCode;
using QRGenerator.Persentation.Web.Services;

namespace QRGenerator.Persentation.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QrProcessController : ControllerBase
    {
        private readonly ILogger<QrProcessController> _logger;
        private readonly QRGenerateService _service;

        public QrProcessController(ILogger<QrProcessController> logger, QRGenerateService service)
        {
            _logger = logger;
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> Process([FromBody] QrCodeDataLoad qrCodeData)
        {
            if (qrCodeData == null)
            {
                _logger.LogWarning("No QR code data received.");
                return BadRequest("Invalid data.");
            }

            try
            {
                _logger.LogInformation("Received QR code data: {QrCodeData}", qrCodeData.QrCodeData);

          
                ReaderViewModel data=  new ReaderViewModel { 
                        DataQrcode = qrCodeData.QrCodeData,
                        ScanCreated = DateTime.Now,
                        UsernameScan = User.FindFirst("Username")?.Value
                };

                var dataResponse = _service.getDataBarcode(data);
                return Ok(dataResponse);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing QR code data.");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error.");
            }
        }
    }

    public class QrCodeDataLoad
    {
        public string QrCodeData { get; set; }
    }
}
