using Alexa.Data;
using Alexa.Data.Model;
using Alexa.Repository.Interfaces;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Alexa.Repository
{
    public class SupplierRepository : ISupplierRepository
    {
        private AlexaDbContext db = new AlexaDbContext();

        public List<Supplier> GetAll()
        {
            return db.Suppliers.ToList();
        }

        public Supplier GetById(int? id)
        {
            return db.Suppliers.Find(id);
        }

        public void Add(Supplier supplier)
        {
            db.Suppliers.Add(supplier);
            db.SaveChanges();
        }

        public void Update(Supplier supplier)
        {
            db.Entry(supplier).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Remove(Supplier supplier)
        {
            db.Suppliers.Remove(supplier);
            db.SaveChanges();
        }
    }
}
