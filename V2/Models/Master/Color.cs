using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace V2.Models.Master
{
    public class Color
    {
        public int ColorId { get; set; }
        
        public string ColorName { get; set; }
        public string ColorCode { get; set; }
        public bool IsActive { get; set; }
    }
}
