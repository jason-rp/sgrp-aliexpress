using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using SGRP.Aliexpress.Data.Entities.Base;

namespace SGRP.Aliexpress.Data.Entities
{
    public class Product : Entity
    {
        public bool? IsParent { get; set; }

        public long ProductId { get; set; }

        public string ProductKeyId { get; set; }

        public string ProductName { get; set; }

        public string Description { get; set; }

        public string BuyingPrice { get; set; }

        public string ItemLot { get; set; }

        public string BrandName { get; set; }

        public long StockNumber { get; set; }

        public long? CategoryId { get; set; }

        public string CategoryName { get; set; }

        public long? StoreId { get; set; }

        public string StoreName { get; set; }

        public string StoreYear { get; set; }

        public string StoreRatingPercent { get; set; }

        public decimal StoreRatingDescribed { get; set; }

        public decimal StoreRatingCommunication { get; set; }

        public decimal StoreRatingShippingSpeed { get; set; }

        public int StoreRatingTotal { get; set; }

        public long OrderNumber { get; set; }

        public long RatingNumber { get; set; }

        public decimal RatingPercent { get; set; }

        public string ProcessingTime { get; set; }

        public string ShippingCompany { get; set; }

        public decimal? ShippingFee { get; set; }

        public string Shipping1ST { get; set; }

        public string Shipping2ND { get; set; }

        public string Shipping3RD { get; set; }

        public string Image1ST { get; set; }

        public string Image2ND { get; set; }

        public string Image3RD { get; set; }

        public string Image4TH { get; set; }

        public string Image5TH { get; set; }

        public string Image6TH { get; set; }

        public string Image7TH { get; set; }

        public string VariationTheme { get; set; }

        public string VariationColor { get; set; }

        public string VariationSize { get; set; }

        public string VariationPlus1ST { get; set; }

        public string VariationPlus2ND { get; set; }

        public string VariationShippingFrom { get; set; }

        public string OnTimeDelivery { get; set; }

        public decimal TotalPrice { get; set; }

        public string Bullet1ST { get; set; }

        public string Bullet2ND { get; set; }

        public string Bullet3RD { get; set; }

        public string Bullet4TH { get; set; }

        public string Bullet5TH { get; set; }

        public string ParentChild { get; set; }

        public long ParentSku { get; set; }

        public string RelationshipType { get; set; }

        public string Specification10 { get; set; }

    }
}
