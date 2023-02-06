using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace InterviewTrackerBackend.Models
{
    [Keyless]
    public class GetPanelsByManagers
    {
        public int Panel_Id {get; set;}
         public String Panel_Name {get; set;} =null!;

         public DateTime Available_From { get; set; }

        public DateTime Available_To { get; set; }
        public String Email_Id{get;set;}=null!;
        public String Skills {get; set;}=null!;
        public String Manager_Name {get; set;} =null!;
    }
}