using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Web;

namespace AuthToken
{
    public class AccessValidator : ServiceAuthorizationManager
    {
        protected override bool CheckAccessCore(OperationContext operationContext)
        {
            // Authorization header alınır, Base64 string çözümlenir
            var authHeader = WebOperationContext.Current.IncomingRequest.Headers["Authorization"];
            if (authHeader != null && authHeader != string.Empty)
            {
                var svcCredentials = System.Text.ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(authHeader.Substring(6))).Split(':');
                var user = new
                {
                    Name = svcCredentials[0],
                    Password = svcCredentials[1]
                };

                if (user.Name == "kubilay" && user.Password == "123456")
                {
                    // başarılı
                    return true;
                }
                else
                {
                    // Authorization header sağlanamadı
                    WebOperationContext.Current.OutgoingResponse.Headers.Add("WWW-Authenticate: Basic realm=\"WcfServiceAuthToken\"");
                    // HTTP status 401 fırlatılır
                    throw new WebFaultException(HttpStatusCode.Unauthorized);
                }
            }
            else
            {
                // Authorization header sağlanamadı
                WebOperationContext.Current.OutgoingResponse.Headers.Add("WWW-Authenticate: Basic realm=\"WcfServiceAuthToken\"");
                // HTTP status 401 fırlatılır
                throw new WebFaultException(HttpStatusCode.Unauthorized);
            }
        }
    }
}