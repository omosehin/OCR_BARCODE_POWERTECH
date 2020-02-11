using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Manual_Validation.Models
{
    public class MeterReading
    {
        public long Id { get; set; }
      //  public string  WebId  { get; set; }
        public string MeterNumber { get; set; }
        public string EnergyConsumption { get; set; }
       

    }
}