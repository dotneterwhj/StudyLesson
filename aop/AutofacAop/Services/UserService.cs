using Autofac.Extras.DynamicProxy;
using AutofacAop;

namespace Autofac
{
    [Intercept(typeof(LogInterceptor))]
    [Intercept(typeof(LogInterceptor))]
    public class UserService : IUserService
    {
        public User GetUser()
        {
            return new User { Id = 1, Name = "nextload" };
        }
    }
}