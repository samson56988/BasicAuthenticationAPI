using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using System.Security.Principal;
using System.Security.Claims;

namespace BasicAuthentication.Secuirity
{
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.Request.Headers.Authorization == null)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, "No Username Or Password Supplied");
            }
            else
            {
                string authInfo = actionContext.Request.Headers.Authorization.Parameter;
                string decodeauthinfo = Encoding.UTF8.GetString(Convert.FromBase64String(authInfo));
                string[] authInfoArray = decodeauthinfo.Split(':');
                string username = authInfoArray[0];
                string password = authInfoArray[1];

                if(UserValidate.Login(username,password))
                {
                    var userData = UserValidate.UserDetails(username, password);
                    var Identity = new GenericIdentity(username);
                    Identity.AddClaim(new Claim(ClaimTypes.Email, userData.MailID));
                    Identity.AddClaim(new Claim("MailID", userData.MailID));
                    Identity.AddClaim(new Claim("City", userData.City));
                    IPrincipal principal = new GenericPrincipal(Identity, userData.role.Split(','));
                    Thread.CurrentPrincipal = principal;
                    if(HttpContext.Current.User!=null)
                    {
                        HttpContext.Current.User = principal; 
                    }
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, "No Username Or Password Supplied");
                }
            }
            base.OnAuthorization(actionContext);
        }
    }
}