using QRGenerator.Business.Interface;
using QRGenerator.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRGenerator.Business.Repositories
{
    public class QRGenerateRepository : IQRGenerateRepository
    {
        private readonly QrgeneratorContext _dbContext;

        public QRGenerateRepository(QrgeneratorContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Insert(Qrcodegenerate qrcodegenerate)
        {
            _dbContext.Qrcodegenerates.Add(qrcodegenerate);
            var ArrowAffacted = _dbContext.SaveChanges();
            return ArrowAffacted > 0 ? true : false;
        }
        public bool Insert(Qrreader qrreader)
        {
            _dbContext.Qrreaders.Add(qrreader);
            var ArrowAffacted = _dbContext.SaveChanges();
            return ArrowAffacted > 0 ? true : false;
        }
        public Qrcodegenerate getByDataBarcode(string dataBarcode)
        {
            return _dbContext.Qrcodegenerates.Where(u => u.Text == dataBarcode).FirstOrDefault();
        }
    }
}
