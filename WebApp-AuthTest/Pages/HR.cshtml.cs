using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp_AuthTest.Pages
{
    [Authorize(Policy = "MustHR")]
    public class HRModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
