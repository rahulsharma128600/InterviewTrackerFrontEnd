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
    //[Route("api/[controller]")]
    public class PanelController : ControllerBase
    {
        private InterviewTrackerDemoContext panContext;

        public PanelController(InterviewTrackerDemoContext PanContext)
        {
            panContext = PanContext;
        }

        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult GetPanels()
        {
            var panel = this.panContext.PanelTbls.ToList();
            return Ok(panel);
        }

        [HttpGet]
        [Route("api/[controller]/{id}")]
        public IActionResult GetPanel(int id)
        {
            var panel = panContext.PanelTbls.Find(id);

            if (panel != null)
            {
                return Ok(panel);
            }
            return NotFound($"Panel with Id: {id} not found");
        }
        //[HttpGet]
        //[Route("api/[controller]/PanelByDate/{from_date}/{to_date}")]
        // public IEnumerable<GetPanelByDate> GetPanelByDates(DateTime from_date,DateTime to_date){
        //     var panel = panContext.GetPanelByDates.FromSqlInterpolated($"exec spGetAvailablePanel2 @Available_From={from_date},@Available_To={to_date}").ToList();
        //     return panel;
        // }

        [HttpGet]
        [Route("api/[controller]/PanelByDate/{from_date}/{to_date}")]
        public IActionResult GetPanelByDates(DateTime from_date, DateTime to_date)
        {
            if (from_date > to_date)
            {
                return Ok(null);
            }
            var panel = panContext.GetPanelByDates.FromSqlInterpolated($"exec spGetAvailablePanel2 @Available_From={from_date},@Available_To={to_date}").ToList();
            return Ok(panel);
        }

        [HttpPost]
        [Route("api/[controller]/Add")]
        public IActionResult AddPanel(PanelTbl panel)
        {
            panContext.PanelTbls.Add(panel);
            panContext.SaveChanges();
            return Ok("Panel Created successfully");
        }

        // [HttpGet]
        // [Route("api/[controller]/GetPanelByMgr/{mgr_id}")]
        // public IActionResult GetPanelByMgr(int mgr_id)
        // {
        //     //need to write sp for get panel by manager id
        //     //var panel = panContext.PanelTbls.Find(mgr_id);
        //     var panel = panContext.PanelTbls.FirstOrDefault(pan => pan.ManagerId == mgr_id);
        //     if (panel != null)
        //     {
        //         return Ok(panel);
        //     }
        //     return NotFound($"Panel with Id: {mgr_id} not found");
        // }
        [HttpGet]
        [Route("api/[controller]/GetPanelByMgr/{mgrId}")]
        public IActionResult GetPanelByMgr(int mgrId)
        {
            var panel = panContext.GetPanelByMgr.FromSqlInterpolated($"exec spGetPanelByManager @Manager_Id={mgrId}").ToList();
            return Ok(panel);
        }

        [HttpGet]
        [Route("api/[controller]/GetPanelByManagers/{str}")]
        public IActionResult GetPanelByManagers(string str)
        {

            // int[] arr_mgrId= Array.ConvertAll(str.Split(","), s=>int.Parse(s));

            // for (int i = 0; i < arr_mgrId.Length; i++)
            // {
            //     if (arr_mgrId[i] != 0)
            //     {
            //         var mgr = panContext.ManagerTbls.Find(arr_mgrId[i]);
                
            //         TempManagerTbl newMgr= new TempManagerTbl{ManagerId=mgr.ManagerId,ManagerName=mgr.ManagerName};
            //         panContext.TempManagerTbls.Add(newMgr);
            //         panContext.SaveChanges();
                
            //     }
            // }
            // //GetPanelByMgrs();
            // //return Ok("TempManagerTbl updated");
             var panel = panContext.GetPanelsByManagers.FromSqlInterpolated($"exec spGetPanelsByManagersFinal @Manager_Id={str}").ToList();
             return Ok(panel);
        }

        // [HttpGet]
        // [Route("api/[controller]/GetPanelByMgr/{mgrId}")]
        // public IActionResult GetPanelByMgrs()
        // {
        //     var panel = panContext.PanelTbls.FromSqlInterpolated($"exec spGetPanelByManagers").ToList();
        //     return Ok(panel);
        // }


        [HttpGet]
        [Route("api/[controller]/PanelByDate/{from_date}/{to_date}/{date_format}")]
        public IActionResult GetPanelByDateFormat(DateTime from_date, DateTime to_date,string dateFormat)
        {
            if (from_date > to_date)
            {
                return Ok(null);
            }
            var panel = panContext.GetPanelByDates.FromSqlInterpolated($"exec spGetAvailablePanel2 @Available_From={from_date},@Available_To={to_date}").ToList();
            return Ok(panel);
        }






    }
}