using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace SGRP.Aliexpress.Bussiness.Models
{

    public class ProcessingTimeModel
    {
        [JsonProperty(PropertyName = "freight")]
        public List<ProcessingTime> ProcessingTimes { get; set; }
    }


    public class ProcessingTime
    {
        public int processingTime { get; set; }
    }
}
