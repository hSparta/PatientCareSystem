using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using PCSystem.Repository;

namespace PCSystem.Controllers
{
    public class ClinicsController : Controller
    {
        // Instantiate log4net
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        readonly IClinicRepository repository;

        private PatientCareSystemEntities db = new PatientCareSystemEntities();


        public ClinicsController(IClinicRepository repository)
        {
            this.repository = repository;
        }

        // GET: Clinics
        public ActionResult Index()
        {
            try
            {
                // return View(db.Clinics.ToList());
                var data = repository.GetAll();
                return View(data);
            }
            catch (Exception ex)
            {
                //If an exception is thrown, log the error and return null. 

                log.Error(string.Format("Failed to load list of clinics {0}", ex));
                return null;
            }
        }

        // GET: Clinics/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                var clinic = repository.Get(id);

                if (clinic == null)
                {
                    return HttpNotFound();
                }
                return View(clinic);
            }
            catch (Exception ex)
            {
                //If an exception is thrown, log the error and return null. 

                log.Error(string.Format("An error had been encountered getting the clinic details {0}, Exception Message {1}", id, ex));
                return null;
            }
        }

        // GET: Clinics/Create
        public ActionResult Create()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                //If an exception is thrown, log the error and return null. 

                log.Error(string.Format("Failed to render the view for creating a clinic {0}", ex));
                return null;
            }
        }

        // POST: Clinics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Clinic1,ID")] Clinic clinic)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    repository.Add(clinic);
                    return RedirectToAction("Index");
                }
                return View(clinic);
            }
            catch (Exception ex) 
            {
                //If an exception is thrown, log the error and return null. 

                log.Error(string.Format("An error has been encountered creating the clinic {0}. Exception Message: {1} ", clinic.ID, ex));
                return null;
            }
        }

        // GET: Clinics/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Clinic clinic = db.Clinics.Find(id);
                if (clinic == null)
                {
                    return HttpNotFound();
                }
                return View(clinic);
            }
            catch (Exception ex)
            {
                //If an exception is thrown, log the error and return null. 

                log.Error(string.Format("An error has been encountered loading the form to edit the clinic {0} information. Exception Message: {1}", id, ex));
                return null;
            }
        }

        // POST: Clinics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Clinic1,ID")] Clinic clinic)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    repository.Update(clinic);                   
                    return RedirectToAction("Index");
                }
                return View(clinic);
            }
            catch (Exception ex)
            {
                //If an exception is thrown, log the error and return null. 

                log.Error(string.Format("An error has been encountered updating clinic {0} information. Exception Message: {1}", clinic.ID, ex));
                return null;
            }
        }

        // GET: Clinics/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Clinic clinic = db.Clinics.Find(id);
                if (clinic == null)
                {
                    return HttpNotFound();
                }
                return View(clinic);
            }
            catch (Exception ex)
            {
                //If an exception is thrown, log the error and return null. 

                log.Error(string.Format("An error has been encountered loading the clinic {0} information to delete. Exception Message: {1}",id,ex));
                return null;
            }
        }

        // POST: Clinics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                repository.Delete(id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                //If an exception is thrown, log the error and return null. 

                log.Error(string.Format("An error has been encountered deleting clinic {0}. Exception Message: {1}", id, ex));
                return null;
            }
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
