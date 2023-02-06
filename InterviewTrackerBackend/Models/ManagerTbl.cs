using System;
using System.Collections.Generic;

namespace InterviewTrackerBackend.Models;

public partial class ManagerTbl
{
    public int ManagerId { get; set; }

    public string ManagerName { get; set; } = null!;

    public virtual ICollection<PanelTbl> PanelTbls { get; } = new List<PanelTbl>();
}
