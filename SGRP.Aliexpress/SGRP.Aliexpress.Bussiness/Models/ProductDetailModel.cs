using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace SGRP.Aliexpress.Bussiness.Models
{
    public class ProductDetailModel
    {

        [JsonProperty(PropertyName = "title")] 
        public string Title { get; set; }
    }
}
