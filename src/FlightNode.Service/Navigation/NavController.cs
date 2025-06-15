using System.Collections.Generic;
using System.Security.Claims;
using System.Web.Http;
using FligthNode.Common.Api.Controllers;
using System.Linq;

namespace FlightNode.Service.Navigation
{
    /// <summary>
    /// API Controller for serving a user-customized navigation tree.
    /// </summary>
    public class NavController : LoggingController
    {
        /// <summary>
        /// HTTP GET request for a navigation menu.
        /// </summary>
        /// <returns>
        /// IHttpActionResult with OK status, containing the navigation tree in the body.
        /// </returns>
        /// <example>
        /// GET: /api/v1/nav
        /// </example>
        public IHttpActionResult Get()
        {
            var reqContext = this.RequestContext;

            var parent = new NavigationNode();

            var claimsPrincipal = reqContext.Principal as ClaimsPrincipal;
            if (claimsPrincipal == null)
            {
                return Ok(parent);
            }

            var claims = (claimsPrincipal.Claims ?? new List<Claim>()).ToList();

            if (claims.Any())
            {
                parent = AddTreeForAllUsers(parent);
                parent = AddAdministrativeTree(claims, parent);
                parent = AddReporterTree(claims, parent);
            }
            else
            {
                parent = NavigationForAnonymousUsers(parent);
            }

            return Ok(parent);
        }

        private static NavigationNode AddTreeForAllUsers(NavigationNode parent)
        {
            parent.AddChild(new NavigationNode("Logout", "#/logout"));
            parent.AddChild(new NavigationNode("My Account", "#/users/profile"));

            return parent;
        }

        private static NavigationNode NavigationForAnonymousUsers(NavigationNode parent)
        {
            parent.AddChild(new NavigationNode("Login", "#/login"));
            parent.AddChild(new NavigationNode("Create Account", "#/users/register"));

            return parent;
        }


        private NavigationNode AddAdministrativeTree(List<Claim> claims, NavigationNode parent)
        {
            if (claims.Any(x => HasRole(x, "Administrator")))
            {
                // When the application is multi-project aware, there will be a need
                // to separate Coordinator and Administrator privileges. Right now,
                // the following make sense for a Coordinator as much as an Admin -
                // except that a coordinator should only be able to create users
                // at a "lower" level.

                var users = new NavigationNode("Manage", "#/users");

                users.AddChild(new NavigationNode("Users", "#/users"));
                users.AddChild(new NavigationNode("Pending Users", "#/users/pending"));
                users.AddChild(new NavigationNode());
                users.AddChild(new NavigationNode("Volunteer Tracking", string.Empty));
                users.AddChild(new NavigationNode("Work Days", "#/workdays"));
                users.AddChild(new NavigationNode("Work Types", "#/worktypes"));
                users.AddChild(new NavigationNode("Locations", "#/locations"));
                users.AddChild(new NavigationNode());
                users.AddChild(new NavigationNode("Bird Surveys", string.Empty));
                users.AddChild(new NavigationNode("Species Lists", "#/species"));

                parent.AddChild(users);
            }
            return parent;
        }

        private NavigationNode AddReporterTree(List<Claim> claims, NavigationNode parent)
        {
            if (claims.Any(x => HasRole(x, "Reporter")))
            {
                var collection = new NavigationNode("Reporting", "");

                collection.AddChild(new NavigationNode("Work Logs", "#/workdays/mylist"));

                collection.AddChild(new NavigationNode("Report Waterbird Foraging Data", "#/foraging"));

                parent.AddChild(collection);
            }
            return parent;
        }

        private static bool HasRole(Claim x, string roleName)
        {
            return x.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role" &&
                   x.Value == roleName;
        }
    }
}