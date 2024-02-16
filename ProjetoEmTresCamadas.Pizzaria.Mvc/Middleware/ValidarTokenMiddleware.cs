using ProjetoEmTresCamadas.Pizzaria.Mvc.Controllers;
using System.Globalization;
using System.Net;
using System.Net.NetworkInformation;

namespace ProjetoEmTresCamadas.Pizzaria.Mvc.Middleware
{
    public class ValidarTokenMiddleware
    {
        private readonly RequestDelegate _next;

        public ValidarTokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.User.Identity.IsAuthenticated)
            {
                context.Request.Cookies.TryGetValue(LoginController.TOKEN_KEY, out var token);


                if (token == null)
                {
                    //Chamar api de autenticação para validar token
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    await context.Response.WriteAsync("Token invalido");
                    return;
                }
            }
            // Call the next delegate/middleware in the pipeline.
            await _next(context);
        }
    }
}
