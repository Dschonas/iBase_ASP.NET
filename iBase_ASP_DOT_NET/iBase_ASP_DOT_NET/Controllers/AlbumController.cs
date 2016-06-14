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

namespace iBase_ASP_DOT_NET.Controllers
{
    public class AlbumController : Controller
    {
        private iBaseEntities db = new iBaseEntities();

        // GET: Album
        public ActionResult Index(string sortOrder, int? popularity, string searchString,
                                    string currentFilter, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "ID" ? "id_desc" : "ID";

            if (searchString != null) {
                page = 1;
            }
            else {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var albums = from alb in db.AlbumTable
                         select alb;

            if (!String.IsNullOrEmpty(searchString))
                albums = albums.Where(a => a.Name.Contains(searchString));

            switch (sortOrder) {
                case "name_desc":
                    albums = albums.OrderByDescending(a => a.Name);
                    break;
                case "id_desc":
                    albums = albums.OrderByDescending(a => a.Id);
                    break;
                default:
                    albums = albums.OrderBy(a => a.Name);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

            return View(albums.ToPagedList(pageNumber, pageSize));
        }

        // GET: Album/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlbumTable albumTable = db.AlbumTable.Find(id);
            if (albumTable == null)
            {
                return HttpNotFound();
            }
            return View(albumTable);
        }

        // GET: Album/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Album/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Href,ImageURL")] AlbumTable albumTable)
        {
            if (ModelState.IsValid)
            {
                db.AlbumTable.Add(albumTable);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(albumTable);
        }

        // GET: Album/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlbumTable albumTable = db.AlbumTable.Find(id);
            if (albumTable == null)
            {
                return HttpNotFound();
            }
            return View(albumTable);
        }

        // POST: Album/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Href,ImageURL")] AlbumTable albumTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(albumTable).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(albumTable);
        }

        // GET: Album/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlbumTable albumTable = db.AlbumTable.Find(id);
            if (albumTable == null)
            {
                return HttpNotFound();
            }
            return View(albumTable);
        }

        // POST: Album/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            AlbumTable albumTable = db.AlbumTable.Find(id);
            db.AlbumTable.Remove(albumTable);
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
