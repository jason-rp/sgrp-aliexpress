using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using SGRP.Aliexpress.Data.Entities.Base;

namespace SGRP.Aliexpress.Data.Entities
{
    public class Product : Entity
    {
        public bool IsParent { get; set; }

        public long ProductId { get; set; }

        public string ProductKeyId { get; set; }

        public string ProductName { get; set; }

        public string Description { get; set; }

        public string BuyingPrice { get; set; }

        public string ItemLot { get; set; }

        public string BrandName { get; set; }

        public long StockNumber { get; set; }

        public long CategoryId { get; set; }

        public string CategoryName { get; set; }

        public long StoreId { get; set; }

        public string StoreName { get; set; }

        public string StoreYear { get; set; }

        public string StoreRatingPercent { get; set; }

        public decimal StoreRatingDescribed { get; set; }

        public decimal StoreRatingCommunication { get; set; }

        public decimal StoreRatingShippingSpeed { get; set; }

        public int StoreRatingTotal { get; set; }

        public long OrderNumber { get; set; }

        public long RatingNumber { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal RatingPercent { get; set; }

        public string ProcessingTime { get; set; }

        public decimal TotalPrice { get; set; }

        public string ParentChild { get; set; }

        public string ParentSku { get; set; }

        public string RelationshipType { get; set; }

        public string Specification10 { get; set; }

    }
}
