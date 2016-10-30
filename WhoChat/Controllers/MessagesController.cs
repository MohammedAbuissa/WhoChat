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
        public ActionResult Threads(string Id)
        {
            if(Id == null)
            {
                var currentUser = UserManager.FindById(User.Identity.GetUserId());
                //get the count of messages that sent to currentUser and aren't read yet.
                var messages = DbContext.Messages.Where(x => x.To.Id == currentUser.Id && !x.IsRead)
                    .OrderByDescending(x => x.DateCreated)
                    .GroupBy(x => x.From)
                    .ToDictionary(key => key.Key, value => value.Count());
                return View("Threads", messages);
            }
            else 
            {
                var currentUser = UserManager.FindById(User.Identity.GetUserId());
                var threadUser = UserManager.FindById(Id);
                //if(threadUser == null)
                //    return View("Error", "Error Msg");
                var messages = DbContext.Messages.Where(x => (x.To.Id == currentUser.Id && x.From.Id == threadUser.Id) || (x.To.Id == threadUser.Id && x.From.Id == currentUser.Id))
                    .OrderBy(x => x.DateCreated);
                var messageBubbles = new List<MessageBubble>();
                foreach (var item in messages)
                {
                    messageBubbles.Add(new MessageBubble { Me = item.From.Id == currentUser.Id, Msg = item });
                }
                return View("Thread", messageBubbles);   
            }
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
        public ActionResult New(SubmitMsgVM SubmitMsg)
        {
            var currentUser = UserManager.FindById(User.Identity.GetUserId());
            if(ModelState.IsValid)
            {
                var Msg = new Message();
                Msg.From = currentUser;
                Msg.DateCreated = DateTime.Now;
                var targetUser = UserManager.FindByEmail(SubmitMsg.ToEmail);
                if (targetUser == null) return View("SendMsgResult", OperationResult.Fail);
                Msg.To = targetUser;
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