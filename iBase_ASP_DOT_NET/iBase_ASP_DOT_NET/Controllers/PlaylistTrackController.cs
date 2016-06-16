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
    public class PlaylistTrackController : Controller
    {
        private iBaseEntities db = new iBaseEntities();

        // GET: PlaylistTrack
        public ActionResult Index (int? playlist)
        {
            var pht = db.PlaylistHasTracks.Include(p => p.PlaylistTable).Include(p => p.TrackTable);

            if (playlist != null)
                pht = pht.Where(p => p.PlaylistId == playlist);

            return View(pht.ToList());
        }

        // GET: PlaylistTrack/Details/5
        public ActionResult Details (int? id)
        {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlaylistHasTracks playlistHasTracks = db.PlaylistHasTracks.Find(id);
            if (playlistHasTracks == null) {
                return HttpNotFound();
            }
            return View(playlistHasTracks);
        }


        // GET: PlaylistTrack/Delete/5
        public ActionResult Delete (int? id)
        {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlaylistHasTracks playlistHasTracks = db.PlaylistHasTracks.Find(id);
            if (playlistHasTracks == null) {
                return HttpNotFound();
            }
            return View(playlistHasTracks);
        }

        // POST: PlaylistTrack/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed (int id)
        {
            PlaylistHasTracks playlistHasTracks = db.PlaylistHasTracks.Find(id);
            db.PlaylistHasTracks.Remove(playlistHasTracks);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose (bool disposing)
        {
            if (disposing) {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        /*
        // GET: PlaylistTrack/Create
        public ActionResult Create()
        {
            ViewBag.PlaylistId = new SelectList(db.PlaylistTable, "Id", "Name");
            ViewBag.TrackId = new SelectList(db.TrackTable, "Id", "Name");
            return View();
        }*/
        /*
        // POST: PlaylistTrack/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,TrackId,PlaylistId")] PlaylistHasTracks playlistHasTracks)
        {
            if (ModelState.IsValid)
            {
                db.PlaylistHasTracks.Add(playlistHasTracks);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PlaylistId = new SelectList(db.PlaylistTable, "Id", "Name", playlistHasTracks.PlaylistId);
            ViewBag.TrackId = new SelectList(db.TrackTable, "Id", "Name", playlistHasTracks.TrackId);
            return View(playlistHasTracks);
        }*/
        /*
        // GET: PlaylistTrack/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlaylistHasTracks playlistHasTracks = db.PlaylistHasTracks.Find(id);
            if (playlistHasTracks == null)
            {
                return HttpNotFound();
            }
            ViewBag.PlaylistId = new SelectList(db.PlaylistTable, "Id", "Name", playlistHasTracks.PlaylistId);
            ViewBag.TrackId = new SelectList(db.TrackTable, "Id", "Name", playlistHasTracks.TrackId);
            return View(playlistHasTracks);
        }*/
        /*
        // POST: PlaylistTrack/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,TrackId,PlaylistId")] PlaylistHasTracks playlistHasTracks)
        {
            if (ModelState.IsValid)
            {
                db.Entry(playlistHasTracks).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PlaylistId = new SelectList(db.PlaylistTable, "Id", "Name", playlistHasTracks.PlaylistId);
            ViewBag.TrackId = new SelectList(db.TrackTable, "Id", "Name", playlistHasTracks.TrackId);
            return View(playlistHasTracks);
        }*/

    }
}
