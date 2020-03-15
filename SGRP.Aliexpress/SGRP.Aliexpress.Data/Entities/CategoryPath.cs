using System;
using System.Collections.Generic;
using System.Text;
using SGRP.Aliexpress.Data.Entities.Base;

namespace SGRP.Aliexpress.Data.Entities
{
    public class CategoryPath : Entity
    {
        public long ProductId { get; set; }

        public long CategoryId { get; set; }

        public string CatMain { get; set; }

        public string CatSub1 { get; set; }

        public string CatSub2 { get; set; }

        public string CatSub3 { get; set; }
    }
}
