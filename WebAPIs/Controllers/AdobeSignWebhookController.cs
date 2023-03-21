using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace WebAPIs.Controllers
{
    public class AdobeSignWebhookController : ApiController
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(AdobeSignWebhookController));
        [HttpPost]
        public async Task<IHttpActionResult> Post()
        {
            // Read the request body
            string requestBody;
            using (var reader = new StreamReader(HttpContext.Current.Request.InputStream))
            {
                requestBody = await reader.ReadToEndAsync();
            }
            _logger.Info($"RequestBody: {requestBody}");
            return Ok();
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            // Check if the X-AdobeSign-ClientId header exists in the request
            string headerClientId = HttpContext.Current.Request.Headers["X-AdobeSign-ClientId"];
            _logger.Info("Get Call - Test");
            if (!string.IsNullOrEmpty(headerClientId))
            {
                // Return the header value in the response header
                HttpContext.Current.Response.Headers.Add("X-AdobeSign-ClientId", headerClientId);
                return Ok();
            }
            var message = "The X-AdobeSign-ClientId header or the xAdobeSignClientId key in the JSON request body does not match the client ID sent in the request.";
            _logger.Warn(message);
            return BadRequest(message);
        }
    }
}
