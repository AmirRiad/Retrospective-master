using Retrospective.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Retrospective.Controllers
{
    public class ActionPlanCommentsController : Controller
    {
        // GET: MostCommonWords
        public ActionResult Index(string Comment)
        {
            var actionPlanComments = GetActionPlanComments(Comment);
            var model = new ActionPlanCommentsModelView();
            model.ActionPlans = actionPlanComments;
            return View(model);
        }

        private List<string> GetActionPlanComments(string Comment)
        {
            using (var ctx = GetRetrospectiveToolsContext())
            {
                return ctx.V_ActionPlans.Where(a=>a.sprintComment.Contains(Comment)).OrderByDescending(a=>a.sprintCommentId).Select(a=>a.sprintComment).ToList();
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