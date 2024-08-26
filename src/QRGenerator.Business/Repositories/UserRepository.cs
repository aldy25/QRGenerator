using QRGenerator.Business.Interface;
using QRGenerator.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRGenerator.Business.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly QrgeneratorContext _dbContext;

        public UserRepository(QrgeneratorContext dbContext)
        {
            _dbContext = dbContext;
        }

        public User GetAccountByUsername(string username)
        {
            return _dbContext.Users.Where(u => u.Username == username).FirstOrDefault();
        }
    }
}
