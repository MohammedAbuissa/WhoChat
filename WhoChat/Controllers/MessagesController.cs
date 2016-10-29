using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WhoChat.Models;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
namespace WhoChat.Controllers
{
    [Authorize]
    public class MessagesController : Controller
    {
        ApplicationDbContext DbContext;
        UserManager<ApplicationUser> UserManager;

        public MessagesController()
        {
            DbContext = new ApplicationDbContext();
            UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(DbContext));
        }
        // GET: Messages
        public async Task<ActionResult> List()
        {
            var currentUser = await UserManager.FindByIdAsync(User.Identity.GetUserId());           
            List<Message> messages = DbContext.Messages.Where(x => x.To.Id == currentUser.Id).ToList();
            return View(messages);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]

    }
}