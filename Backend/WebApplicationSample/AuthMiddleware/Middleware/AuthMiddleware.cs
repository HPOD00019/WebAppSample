using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthMiddleware.Entities;
using AuthMiddleware.Services;
using Microsoft.AspNetCore.Http;

namespace AuthMiddleware.Middleware
{
    public class AuthMiddleware
    {
        private RequestDelegate _nextDelegate;

        public AuthMiddleware(RequestDelegate nextDelegate)
        {
            _nextDelegate = nextDelegate;
        }
        public async Task InvokeAsync(HttpContext context, AuthService service)
        {
            var isTokenNotNull = GetTokenFromHeader(context.Request, out var token);
            if (!isTokenNotNull)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Token is missing");
                return;
            }
            var auth = await service.ValidateAccessToken(token);
            if (!auth.IsValid)
            {
                context.Response.StatusCode = 401;
                await context.Response.WriteAsync("Invalid access token");
                return; 
            }
            context.Items["UserId"] = auth.UserId;
            
            await _nextDelegate(context);
        }
        private bool GetTokenFromHeader(HttpRequest request, out string token)
        {
            token = null;
            if (!request.Headers.TryGetValue("Authorization", out var authHeader))
            {
                return false;
            }
            var value = authHeader.ToString();
            if (!value.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase)) return false;
            token = value.Substring("Bearer ".Length).Trim();
            return !string.IsNullOrEmpty(token);
        } 
    }
}
