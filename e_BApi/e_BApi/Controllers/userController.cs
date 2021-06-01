using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using e_BApi.Models;
using e_BApi.DAL;

namespace e_BApi.Controllers
{
    public class userController : ApiController
    {
        private User _u;
        public userController()
        {
            _u = new User();
        }

        #region (Admin,User Auth)

        [Authorize(Roles = "Admin, User")]
        [HttpGet]
        [Route("getId")]
        public IHttpActionResult GetInfo()
        {
            var identity = (ClaimsIdentity)User.Identity;
            string id = identity.Claims.FirstOrDefault(c => c.Type == "ExpID").Value;
            int expid = Int32.Parse(id);


            return Ok(_u.GetExp(expid));
        }

        [Authorize(Roles = "Admin, User")]
        [HttpPost]
        [Route("pPrediction")] //post Prediction
        public IHttpActionResult PostPrediction([FromBody] Prediction pr)
        {
            var identity = (ClaimsIdentity)User.Identity;
            string id = identity.Claims.FirstOrDefault(c => c.Type == "ExpID").Value;
            int ide = Int32.Parse(id);
            pr.ExpID = ide;
            return Ok(_u.PostPrediction(pr));
        }

        [Authorize(Roles = "Admin, User")]
        [HttpPost]
        [Route("pDailyEntry")] //post DailyEntry
        public IHttpActionResult PostDE([FromBody] DailyEntry de)
        {
            var identity = (ClaimsIdentity)User.Identity;
            string id = identity.Claims.FirstOrDefault(c => c.Type == "ExpID").Value;
            int ide = Int32.Parse(id);

            de.ExpID = ide;
            return Ok(_u.PostDE(de));
        }

        [Authorize(Roles = "Admin, User")]
        [HttpGet]
        [Route("gDailyEntry/{dateid}")] //Get List for his id  and @dateid from DailyEntry
        public IHttpActionResult GetListDE(int dateid)
        {
            var identity = (ClaimsIdentity)User.Identity;
            string xid = identity.Claims.FirstOrDefault(c => c.Type == "ExpID").Value;
            int id = Int32.Parse(xid);


            return Ok(_u.GetListDe(id, dateid));
        }


        #endregion


        #region (User Auth)



        #endregion


        #region (Admin Auth)

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("GetFNPDC/{expid}/{dateid}")] // Get FullName, Prediction, QuantityDailyEntry, CountDailyEntry
        public IHttpActionResult GetFNPDC(int expid, int dateid)
        {

            return Ok(_u.GetFNPDC(expid, dateid));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("GeListFNPDC/{dateid}")] //Get List    -||-     for all Exporters
        public IHttpActionResult GetListFNPDC(int dateid)
        {

            return Ok(_u.GetListFNPDC(dateid));
        }




        [Authorize(Roles = "Admin")]
        [HttpPost]
        [Route("pDate")] //post Time Period start/enddate
        public IHttpActionResult PosteDate([FromBody] Date d)
        {
            return Ok(_u.PostDate(d));
        }


        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("GetPE/{dateid}")]// get sumPrediction / sumDailyEntry for @dateId
        public IHttpActionResult GetPE(int dateid)
        {

            return Ok(_u.GetPE(dateid));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("GetListExp")] // get list of all exporters
        public IHttpActionResult GetListExp()
        {

            return Ok(_u.GetListExp());
        }


        #endregion
































    }
}
