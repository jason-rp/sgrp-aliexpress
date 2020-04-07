using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace SGRP.Aliexpress.Bussiness.ViewModel
{
    public class FirstPhaseDataModel
    {
        [JsonProperty(PropertyName = "isFistPhase")]
        public bool IsFirstPhase { get; set; }

        [JsonProperty(PropertyName = "data")]
        public List<FirstPhaseUrlModel> FirstPhaseUrlModels { get; set; }

        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "passWord")]
        public string Password { get; set; }
    }

    public class FirstPhaseUrlModel
    {
        [JsonProperty(PropertyName = "min")]
        public decimal Min { get; set; }

        [JsonProperty(PropertyName = "max")]
        public decimal Max { get; set; }

        [JsonProperty(PropertyName = "resultCount")]
        public int ResultCount { get; set; }

        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }
    }
}
