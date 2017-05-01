using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace PCSystem.Controllers
{
    public class PatientsController : Controller
    {
        // Instantiate log4net
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private PatientCareSystemEntities db = new PatientCareSystemEntities();

        // GET: Patients
        public ActionResult Index()
        {
            try
            {
                var patients = db.Patients.Include(p => p.Clinic).Include(p => p.Gender1);
                return View(patients.ToList());
            }
            catch (Exception ex)
            {
                //If an exception is thrown, log the error and return null. 

                log.Error(string.Format("Failed to retrieve a list of patients. Exception message: {0}", ex)); //The exception retrieving the list of patients is logged 
                return null;
            }
        }

        // GET: Patients/Details/5
        public ActionResult Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Patient patient = db.Patients.Find(id);
                if (patient == null)
                {
                    return HttpNotFound();
                }
                return View(patient);
            }
            catch (Exception ex)
            {
                //If an exception is thrown, log the error and return null. 

                log.Error(string.Format("Failed to retrieve patient details. Exception Message: {0}", ex));
                return null;
            }
        }

        // GET: Patients/Create
        public ActionResult Create()
        {
            try
            {

                ViewBag.REGISTERED_AT_CLINIC = new SelectList(db.Clinics, "ID", "Clinic1");
                ViewBag.GENDER = new SelectList(db.Genders, "GENDER1", "GENDER1");
                return View();
            }
            catch (Exception ex)
            {
                //If an exception is thrown, log the error and return null. 

                log.Error(string.Format("Issue with building drop down list. Exception: {0}", ex));
                return null;
            }
       
        }

        // POST: Patients/Create
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,NAME,AGE,ADDRESS,GENDER,REGISTERED_AT_CLINIC")] Patient patient)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Patients.Add(patient);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.REGISTERED_AT_CLINIC = new SelectList(db.Clinics, "ID", "Clinic1", patient.REGISTERED_AT_CLINIC);
                ViewBag.GENDER = new SelectList(db.Genders, "GENDER1", "GENDER1", patient.GENDER);
                return View(patient);
            }
            catch (Exception ex)
            {
                //If an exception is thrown, log the error and return null. 

                log.Error(string.Format("Failed to create new patient {0}. Exception Message {1}", patient.ID, ex));
                return null;
            }
         
        }

        // GET: Patients/Edit/5
        public ActionResult Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Patient patient = db.Patients.Find(id);
                if (patient == null)
                {
                    return HttpNotFound();
                }
                ViewBag.REGISTERED_AT_CLINIC = new SelectList(db.Clinics, "ID", "Clinic1", patient.REGISTERED_AT_CLINIC);
                ViewBag.GENDER = new SelectList(db.Genders, "GENDER1", "GENDER1", patient.GENDER);
                return View(patient);
            }
            catch (Exception ex)
            {
                //If an exception is thrown, log the error and return null. 

                log.Error(string.Format("Faild to load form to edit patient infromation. Exception Message: {0}", ex));
                return null;
            }
        }

        // POST: Patients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,NAME,AGE,ADDRESS,GENDER,REGISTERED_AT_CLINIC")] Patient patient)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Entry(patient).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.REGISTERED_AT_CLINIC = new SelectList(db.Clinics, "ID", "Clinic1", patient.REGISTERED_AT_CLINIC);
                ViewBag.GENDER = new SelectList(db.Genders, "GENDER1", "GENDER1", patient.GENDER);
                return View(patient);
            }
            catch (Exception ex)
            {
                //If an exception is thrown, log the error and return null. 

                log.Error(string.Format("Failed to edit patient details, patient {0}. Exception Message{1}", patient.ID, ex));
                return null;
            }
        }

        // GET: Patients/Delete/5
        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Patient patient = db.Patients.Find(id);
                if (patient == null)
                {
                    return HttpNotFound();
                }
                return View(patient);
            }
            catch (Exception ex)
            {
                //If an exception is thrown, log the error and return null. 

                log.Error(string.Format("An error encountered while finding patient information. ID: {0}. Exception Message: {1}", id,ex));
                return null;
            }
        }

        // POST: Patients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Patient patient = db.Patients.Find(id);
                db.Patients.Remove(patient);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                //If an exception is thrown, log the error and return null. 

                log.Error(string.Format("Failed to delete patient: {0}",ex));
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
