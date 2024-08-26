using System.ComponentModel.DataAnnotations;
using QRGenerator.Persentation.Web.CustomValidation;

namespace QRGenerator.Persentation.Web.Models.QRCode
{
    public class GenerateViewModel
    {
        public string? Username { get; set; }
        public DateTime? Created { get; set; }

        [MinLength(8, ErrorMessage = "Text Barcode must be at least 8 characters long")]
        [MustContainUppercase(ErrorMessage = "Text Barcode must be Uppercase")]
        public string Text { get; set; }
        public string? QRCodeImageBase64 { get; set; }
    }
}
