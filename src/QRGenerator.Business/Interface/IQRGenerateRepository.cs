using QRGenerator.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRGenerator.Business.Interface
{
    public  interface IQRGenerateRepository
    {
        public  bool Insert(Qrcodegenerate qrcodegenerate);
        public bool Insert(Qrreader qrreader);

        public Qrcodegenerate getByDataBarcode(string dataBarcode);
    }
}
