using Alexa.Data.Model;
using System.Collections.Generic;

namespace Alexa.Repository.Interfaces
{
    public interface ISupplierRepository
    {
        List<Supplier> GetAll();
        Supplier GetById(int? id);

        void Add(Supplier supplier);
        void Update(Supplier supplier);
        void Remove(Supplier supplier);
    }
}
