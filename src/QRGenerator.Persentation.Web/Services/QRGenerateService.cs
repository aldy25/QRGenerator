using Microsoft.AspNetCore.Hosting;
using QRCoder;
using QRGenerator.Business.Interface;
using QRGenerator.DataAccess.Models;
using QRGenerator.Persentation.Web.Models.QRCode;
using System.Drawing.Imaging;
using System.Drawing;
using Microsoft.AspNetCore.Mvc;
using Aspose.Pdf.Text;
using Aspose.Pdf;

namespace QRGenerator.Persentation.Web.Services
{
    public  class QRGenerateService
    {
        private   readonly IQRGenerateRepository _repository;
        private  readonly IWebHostEnvironment _webHostEnvironment;

        public QRGenerateService(IQRGenerateRepository repository, IWebHostEnvironment webHostEnvironment)
        {
            _repository = repository;
            _webHostEnvironment = webHostEnvironment;
        }
        private  string GenerateBarcode(string text)
        {
            string file = "";
            using (QRCodeGenerator qrGenerator = new QRCodeGenerator())
            using (QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q))
            using (PngByteQRCode qrCode = new PngByteQRCode(qrCodeData))
            {
                byte[] qrCodeImage = qrCode.GetGraphic(20);
                using (MemoryStream ms = new MemoryStream(qrCodeImage))
                {
                    using (Bitmap bitmap = new Bitmap(ms))
                    {
                        string wwwRootPath = _webHostEnvironment.WebRootPath;
                        string fileName = "qrcode.png";
                        string filePath = Path.Combine(wwwRootPath, "assets", "images", "barcode", fileName);
                        bitmap.Save(filePath, ImageFormat.Png);
                        file = "assets/images/barcode/" + fileName;
                    }
                }
            }

            return file;
        }

        private  string ReverseString(string input)
        {
            char[] charArray = input.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        public  GenerateViewModel SaveBarcode(GenerateViewModel vm)
        {

            Qrcodegenerate qrcodegenerate = new Qrcodegenerate
            {
                Username = vm.Username,
                Created = DateTime.Now,
                Text = vm.Text,
            };

            if (!_repository.Insert(qrcodegenerate)) { throw new Exception("data gagal di insert"); }

            return new GenerateViewModel
            {
                Username = vm.Username,
                Created = qrcodegenerate.Created,
                Text = vm.Text,
                QRCodeImageBase64 = GenerateBarcode(vm.Text)
            };

        }

        public Document DownloadPdf(string imagePath)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;
            string output = imagePath.Replace('/', '\\');

            string filePath = Path.Combine(wwwRootPath, output);
            Console.WriteLine("imagepath : " + imagePath);
            Console.WriteLine("filepath : " + filePath);
            var document = new Document();
            var page = document.Pages.Add();


            double pageWidth = page.PageInfo.Width;  
            double pageHeight = page.PageInfo.Height; 
            double imageWidth = 500;  
            double imageHeight = 500; 
            double xIndent = (pageWidth - imageWidth) / 2;
            double yIndent = (pageHeight - imageHeight) / 2;

            if (System.IO.File.Exists(filePath))
            {


                var image = new ImageStamp(filePath)
                {
                    XIndent = xIndent,
                    YIndent = yIndent,
                    Width = imageWidth,
                    Height = imageHeight

                };
                page.AddStamp(image);
            }
            else
            {
                throw new FileNotFoundException("Image file not found", imagePath);
            }

            return document;
        }

        public ReaderViewModel getDataBarcode(ReaderViewModel dataBarcode)
        {
            var data = _repository.getByDataBarcode(dataBarcode.DataQrcode);
            dataBarcode.UsernameCreated = data.Username;
            dataBarcode.Created = data.Created;
            dataBarcode.ReverseDataQRcode = ReverseString(dataBarcode.DataQrcode);
            return dataBarcode;
        }

        public void AddScanner(ReaderViewModel dataBarcode)
        {
            Qrreader qrReader = new Qrreader
            {
                Username = dataBarcode.UsernameScan,
                Created = DateTime.Now,
                Text = dataBarcode.DataQrcode,
                Remark = dataBarcode.Remark
            };
            if (!_repository.Insert(qrReader)) { throw new Exception("data gagal di insert"); }

        }
    }
}
