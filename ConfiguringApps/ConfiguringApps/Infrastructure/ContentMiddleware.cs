using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace ConfiguringApps.Infrastructure
{
    public class ContentMiddleware
    {
        private readonly RequestDelegate _nextDelegate;
        private readonly UptimeService _uptime;

        public ContentMiddleware(RequestDelegate next, UptimeService up)
        {
            _nextDelegate = next;
            _uptime = up;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Path.ToString().ToLower() == "/middleware")
            {
                await httpContext.Response.WriteAsync("this is the from the content middleware" + $"{_uptime.Uptime}ms", Encoding.UTF8);
            }
            else
            {
                await _nextDelegate.Invoke(httpContext);
            }
        }
    }
}
