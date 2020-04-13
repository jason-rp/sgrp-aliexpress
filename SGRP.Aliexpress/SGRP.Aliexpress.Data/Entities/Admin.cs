using System;
using System.Collections.Generic;
using System.Text;
using SGRP.Aliexpress.Data.Entities.Base;

namespace SGRP.Aliexpress.Data.Entities
{
    public class Admin : Entity
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
