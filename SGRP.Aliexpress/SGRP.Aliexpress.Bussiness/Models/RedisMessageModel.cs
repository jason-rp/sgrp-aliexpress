using System;
using System.Collections.Generic;
using System.Text;

namespace SGRP.Aliexpress.Bussiness.Models
{
    public class RedisMessageModel 
    {
        public bool IsRun { get; set; }

        public List<string> Urls { get; set; }
    }

    public class RedisCategoryUrlModel
    {
        public List<string> Urls { get; set; }

    }
}
