using Alexa.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Alexa.Data.Model;
using Alexa.Tests.Repositories;

namespace Alexa.Tests.Repository
{
    public class SupplierRepositoryTest : ISupplierRepository
    {
        private AlexaDbContextTest db;

        public SupplierRepositoryTest(AlexaDbContextTest db)
        {
            this.db = db;
        }

        public List<Supplier> GetAll()
        {
            return db.Suppliers;
        }

        public Supplier GetById(int? id)
        {
            return db.Suppliers.Single(s => s.ID == id.GetValueOrDefault(0));
        }

        public void Add(Supplier supplier)
        {
            db.Suppliers.Add(supplier);
        }

        public void Remove(Supplier supplier)
        {
            var db_supplier = db.Suppliers.Single(s => s.ID == supplier.ID);
            db.Suppliers.Remove(db_supplier);
        }

        public void Update(Supplier supplier)
        {
            var db_supplier = db.Suppliers.Single(s => s.ID == supplier.ID);
            db_supplier.Name = supplier.Name;
            db_supplier.Address = supplier.Address;
            db_supplier.Email = supplier.Email;
            db_supplier.Phone = supplier.Phone;

            db_supplier.Groups.Clear();
            foreach (Group group in supplier.Groups)
            {
                db_supplier.Groups.Add(db.Groups.Single(item => item.ID == group.ID));
            }
        }
    }
}
