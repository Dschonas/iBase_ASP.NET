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
    public class ArtistController : Controller
    {
        private iBaseEntities db = new iBaseEntities();

        // GET: Artist
        public ActionResult Index(string suchname)
        {
            var artists = db.ArtistTable.ToList();

            if (!string.IsNullOrEmpty(suchname))
                artists = artists.Where(at => at.Name.Contains(suchname)).ToList();
            
            return View(artists);
        }

        // GET: Artist/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArtistTable artistTable = db.ArtistTable.Find(id);
            if (artistTable == null)
            {
                return HttpNotFound();
            }
            return View(artistTable);
        }

        // GET: Artist/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Artist/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Href,Genre,FollowersTotal,Popularity,Type")] ArtistTable artistTable)
        {
            if (ModelState.IsValid)
            {
                db.ArtistTable.Add(artistTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(artistTable);
        }

        // GET: Artist/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArtistTable artistTable = db.ArtistTable.Find(id);
            if (artistTable == null)
            {
                return HttpNotFound();
            }
            return View(artistTable);
        }

        // POST: Artist/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Href,Genre,FollowersTotal,Popularity,Type")] ArtistTable artistTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(artistTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(artistTable);
        }

        // GET: Artist/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ArtistTable artistTable = db.ArtistTable.Find(id);
            if (artistTable == null)
            {
                return HttpNotFound();
            }
            return View(artistTable);
        }

        // POST: Artist/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ArtistTable artistTable = db.ArtistTable.Find(id);
            db.ArtistTable.Remove(artistTable);
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
