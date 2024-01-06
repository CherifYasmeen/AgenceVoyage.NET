using ApplicationCore.Domain;
using ApplicationCore.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace UI.Web.Controllers
{
    public class ClientController : Controller
    {
        IServiceClient sc;
        IServiceConseiller scons;

        public ClientController(IServiceClient sc, IServiceConseiller scons)
        {
            this.sc = sc;
            this.scons = scons;
        }

        // GET: ClientController
        public ActionResult Index(string? loginSearch)
        {
            if(loginSearch==null)
            return View(sc.GetMany());
            else
            return View(sc.GetMany(c=>c.Login.Contains(loginSearch)));

        }

        // GET: ClientController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ClientController/Create
        public ActionResult Create()
        {
            ViewBag.lsConseillers = new SelectList(scons.GetMany(), "ConseillerId", "Prenom") ;
            return View();
        }

        // POST: ClientController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Client collection,IFormFile PhotoFile)
        {
            try
            {
                //sauvegarder l'image sous uploads
                if (PhotoFile != null)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(),
                    "wwwroot", "uploads", PhotoFile.FileName);
                    Stream stream = new FileStream(path, FileMode.Create);
                    PhotoFile.CopyTo(stream);
                    collection.Photo = PhotoFile.FileName;
                }
                sc.Add(collection);
                sc.Commit();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ClientController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ClientController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ClientController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ClientController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
