using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookMillApp_Domain.Model
{
    public class User : IdentityUser
    {
        public Supplier Supplier { get; set; }
        public Manufacturer Manufacturer { get; set; }
    }
}
