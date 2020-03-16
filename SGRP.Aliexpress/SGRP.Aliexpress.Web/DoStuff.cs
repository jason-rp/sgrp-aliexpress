using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SGRP.Aliexpress.Web.Interfaces;

namespace SGRP.Aliexpress.Web
{
    public class DoStuff : IDoStuff
    {
        public string GetData()
        {
            return "MyData";
        }
    }
}
