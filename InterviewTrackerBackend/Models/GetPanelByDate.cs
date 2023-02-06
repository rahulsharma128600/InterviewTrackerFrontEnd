using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace InterviewTrackerBackend.Models
{
    [Keyless]
    public class GetPanelByDate
    {
        public int Panel_Id {get; set;}
         public String Panel_Name {get; set;} =null!;

         public DateTime Available_From { get; set; }

        public DateTime Available_To { get; set; }
        public String Skills {get; set;}=null!;
        public String Manager_Name {get; set;} =null!;

        

    }
}