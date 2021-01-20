using Retrospective.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Retrospective.Controllers
{
    public class MostCommonWordsController : Controller
    {
        // GET: MostCommonWords
        public ActionResult Index()
        {
            var mostCommonWords = GetMostCommonWords();
            var model = new MostCommonWordsModelView();
            model.MostCommonWords = mostCommonWords;
            return View(model);
        }

        private List<MostDuplicatedWords_Result> GetMostCommonWords()
        {
            using (var ctx = GetRetrospectiveToolsContext())
            {
                return ctx.MostDuplicatedWords().ToList();
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