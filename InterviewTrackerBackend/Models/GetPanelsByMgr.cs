using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace InterviewTrackerBackend.Models
{
    [Keyless]
    public class GetPanelsByMgr
    {
    public int Panel_Id { get; set; }

    public int Manager_Id { get; set; }

    public string Panel_Name { get; set; } = null!;

    public DateTime Available_From { get; set; }

    public DateTime Available_To { get; set; }

    public string Skills { get; set; } = null!;

    public string Email_Id { get; set; } = null!;
    //public String Manager_Name {get; set;} =null!;
    }
}