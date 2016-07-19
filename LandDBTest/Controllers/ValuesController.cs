using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using LandDBTest.Models;
using PagedList;
using Microsoft.AspNet.Identity;
using System.Web.Security;
using System.Collections;
using System.Collections.Generic;

namespace LandDBTest.Controllers
{
    public class ValuesController : Controller
    {
        private landdbContainer db = new landdbContainer();
        String matrix;
       

        // GET: Values
       
      public ActionResult Index()
        {
           return View();
        }
       
        public ActionResult List(int? block, string vill_search, string county, string sortVal, string Filter_Value, int? Page_No)
        {
         
            
            ViewBag.CurrentSortOrder = sortVal;
            ViewBag.blockFilter = block;
            ViewBag.SortingDate = sortVal == "Date_desce" ? "Date_" : "Date_desce";

            if (vill_search != null)
                {

                    Page_No = 1;
                }
                else
                {
                vill_search = Filter_Value;
                
                
                 ViewBag.FilterValue = vill_search;
                ViewBag.blockFilter = block;
                }
          
                ViewBag.FilterValue = vill_search;
     
            var val = from s in db.Values
                      select s;

            if (!String.IsNullOrEmpty(vill_search)|| block!=null)
                {

                val = val.Where(v => v.village.Contains(vill_search.Trim()));
                if (block != null) { val = val.Where(v => v.block.ToString().Contains(block.ToString())); }
               // val=val.Where(v=> v.block.ToString().Contains(block.ToString()));
                }
                switch (sortVal)
                {
                   
                    case "Date_":
                        val = val.OrderBy(stu => stu.period);
                        break;

                case "Date_desce":
                    val = val.OrderByDescending(stu => stu.period);
                    break;

                default:
                        val = val.OrderBy(stu => stu.Id);
                        break;
                }

                int Size_Of_Page = 10;
                int No_Of_Page = (Page_No ?? 1);

            List<string> liststring = new List<string>();

            foreach (var row in val.ToList().Where(x => x.period != null))
            {

                var d8 = Convert.ToDateTime(row.period);
                var d8string = string.Format("new Date({0},{1},{2})", d8.Year, d8.Month, d8.Day);
                string stringRay = string.Format("[{0},{1}]",d8string,row.rateperacre);
               // var joinString = string.Join(",", stringRay);
                // matrix = 
                liststring.Add(stringRay);
            }
          matrix=  string.Join(",", liststring);
                ViewBag.ArrayList = matrix;
            

            
                return View(val.ToPagedList(No_Of_Page, Size_Of_Page));

            }


        [Authorize(Roles = "Admin, CanEdit")]
        // GET: Values/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Value value = db.Values.Find(id);
            if (value == null)
            {
                return HttpNotFound();
            }
            return View(value);
        }

        // GET: Values/Create
        [Authorize(Roles = "Admin, CanEdit")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Values/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,tenure,block,district,county,acreage,use_value,fair_value,village,user")] Value value)
        {
            if (ModelState.IsValid)
            {
                db.Values.Add(value);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(value);
        }

        // GET: Values/Edit/5
        [Authorize(Roles = "Admin, CanEdit")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Value value = db.Values.Find(id);
            if (value == null)
            {
                return HttpNotFound();
            }
            return View(value);
        }

        // POST: Values/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin, CanEdit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,tenure,block,county,acreage,use_value,fair_value,village,user,period")] Value value)
        {
            if (ModelState.IsValid)
            {
                db.Entry(value).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(value);
        }

        // GET: Values/Delete/5
        [Authorize(Roles = "Admin, CanEdit")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Value value = db.Values.Find(id);
            if (value == null)
            {
                return HttpNotFound();
            }
            return View(value);
        }

        // POST: Values/Delete/5
        [Authorize(Roles = "Admin, CanEdit")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Value value = db.Values.Find(id);
            db.Values.Remove(value);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        [AllowAnonymous]
        public JsonResult AutoCompleteVillage(string term)
        {
            var result = (from r in db.Values
                          where r.village.ToLower().StartsWith(term)
                          select new { r.village }).Distinct().Take(8);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ReportWithPivot(int? block, string vill_search)
        {
            if(block!=null && !string.IsNullOrEmpty(vill_search))
            {
                var val = from s in db.Values
                          select s;
                val = val.Where(s => s.village.Contains(vill_search) && s.block.ToString().Contains(block.ToString())&& s.period!=null);

                return View(val.ToList());
            }
            else
            {
              return  View();
            }
            
        }
    }
}
