using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterviewTrackerBackend.Models
{
    public class TempManagerTbl
    {
        [System.Text.Json.Serialization.JsonIgnore]
        public int ManagerId { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public string ManagerName { get; set; } = null!;

        public static explicit operator TempManagerTbl(ManagerTbl? v)
        {
            throw new NotImplementedException();
        }
    }
}