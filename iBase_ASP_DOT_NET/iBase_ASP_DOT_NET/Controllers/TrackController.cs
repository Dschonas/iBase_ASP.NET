using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using iBase_ASP_DOT_NET.Models;
using PagedList;
using System.Data.Entity.Infrastructure;

namespace iBase_ASP_DOT_NET.Controllers
{
    public class TrackController : Controller
    {
        private iBaseEntities db = new iBaseEntities();

        // GET: Track
        public ActionResult Index (int? popularity, string searchString, string sortOrder,
                                    int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.NumberSortParm = sortOrder == "Number" ? "number_desc" : "Number";
            ViewBag.TypeSortParm = sortOrder == "Type" ? "type_desc" : "Type";
            ViewBag.PopularitySortParm = sortOrder == "Popularity" ? "popularity_desc" : "Popularity";

            var tracks = from t in db.TrackTable
                         select t;

            if (popularity != null)
                tracks = tracks.Where(tt => tt.Popularity >= popularity);

            if (!string.IsNullOrEmpty(searchString))
                tracks = tracks.Where(tt => tt.Name.Contains(searchString));

            ViewBag.CurrentFilter = searchString;

            switch (sortOrder) {
                case "name_desc":
                    tracks = tracks.OrderByDescending(a => a.Name);
                    break;
                case "number_desc":
                    tracks = tracks.OrderByDescending(a => a.TrackNumber);
                    break;
                case "type_desc":
                    tracks = tracks.OrderByDescending(a => a.Type);
                    break;
                case "popularity_desc":
                    tracks = tracks.OrderByDescending(a => a.Popularity);
                    break;
                default:
                    tracks = tracks.OrderBy(a => a.Name);
                    break;
            }

            int pageSize = 9;
            int pageNumber = (page ?? 1);

            ViewBag.playlist = new SelectList(db.PlaylistTable, "Id", "Name");
            return View(tracks.ToPagedList(pageNumber, pageSize));
        }

        // GET: Track/Details/5
        public ActionResult Details (string id)
        {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrackTable trackTable = db.TrackTable.Find(id);
            if (trackTable == null) {
                return HttpNotFound();
            }
            return View(trackTable);
        }

        // GET: Track/Create
        public ActionResult Create ()
        {
            ViewBag.ArtistID = new SelectList(db.ArtistTable, "Id", "Name");
            return View();
        }

        // POST: Track/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create ([Bind(Include = "Id,Name,Album,DiscNumber,DurationMS,Explicity,Href,Popularity,ImageURL,PreviewURL,TrackNumber,Type,ArtistID")] TrackTable trackTable)
        {
            if (ModelState.IsValid) {
                db.TrackTable.Add(trackTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ArtistID = new SelectList(db.ArtistTable, "Id", "Name", trackTable.ArtistID);
            return View(trackTable);
        }

        // GET: Track/Edit/5
        public ActionResult Edit (string id)
        {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrackTable trackTable = db.TrackTable.Find(id);
            if (trackTable == null) {
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
        public ActionResult Edit ([Bind(Include = "Id,Name,Album,DiscNumber,DurationMS,Explicity,Href,Popularity,ImageURL,PreviewURL,TrackNumber,Type,ArtistID")] TrackTable trackTable)
        {
            if (ModelState.IsValid) {
                db.Entry(trackTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ArtistID = new SelectList(db.ArtistTable, "Id", "Name", trackTable.ArtistID);
            return View(trackTable);
        }

        // GET: Track/Delete/5
        public ActionResult Delete (string id)
        {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrackTable trackTable = db.TrackTable.Find(id);
            if (trackTable == null) {
                return HttpNotFound();
            }
            return View(trackTable);
        }

        // POST: Track/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed (string id)
        {
            TrackTable trackTable = db.TrackTable.Find(id);
            db.TrackTable.Remove(trackTable);
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

        public ActionResult Add (string id)
        {
            if (id == null) {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TrackTable tracks = db.TrackTable.Find(id);
            if (tracks == null) {
                return HttpNotFound();
            }

            ViewBag.playlist = new SelectList(db.PlaylistTable, "Id", "Name");
            return View(tracks);
        }

        [HttpPost, ActionName("Add")]
        [ValidateAntiForgeryToken]
        public ActionResult AddConfirmed (string id, string playlist)
        {
            var playhastrack = new PlaylistHasTracks();
            playhastrack.TrackId = id;
            playhastrack.PlaylistId = int.Parse(playlist);
            db.PlaylistHasTracks.Add(playhastrack);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
