using Retrospective.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Retrospective.Controllers
{
    public class SearchController : Controller
    {
        // GET: ActionPlan
        public ActionResult Index(string Comment = "Enter your comment...")
        {
            var actionPlan = new ActionPlanModelView();
            actionPlan.Comment = Comment;
            return View(actionPlan);
        }

        public ActionResult Search(string Comment)
        {
            var actionPlan = new ActionPlanModelView();
            var actionDB = GetActionPlan(Comment);
            if (actionDB != null)
            {
                actionPlan.Comment = Comment;
                actionPlan.sprintName = actionDB.sprintName;
                actionPlan.sprintCommentId = actionDB.sprintCommentId;
                actionPlan.sprintComment = actionDB.sprintComment;
                actionPlan.decsions = actionDB.Decisions.Split(',').ToList();
            }
            return View("Index", actionPlan);
        }


        private V_ActionPlans GetActionPlan(string Comment)
        {
            using (var ctx = GetRetrospectiveToolsContext())
            {
                return ctx.V_ActionPlans.FirstOrDefault(a=> a.sprintComment.ToLower().Trim() == Comment.ToLower().Trim());
            }
        }
        internal static RetrospectiveToolsContext GetRetrospectiveToolsContext()
        {
            RetrospectiveToolsContext entities = new RetrospectiveToolsContext();
            entities.Database.ExecuteSqlCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;");

            return entities;
        }

    }
}