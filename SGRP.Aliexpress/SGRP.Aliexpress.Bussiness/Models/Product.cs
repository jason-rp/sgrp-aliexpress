using SGRP.Aliexpress.Bussiness.Models.Common;

namespace SGRP.Aliexpress.Bussiness.Models
{
    public class Product:BaseEntity
    {
        public long ProductId { get; set; }

        public string ProductName { get; set; }
    }
}
