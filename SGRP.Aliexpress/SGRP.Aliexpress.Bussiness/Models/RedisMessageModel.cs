using System;
using System.Collections.Generic;
using System.Text;

namespace SGRP.Aliexpress.Bussiness.Models
{
    public class RedisMessageModel 
    {
        public bool IsRun { get; set; }

        public string Url { get; set; }
    }

    public class RedisCategoryUrlModel
    {
        public string[] Url { get; set; }
    }
}
