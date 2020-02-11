using Manual_Validation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using IronOcr;
using System.Drawing;
using System.IO;
using System.Web;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using System.Configuration;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Text;
using System.Text.RegularExpressions;

namespace Manual_Validation.Controllers
{
   
    public class ReadingsController : ApiController
    {
        private static readonly string _oCRSpaceApi = ConfigurationManager.AppSettings["OCRSpaceApi"];
        private static readonly string _oCRSpaceApiKey = ConfigurationManager.AppSettings["OCRSpaceApiKey"];
        private static readonly HttpClient _client = new HttpClient();

        [HttpPost]
        public async Task<HttpResponseMessage> AddReadings()
        {
            
            var res = HttpContext.Current;
            var httpContext = HttpContext.Current.Request;
            var OCREngine = httpContext.Params["OCREngine"];
            var ImageBase64 = httpContext.Params["ImageBase64"];
            var MeterNumber = httpContext.Params["MeterNumber"];

            MultipartFormDataContent multipartForm = new MultipartFormDataContent();

            multipartForm.Add(new StringContent(ImageBase64), "Base64Image");
            multipartForm.Add(new StringContent(OCREngine), "OcrEngine");
            
            _client.DefaultRequestHeaders.Add("apikey", _oCRSpaceApiKey);
          
            var response = await _client.PostAsync(_oCRSpaceApi, multipartForm);

            string strContent = await response.Content.ReadAsStringAsync();

            Rootobject ocrResult = JsonConvert.DeserializeObject<Rootobject>(strContent);

            StringBuilder txtResult = new StringBuilder();
            StringBuilder errResult = new StringBuilder();
            var validateScannedConsumptionenergy = new Regex(@"^[0-9]*$"); //regex to validate only numbers

            if (ocrResult.OCRExitCode == 1)
            {
                for (int i = 0; i < ocrResult.ParsedResults.Count(); i++)
                {
                    var scannedText = ocrResult.ParsedResults[i].ParsedText;
                    if (!validateScannedConsumptionenergy.IsMatch(scannedText))
                    {
                        HttpResponseMessage errResponse = Request.CreateResponse(HttpStatusCode.BadRequest, "Energy consumption should only be scan numbers");
                        return errResponse;
                    }
                    if (scannedText == "")
                    {
                        HttpResponseMessage emptyScanErrResponse = Request.CreateResponse(HttpStatusCode.BadRequest, "Energy consumption is not scanned very well");
                        return emptyScanErrResponse;
                    }

                    txtResult.Append(scannedText);
                }
            }
            else
            {
                errResult.Append("ERROR: " + strContent);
            }


            var MeterReadings = new MeterReading
            {
                MeterNumber = MeterNumber,
                EnergyConsumption =Convert.ToString(txtResult),
              //  WebId = Guid.NewGuid().ToString().Replace('-', '0').ToUpper()
            };
            using var _context = new DataContext();
            _context.MeterReadings.Add(MeterReadings);
            var save = _context.SaveChanges() > 1;
            HttpResponseMessage myresponse = Request.CreateResponse(HttpStatusCode.OK,"Successfully saved");
            return myresponse;

            throw new Exception("Bad Request");
        }
    }
}