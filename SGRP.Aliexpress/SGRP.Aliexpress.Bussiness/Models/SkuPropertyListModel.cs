using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace SGRP.Aliexpress.Bussiness.Models
{
    public class SkuPropertyListModel
    {
        [JsonProperty(PropertyName = "skuPropertyName")]
        public string SkuPropertyName { get; set; }

        [JsonProperty(PropertyName = "skuPropertyValues")]
        public List<SkuPropertyValues> SkuPropertyValues { get; set; }
    }

    public class SkuPropertyValues
    {
        public string propertyValueDisplayName { get; set; }

        public long propertyValueId { get; set; }


    }

    public class SkuPriceList
    {
        public string skuPropIds { get; set; }


        public SkuVal skuVal { get; set; }
    }

    public class SkuVal
    {
        public string actSkuCalPrice { get; set; }
    }

}
