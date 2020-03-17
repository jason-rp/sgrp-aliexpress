using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace SGRP.Aliexpress.Web.Models
{
    public class IndexModel : PageModel
    {

        [BindProperty]
        public string[] txttest { get; set; }

        public void OnGet()
        {

        }
        public void OnPost()
        {
            // do something with txttest
            var data = txttest;
        }
    }
}
