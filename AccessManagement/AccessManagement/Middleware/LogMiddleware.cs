using AccessManagementServices.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AccessManagement.Middleware
{
    public class LogMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LogMiddleware> _logger;
        public LogMiddleware(RequestDelegate next, ILogger<LogMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            var requestPath = context.Request.Path.ToString();
            var requestMethod = context.Request.Method.ToString();
            var requestHost = context.Request.Host.ToString();
            var requestHeaders = context.Request.Headers;
            //_logger.LogInformation("Request Path:" + requestPath + Environment.NewLine
            //    + " requestMethod:" + requestMethod + " requestHost:" + requestHost + Environment.NewLine
            //    + " requestHeaders:" + requestHeaders);
            if (requestPath.Contains("api"))
            {
                _logger.LogInformation("Request Path:" + requestHost + requestPath + " requestMethod:" + requestMethod
                + " requestHeaders User-Agent:" + requestHeaders["User-Agent"]);
                if (requestMethod == "POST")
                {
                    var requestBody = await ReadBodyAsync(context.Request);
                    _logger.LogInformation("Request Body:" + requestBody);
                }
                this.EnableReadAsync(context.Response);

                context.Response.OnCompleted(async o =>
                {
                    var c = o as HttpContext;
                    if (c != null)
                    {
                        var retStr = await this.ReadBodyAsync(c.Response).ConfigureAwait(false);
                        _logger.LogInformation("Response Path:" + requestHost + requestPath + Environment.NewLine
                            + "Body:" + retStr);
                    }
                }, context);
            }
            
            await _next.Invoke(context);
            
        }

        private async Task<string> ReadBodyAsync(HttpRequest request)
        {
            if (request.ContentLength > 0)
            {
                await EnableRewindAsync(request).ConfigureAwait(false);
                var encoding = GetEncoding(request.ContentType);
                return await this.ReadStreamAsync(request.Body, encoding).ConfigureAwait(false);
            }
            return null;
        }

        //private Encoding GetRequestEncoding(HttpRequest request)
        //{
        //    var requestContentType = request.ContentType;
        //    var requestMediaType = requestContentType == null ? default(MediaType) : new MediaType(requestContentType);
        //    var requestEncoding = requestMediaType.Encoding;
        //    if (requestEncoding == null)
        //    {
        //        requestEncoding = Encoding.UTF8;
        //    }
        //    return requestEncoding;
        //}

        private async Task EnableRewindAsync(HttpRequest request)
        {
            if (!request.Body.CanSeek)
            {
                request.EnableBuffering();

                await request.Body.DrainAsync(CancellationToken.None);
                request.Body.Seek(0L, SeekOrigin.Begin);
            }
        }

        private async Task<string> ReadStreamAsync(Stream stream, Encoding encoding)
        {
            using (StreamReader sr = new StreamReader(stream, encoding, true, 1024, true))//这里注意Body部分不能随StreamReader一起释放
            {
                var str = await sr.ReadToEndAsync();
                stream.Seek(0, SeekOrigin.Begin);//内容读取完成后需要将当前位置初始化，否则后面的InputFormatter会无法读取
                return str;
            }
        }

        private async Task<string> ReadBodyAsync(HttpResponse response)
        {
            if (response.Body.Length > 0)
            {
                //var position = response.Body.Position;
                response.Body.Seek(0, SeekOrigin.Begin);
                var encoding = this.GetEncoding(response.ContentType);
                var retStr = await ReadStreamAsync(response.Body, encoding, false).ConfigureAwait(false);
                //response.Body.Position = position;
                //读取完成后再重新赋值位置这个过程可能不需要，因为数据流是只写的
                return retStr;
            }
            return null;
        }

        private Encoding GetEncoding(string contentType)
        {
            var mediaType = contentType == null ? default(MediaType) : new MediaType(contentType);
            var encoding = mediaType.Encoding;
            if (encoding == null)
            {
                encoding = Encoding.UTF8;
            }
            return encoding;
        }

        private void EnableReadAsync(HttpResponse response)
        {
            if (!response.Body.CanRead || !response.Body.CanSeek)
            {
                response.Body = new MemoryWrappedHttpResponseStream(response.Body);
            }
        }

        private async Task<string> ReadStreamAsync(Stream stream, Encoding encoding, bool forceSeekBeginZero = true)
        {
            using (StreamReader sr = new StreamReader(stream, encoding, true, 1024, true))//这里注意Body部分不能随StreamReader一起释放
            {
                var str = await sr.ReadToEndAsync();
                if (forceSeekBeginZero)
                {
                    stream.Seek(0, SeekOrigin.Begin);//内容读取完成后需要将当前位置初始化，否则后面的InputFormatter会无法读取
                }
                return str;
            }
        }
    }
}
