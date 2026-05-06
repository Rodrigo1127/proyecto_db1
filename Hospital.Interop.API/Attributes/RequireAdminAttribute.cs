using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Hospital.Interop.API.Attributes
{
    /// <summary>
    /// Atributo para determinar si el usuario es administrador
    /// basado en un header X-Admin-Key
    /// </summary>
    public class RequireAdminAttribute : Attribute, IAsyncAuthorizationFilter
    {
        private readonly string _expectedKey;

        public RequireAdminAttribute(string expectedKey = "admin-secret-key")
        {
            _expectedKey = expectedKey;
        }

        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var request = context.HttpContext.Request;

            if (!request.Headers.TryGetValue("X-Admin-Key", out var adminKey) || 
                adminKey != _expectedKey)
            {
                context.Result = new UnauthorizedResult();
            }

            await Task.CompletedTask;
        }
    }
}
