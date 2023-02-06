using System;
using System.Collections.Generic;

namespace InterviewTrackerBackend.Models;

public partial class PanelTbl
{
    public int PanelId { get; set; }

    public int ManagerId { get; set; }

    public string PanelName { get; set; } = null!;

    public DateTime AvailableFrom { get; set; }

    public DateTime AvailableTo { get; set; }

    public string Skills { get; set; } = null!;

    public string EmailId { get; set; } = null!;

    public virtual ManagerTbl Manager { get; set; } = null!;
}
