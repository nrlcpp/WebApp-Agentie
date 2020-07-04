using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class RemarksForReservationsDetails
    {
        public string Agent { get; set; }
        public string Content { get; set; }

        public static RemarksForReservationsDetails FromRemarks(Remarks remarks)
        {
            return new RemarksForReservationsDetails
            {
                Content = remarks.Content,
                Agent = remarks.Agent
            };
        }
    }
}
