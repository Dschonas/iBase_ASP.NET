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
    public class PlaylistController : Controller
    {
        private iBaseEntities db = new iBaseEntities();

        // GET: Playlist
        public ActionResult Index ()
        {
            var playlists = db.PlaylistTable.Include(p => p.UserTable);
            if (!string.IsNullOrEmpty(User.Identity.Name))
                playlists = db.PlaylistTable.Where(p => p.UserTable.Username == User.Identity.Name);

            return View(playlists.ToList());
        }

        // GET: Playlist/Details/5
        public ActionResult Details (int? id)
        {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlaylistTable playlistTable = db.PlaylistTable.Find(id);
            if (playlistTable == null) {
                return HttpNotFound();
            }
            return View(playlistTable);
        }

        // GET: Playlist/Create
        public ActionResult Create ()
        {
            ViewBag.UserId = new SelectList(db.UserTable, "Id", "Username");
            return View();
        }

        // POST: Playlist/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create ([Bind(Include = "Id,UserId,Name")] PlaylistTable playlistTable)
        {
            if (ModelState.IsValid) {
                db.PlaylistTable.Add(playlistTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserId = new SelectList(db.UserTable, "Id", "Username", playlistTable.UserId);
            return View(playlistTable);
        }

        // GET: Playlist/Edit/5
        public ActionResult Edit (int? id)
        {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlaylistTable playlistTable = db.PlaylistTable.Find(id);
            if (playlistTable == null) {
                return HttpNotFound();
            }
            ViewBag.UserId = new SelectList(db.UserTable, "Id", "Username", playlistTable.UserId);
            return View(playlistTable);
        }

        // POST: Playlist/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit ([Bind(Include = "Id,UserId,Name")] PlaylistTable playlistTable)
        {
            if (ModelState.IsValid) {
                db.Entry(playlistTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.UserTable, "Id", "Username", playlistTable.UserId);
            return View(playlistTable);
        }

        // GET: Playlist/Delete/5
        public ActionResult Delete (int? id)
        {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PlaylistTable playlistTable = db.PlaylistTable.Find(id);
            if (playlistTable == null) {
                return HttpNotFound();
            }
            return View(playlistTable);
        }

        // POST: Playlist/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed (int id)
        {
            PlaylistTable playlistTable = db.PlaylistTable.Find(id);
            db.PlaylistTable.Remove(playlistTable);

            var phtremove = db.PlaylistHasTracks.Where(p => p.PlaylistId == id).ToList();
            foreach (var item in phtremove) {
                db.PlaylistHasTracks.Remove(item);
            }

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
    }
}
