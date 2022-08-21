using Framework.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Repository.Interfaces.B2SMainInterfaceRepositories;
using System.Web.Http;


namespace Framework.Attributes
{
    public class PrivilegeCheckerAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        
        public string Title { get; }
        public string Gid { get; }
        

        public PrivilegeCheckerAttribute(string gid, string title)
        {
            Gid = gid;
            Title = title;
        }


      
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var privilegeRepository =(IPrivilegeRepository) context.HttpContext.RequestServices.GetService(typeof(IPrivilegeRepository));
            if (context.HttpContext.User.Identity != null)  
            {
                if(!context.HttpContext.User.Identity.IsAuthenticated)
                {
                    context.Result = new JsonResult("UserIsNotAuthenticated");
                    return;
                }
                else
                {
                    string username = context.HttpContext.User.Identity.Name;
                    bool hasPrivilege = privilegeRepository.CheckPrivilege(Guid.Parse(Gid), username, context.HttpContext.RequestAborted);
                    if (!hasPrivilege)
                    {
                        context.Result = new JsonResult("PrivilegeIsNotAllowed!");
                        return;
                    }
                }
            }

        }
    }
}
