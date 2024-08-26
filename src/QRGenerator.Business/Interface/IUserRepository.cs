using QRGenerator.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRGenerator.Business.Interface
{
    public interface IUserRepository
    {

        public User GetAccountByUsername(string username);
    }
}
