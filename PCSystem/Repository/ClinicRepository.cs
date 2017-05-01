using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PCSystem.Repository
{
    public class ClinicRepository : IClinicRepository
    {

        private PatientCareSystemEntities db = new PatientCareSystemEntities();

        public Clinic Add(Clinic clinic)
        {
            if(clinic == null)
            {
                throw new ArgumentNullException();
            }
            db.Clinics.Add(clinic);
            db.SaveChanges();
            return clinic;
        }

        public bool Delete(int id)
        {
            Clinic clinic = db.Clinics.Find(id);
            db.Clinics.Remove(clinic);
            db.SaveChanges();

            return true;
        }

        public Clinic Get(int? id)
        {
            Clinic clinic = db.Clinics.Find(id);

            return clinic;

        }

        public IEnumerable<Clinic> GetAll()
        {
            return db.Clinics.ToList();
        }

        public bool Update(Clinic clinic)
        {
            if (clinic == null)
            {
                throw new ArgumentNullException();
            }

            db.Entry(clinic).State = EntityState.Modified;
            db.SaveChanges();

            return true;
        }
    }
}