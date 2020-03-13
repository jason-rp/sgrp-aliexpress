using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace SGRP.Aliexpress.Bussiness.Models
{
    public class StoreRatingDetailModel
    {
        [JsonProperty(PropertyName = "desc")] 
        public StoreRatingDesc StoreRatingDesc { get; set; }

        [JsonProperty(PropertyName = "seller")]
        public StoreRatingSeller StoreRatingSeller { get; set; }

        [JsonProperty(PropertyName = "shipping")]
        public StoreRatingShipping StoreRatingShipping { get; set; }
    }

    public class StoreRatingDesc
    {
        [JsonProperty(PropertyName = "score")]
        public decimal Score { get; set; }

        [JsonProperty(PropertyName = "ratings")]
        public decimal Rating { get; set; }

        [JsonProperty(PropertyName = "percent")]
        public decimal Percent { get; set; }
    }

    public class StoreRatingSeller
    {
        [JsonProperty(PropertyName = "score")]
        public decimal Score { get; set; }

        [JsonProperty(PropertyName = "ratings")]
        public decimal Rating { get; set; }

        [JsonProperty(PropertyName = "percent")]
        public decimal Percent { get; set; }
    }

    public class StoreRatingShipping
    {
        [JsonProperty(PropertyName = "score")]
        public decimal Score { get; set; }

        [JsonProperty(PropertyName = "ratings")]
        public decimal Rating { get; set; }

        [JsonProperty(PropertyName = "percent")]
        public decimal Percent { get; set; }
    }
}
