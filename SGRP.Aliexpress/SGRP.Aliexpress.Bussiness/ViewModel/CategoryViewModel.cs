using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace SGRP.Aliexpress.Bussiness.ViewModel
{
    public class CategoryViewModel
    {
        [JsonProperty(PropertyName = "categoryId")]
        public long CategoryId { get; set; }

        [JsonProperty(PropertyName = "categoryName")]
        public string CategoryName { get; set; }

        [JsonProperty(PropertyName = "pathCategories")]
        public string PatchCategory { get; set; }

        [JsonProperty(PropertyName = "productId")]
        public long ProductId { get; set; }

        [JsonProperty(PropertyName = "productName")]
        public string ProductName { get; set; }

        //[JsonProperty(PropertyName = "productSkuProps")]
        //public string ProductSkuProps { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "buyingPrice")]
        public string BuyingPrice { get; set; }

        [JsonProperty(PropertyName = "itemLot")]
        public string ItemLot { get; set; }

        [JsonProperty(PropertyName = "brandName")]
        public string BrandName { get; set; }

        [JsonProperty(PropertyName = "stockNumber")]
        public long StockNumber { get; set; }

        [JsonProperty(PropertyName = "storeId")]
        public long StoreId { get; set; }

        [JsonProperty(PropertyName = "storeName")]
        public string StoreName { get; set; }

        [JsonProperty(PropertyName = "storeYear")]
        public string StoreYear { get; set; }

        [JsonProperty(PropertyName = "storeRating")]
        public string StoreRating { get; set; }

        [JsonProperty(PropertyName = "storeRatingMultiple")]
        public string StoreRatingMultiple { get; set; }

        [JsonProperty(PropertyName = "storeRatingTotal")]
        public int StoreRatingTotal { get; set; }

        [JsonProperty(PropertyName = "orderNumber")]
        public long OrderNumber { get; set; }

        [JsonProperty(PropertyName = "ratingNumber")]
        public long RatingNumber { get; set; }

        [JsonProperty(PropertyName = "ratingPercentNumber")]
        public string RatingPercentNumber { get; set; }

        [JsonProperty(PropertyName = "shippingContent")]
        public string ShippingContent { get; set; }

        [JsonProperty(PropertyName = "onTimeDelivery")]
        public string OnTimeDelivery { get; set; }

        [JsonProperty(PropertyName = "imagePathList")]
        public string ImagePatchList { get; set; }


        [JsonProperty(PropertyName = "specificationHtml")]
        public string SpecificationHtml { get; set; }

        [JsonProperty(PropertyName = "productSKUPropertyList")]
        public string ProductSKUPropertyList { get; set; }

        [JsonProperty(PropertyName = "skuPriceList")]
        public string SkuPriceList { get; set; }

        [JsonProperty(PropertyName = "specification1")]
        public string Specification1 { get; set; }

        [JsonProperty(PropertyName = "specification2")]
        public string Specification2 { get; set; }

        [JsonProperty(PropertyName = "specification3")]
        public string Specification3 { get; set; }

        [JsonProperty(PropertyName = "specification4")]
        public string Specification4 { get; set; }
    }
}
