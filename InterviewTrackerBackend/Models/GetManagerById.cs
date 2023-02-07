using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace InterviewTrackerBackend.Models
{
    [Keyless]
    public class GetManagerById
    {
        public string ManagerName { get; set; } = null!;
    }
}