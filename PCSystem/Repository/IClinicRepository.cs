using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCSystem.Repository
{
   public interface IClinicRepository
    {

        IEnumerable<Clinic> GetAll();
        Clinic Get(int? id);
        Clinic Add(Clinic clinic);
        bool Update(Clinic clinic);
        bool Delete(int id);
    }
}
