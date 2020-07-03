using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Dto
{
    public class RemarksDtoDetail
    {
        public long Id { get; set; }
        public string Agent { get; set; }
        public string Content { get; set; }
        public Importance Importance { get; set; }

    }
}
