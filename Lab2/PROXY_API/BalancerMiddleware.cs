using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net.Http;
using System.Net;

namespace PAD
{
    public class BalancerMiddleWare
    {
        public BalancerMiddleWare(RequestDelegate next)
        {

        }
        public async Task Invoke(HttpContext context, IRedisManager redisManager)
        {
            try
            {

                var requestMessage = new HttpRequestMessage();
                var requestMethod = context.Request.Method;

                if (!HttpMethods.IsGet(requestMethod) && !HttpMethods.IsHead(requestMethod) && !HttpMethods.IsDelete(requestMethod) && !HttpMethods.IsTrace(requestMethod))
                {
                    var streamContent = new StreamContent(context.Request.Body);
                    requestMessage.Content = streamContent;
                }

                // All request headers and cookies must be transferend to remote server. Some headers will be skipped
                foreach (var header in context.Request.Headers)
                {
                    if (!requestMessage.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray()) && requestMessage.Content != null)
                    {
                        requestMessage.Content?.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
                    }
                }

                string uriString = GetUri(context, redisManager);

                //recreate remote url
                requestMessage.RequestUri = new Uri(uriString);
                requestMessage.Method = new HttpMethod(context.Request.Method);
                var responseMessage = await redisManager.GetConnection().SendAsync(requestMessage);


                context.Response.StatusCode = (int)responseMessage.StatusCode;
                foreach (var header in responseMessage.Headers)
                {
                    context.Response.Headers[header.Key] = header.Value.ToArray();
                }

                foreach (var header in responseMessage.Content.Headers)
                {
                    context.Response.Headers[header.Key] = header.Value.ToArray();
                }


                //tell to the browser that response is not chunked
                //context.Response.Headers.Remove("transfer-encoding");
                await responseMessage.Content.CopyToAsync(context.Response.Body);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

        }

        private static string GetUri(HttpContext context, IRedisManager redisManager)
        {
            return $"{redisManager.GetBaseAddress()}/{context.Request.PathBase}{context.Request.Path}{context.Request.QueryString}";
        }
    }

    public static class BalancerMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestBalancer(
            this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<BalancerMiddleWare>();
        }
    }
}