using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Storage;
using SGRP.Aliexpress.Data.Entities.Base;

namespace SGRP.Aliexpress.Data.Entities
{
    public class Category : Entity
    {
        public long CategoryId { get; set; }

        public string CategoryName { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
