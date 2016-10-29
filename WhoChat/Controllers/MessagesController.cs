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
using WhoChat.Models.ViewModels.Messages;

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
            List<Message> messages = DbContext.Messages.Where(x => x.To.Id == currentUser.Id).OrderByDescending(x => x.DateCreated).ToList();
            return View(messages);
        }

        //GET: New Message Form
        public ActionResult New()
        {
            return View();
        }
        public enum OperationResult
        {
            Success, Fail
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> New(SubmitMsgVM SubmitMsg)
        {
            var currentUser = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            if(ModelState.IsValid)
            {
                var Msg = new Message();
                Msg.From = currentUser;
                Msg.DateCreated = DateTime.Now;
                Msg.To = await UserManager.FindByEmailAsync(SubmitMsg.ToEmail);
                Msg.MsgText = SubmitMsg.MsgText;
                DbContext.Messages.Add(Msg);
                DbContext.SaveChanges();
                return View("SendMsgResult", OperationResult.Success);
            }
            else
            {
                return View("SendMsgResult", OperationResult.Fail);
            }
        }

    }
}