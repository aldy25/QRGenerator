namespace QRGenerator.Persentation.Web.Models.QRCode
{
    public class ReaderViewModel
    {

        public string DataQrcode { get; set; }
        public string ReverseDataQRcode { get; set; }
        public string UsernameCreated { get; set; }
        public string? UsernameScan { get; set; }
        public DateTime? ScanCreated { get; set; }
        public DateTime? Created { get; set; }

        public string Remark { get; set; }

    }
}
