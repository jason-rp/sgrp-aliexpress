using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace SGRP.Aliexpress.Bussiness.ViewModel
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; }


        [JsonProperty(PropertyName = "productId")]
        public int ProductId { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string ProductName { get; set; }
    }
}
