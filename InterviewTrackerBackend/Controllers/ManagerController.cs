using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterviewTrackerBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InterviewTrackerBackend.Controllers
{
    [ApiController]
    
    public class ManagerController : ControllerBase
    {
        private InterviewTrackerDemoContext mgrContext;

        public ManagerController(InterviewTrackerDemoContext MgrContext)
        {
            mgrContext = MgrContext;
        }
        [HttpGet]
        [Route("api/[controller]/GetMgr")]
        public IActionResult GetManager(){
            var mgr = mgrContext.ManagerTbls.ToList();
            return Ok(mgr);
        }

        [HttpGet]
        [Route("api/[controller]/GetMgrById/{str}")]
        public IActionResult GetMgrById(string str){
        var manager = mgrContext.GetManagerByIds.FromSqlInterpolated($"exec spGetManagersByManagerId @Manager_Id={str}").ToList();
             return Ok(manager);
    }
}
}