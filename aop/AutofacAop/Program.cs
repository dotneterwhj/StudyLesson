using System;
using Autofac;
using Autofac.Extras.DynamicProxy;
using Newtonsoft.Json;

namespace AutofacAop
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<LogInterceptor>();

            builder.RegisterType<UserService>().As<IUserService>().EnableInterfaceInterceptors();

            var container = builder.Build();

            var userService = container.Resolve<IUserService>();

            var user = userService.GetUser();

            System.Console.WriteLine(JsonConvert.SerializeObject(user));

        }
    }
}
