using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InterviewTrackerBackend.Models;
using Microsoft.AspNetCore.Mvc;

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
    }
}