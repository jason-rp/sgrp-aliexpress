using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace SGRP.Aliexpress.Bussiness.Models
{

    public class processingTimeModel
    {
        [JsonProperty(PropertyName = "freight")]
        public List<ProcessingTime> ProcessingTimes { get; set; }
    }


    public class ProcessingTime
    {
        [JsonProperty(PropertyName = "processingTime")]
        public int ProcessingTimeValue { get; set; }

        [JsonProperty(PropertyName = "companyDisplayName")]
        public string CompanyDisplayName { get; set; }

        [JsonProperty(PropertyName = "price")]
        public decimal Price { get; set; }

        [JsonProperty(PropertyName = "commitDay")]
        public int? CommitDay { get; set; }

        [JsonProperty(PropertyName = "isTracked")]
        public bool IsTracked { get; set; }


    }

    public class ShippingContentResultModel
    {
        public int ProcessingTime { get; set; }
        public string ShippingCompany { get; set; }

        public decimal? ShippingFee { get; set; }

        public int? OnTimeDelivery { get; set; }

        public decimal SubTotalPrice { get; set; }

        public string Shipping1 { get; set; }
        public string Shipping2 { get; set; }
        public string Shipping3 { get; set; }
    }
}
