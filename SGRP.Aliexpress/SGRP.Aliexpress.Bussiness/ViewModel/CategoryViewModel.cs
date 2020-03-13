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

        [JsonProperty(PropertyName = "productSkuProps")]
        public string ProductSkuProps { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "buyingPrice")]
        public string BuyingPrice { get; set; }

        [JsonProperty(PropertyName = "itemLot")]
        public string ItemLot { get; set; }

        [JsonProperty(PropertyName = "brandName")]
        public string BrandName { get; set; }

        [JsonProperty(PropertyName = "stockNumber")]
        public string StockNumber { get; set; }

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
        public int RatingNumber { get; set; }

        [JsonProperty(PropertyName = "ratingPercentNumber")]
        public int RatingPercentNumber { get; set; }

        [JsonProperty(PropertyName = "shippingContent")]
        public string ShippingContent { get; set; }

        [JsonProperty(PropertyName = "onTimeDelivery")]
        public string OnTimeDelivery { get; set; }

        [JsonProperty(PropertyName = "imagePathList")]
        public string ImagePatchList { get; set; }


        [JsonProperty(PropertyName = "specificationHtml")]
        public string SpecificationHtml { get; set; }
    }
}
