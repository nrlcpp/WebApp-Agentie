using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp.Models;

namespace WebApp.Dto
{
    public class RemarksForReservationDetails
    {
        public string Agent { get; set; }
        public string Content { get; set; }

        public static RemarksForReservationDetails FromRemarks(Remarks remarks)
        {
            return new RemarksForReservationDetails
            {
                Content = remarks.Content,
                Agent = remarks.Agent
            };
        }
    }
}
