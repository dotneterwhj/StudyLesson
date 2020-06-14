using Castle.DynamicProxy;

namespace AutofacAop
{
    public class LogInterceptor : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            var methodName = invocation.Method.Name;

            System.Console.WriteLine($"{methodName} 方法执行前");

            invocation.Proceed();

            System.Console.WriteLine($"{methodName} 方法执行后");
        }
    }
}