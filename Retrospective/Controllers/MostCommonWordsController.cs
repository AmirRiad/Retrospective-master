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
        public ActionResult Search(string Comment)
        {
            var model = new MostCommonWordsModelView();
            var mostCommonWordsDB = GetMostCommonWords(Comment);
            if (mostCommonWordsDB != null)
            {
                model.Comment = Comment;
                model.MostCommonWords = mostCommonWordsDB;
            }
            return View("Index", model);
        }
        private List<MostDuplicatedWords_Result> GetMostCommonWords()
        {
            using (var ctx = GetRetrospectiveToolsContext())
            {
                return ctx.MostDuplicatedWords().ToList();
            }
        }
        private List<MostDuplicatedWords_Result> GetMostCommonWords(string comment)
        {
            using (var ctx = GetRetrospectiveToolsContext())
            {
                return ctx.MostDuplicatedWords().Where(o=>o.Item.ToLower().Contains(comment.ToLower())).ToList();
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