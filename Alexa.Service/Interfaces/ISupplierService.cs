using Alexa.Service.Dtos;
using System.Collections.Generic;

namespace Alexa.Service.Interfaces
{
    public interface ISupplierService
    {
        List<SupplierDto> GetAll();
        List<SupplierDto> getAllSuppliers();
        SupplierDto GetById(int? id);

        SupplierDto Create();
        void Add(SupplierDto supplier);
        void Edit(SupplierDto supplierDb);
        void Delete(int id);
    }
}