using BasicAuthentication.Secuirity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Web.Http;

namespace BasicAuthentication.Controllers
{
    [BasicAuthentication]
    public class EmployeeController : ApiController
    {

        [Authorize(Roles="Male,Female")]
        [Route("api/GetSignedInfo")]
        public IHttpActionResult GetSignedUserInfo()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var claims = from c in identity.Claims
                         select new
                         {
                             subject = c.Subject.Name,
                             type = c.Type,
                             value = c.Value
                         };
            return Ok(claims);
        }
        [Authorize(Roles ="Male")]
        [Route("api/GetMaleEmployee")]
        public IHttpActionResult GetMaleEmployee()
        {
            using (APIEntities entities = new APIEntities())
            {
               
            return Ok(entities.EmployeeDates.Where(emp => emp.Gender.ToLower() == "male").ToList());
                
                
            }
        }
        [Authorize(Roles = "Female")]
        [Route("api/GetFemaleEmployee")]
        public IHttpActionResult GetFemaleEmployee()
        {
            using (APIEntities entities = new APIEntities())
            {

                return Ok(entities.EmployeeDates.Where(emp => emp.Gender.ToLower() == "female").ToList());


            }
        }
        [Authorize(Roles = "Male,Female")]
        [Route("api/GetAllEmployee")]
        public IHttpActionResult GetAllEmployee()
        {
            using (APIEntities entities = new APIEntities())
            {

                return Ok(entities.EmployeeDates.ToList());


            }
        }

    }
}
