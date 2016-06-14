using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using iBase_ASP_DOT_NET.Models;

namespace iBase_ASP_DOT_NET.Controllers
{
    public class TrackController : Controller
    {
        private iBaseEntities db = new iBaseEntities();

        // GET: Track
        public ActionResult Index(int? popularity, string suchname)
        {
            var trackTables = db.TrackTable.Include(t => t.ArtistTable);

            if (popularity != null)
                trackTables = trackTables.Where(tt => tt.Popularity >= popularity);

            if (!string.IsNullOrEmpty(suchname))
                trackTables = trackTables.Where(tt => tt.Name.Contains(suchname));

            return View(trackTables.ToList());
        }

        // GET: Track/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrackTable trackTable = db.TrackTable.Find(id);
            if (trackTable == null)
            {
                return HttpNotFound();
            }
            return View(trackTable);
        }

        // GET: Track/Create
        public ActionResult Create()
        {
            ViewBag.ArtistID = new SelectList(db.ArtistTable, "Id", "Name");
            return View();
        }

        // POST: Track/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Album,DiscNumber,DurationMS,Explicity,Href,Popularity,ImageURL,PreviewURL,TrackNumber,Type,ArtistID")] TrackTable trackTable)
        {
            if (ModelState.IsValid)
            {
                db.TrackTable.Add(trackTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ArtistID = new SelectList(db.ArtistTable, "Id", "Name", trackTable.ArtistID);
            return View(trackTable);
        }

        // GET: Track/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrackTable trackTable = db.TrackTable.Find(id);
            if (trackTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.ArtistID = new SelectList(db.ArtistTable, "Id", "Name", trackTable.ArtistID);
            return View(trackTable);
        }

        // POST: Track/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Album,DiscNumber,DurationMS,Explicity,Href,Popularity,ImageURL,PreviewURL,TrackNumber,Type,ArtistID")] TrackTable trackTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trackTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ArtistID = new SelectList(db.ArtistTable, "Id", "Name", trackTable.ArtistID);
            return View(trackTable);
        }

        // GET: Track/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrackTable trackTable = db.TrackTable.Find(id);
            if (trackTable == null)
            {
                return HttpNotFound();
            }
            return View(trackTable);
        }

        // POST: Track/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            TrackTable trackTable = db.TrackTable.Find(id);
            db.TrackTable.Remove(trackTable);
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
