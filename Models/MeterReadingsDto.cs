using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Manual_Validation.Models
{
    public class MeterReadingsDto
    {

        //[Required]
        //[MaxLength(20), MinLength(4)]
        //public string MeterNumber { get; set; }
        public string ImageBase64 { get; set; }
        public string OcrEngine { get; set; } = "2"; //2 is to pass as a param to OCRSpace engine to get a better result
        


    }
}