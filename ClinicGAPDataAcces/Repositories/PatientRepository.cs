using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClinicGAPDataAcces.Repositories
{
    public class PatientRepository : Repository<DBModel>
    {
        Repository<DBModel> repository = new Repository<DBModel>();

        public List<Patients> GetAll()
        {
            return repository.GetAll<Patients>();
        }

        public Patients GetById(int id)
        {
            return repository.GetById<Patients>(id);
        }

        public void Add(Patients item)
        {
            repository.Add<Patients>(item);
        }

        public void Update(Patients item)
        {
            repository.Update<Patients>(item);
        }

        public void Delete(Patients item)
        {
            repository.Delete<Patients>(item);
        }
    }
}
