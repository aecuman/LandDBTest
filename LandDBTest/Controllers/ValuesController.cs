﻿using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using LandDBTest.Models;
using PagedList;

namespace LandDBTest.Controllers
{
    public class ValuesController : Controller
    {
        private landdbContainer db = new landdbContainer();

       
        // GET: Values
    [HttpGet]
        public ActionResult Index(float? block, string vill_search, string county, string sortVal,string Filter_Value, int? Page_No)
        {
            ViewBag.CurrentSortOrder = block;          
            if (vill_search != null)
            {
                Page_No = 1;
            }
            else
            {
                vill_search = Filter_Value;
                ViewBag.FilterValue = vill_search;
            }

            ViewBag.FilterValue = vill_search;
            var val = from s in db.Values
                      select s;
            if (!String.IsNullOrEmpty(vill_search)|| block!=null)
            {
                val = val.Where(v => v.village.Contains(vill_search)|| v.block.ToString().Contains(block.ToString()));               
            }
            switch (sortVal)
            {
                case "district":
                    val = val.OrderByDescending(stu => stu.district);
                    break;
                case "tenure":
                    val = val.OrderBy(stu => stu.tenure);
                    break;
                
                default:
                    val = val.OrderBy(stu => stu.Id);
                    break;
            }

            int Size_Of_Page = 10;
            int No_Of_Page = (Page_No ?? 1);
            return View(val.ToPagedList(No_Of_Page, Size_Of_Page));

         
        }

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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,tenure,block,district,county,acreage,use_value,fair_value,village,user")] Value value)
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
    }
}