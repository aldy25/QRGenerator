using QRGenerator.Business.Interface;

namespace QRGenerator.Persentation.Web.Services
{
    public class HomeSerivce
    {
        private readonly IUserRepository _userRepository;
        
        public HomeSerivce(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }


    }
}
