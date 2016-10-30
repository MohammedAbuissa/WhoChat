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
        // GET: All Threads of Your Messages
        public async Task<ActionResult> ListThreads()
        {
            var currentUser = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            //get the count of messages that sent to currentUser and aren't read yet.
            var messages = DbContext.Messages.Where(x => x.To.Id == currentUser.Id && !x.IsRead)
                .OrderByDescending(x => x.DateCreated)
                .GroupBy(x => x.From.UserName)
                .ToDictionary(key => key.Key, value => value.Count());
            return View(messages);
        }

        public async Task<ActionResult> List(string Email)
        {
            return View();
        }

        //GET: New Message Form
        public ActionResult New()
        {
            return View();
        }

        //ToDo: New Message
        public enum OperationResult
        {
            Success, Fail
        }

        //POST: send Message to someone
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