using Alexa.Data.Model;
using Alexa.Repository;
using Alexa.Repository.Interfaces;
using Alexa.Service.Dtos;
using Alexa.Service.Interfaces;
using System.Collections.Generic;

namespace Alexa.Service
{
    public class SupplierService : ISupplierService
    {
        private ISupplierRepository SupplierRepository = new SupplierRepository();
        private IGroupRepository GroupRepository = new GroupRepository();

        public SupplierService()
        {
            SupplierRepository = new SupplierRepository();
            GroupRepository = new GroupRepository();
        }

        public SupplierService(ISupplierRepository sr, IGroupRepository gr)
        {
            SupplierRepository = sr;
            GroupRepository = gr;
        }

        public List<SupplierDto> GetAll()
        {
            var suppliers = new List<SupplierDto>();

            List<Supplier> Suppliers = SupplierRepository.GetAll();

            foreach (Supplier dbs in Suppliers)
            {
                var groups = new List<GroupDto>();
                foreach(Group dbg in dbs.Groups)
                {
                    groups.Add(new GroupDto(dbg.ID, dbg.Name));
                }

                var SupplierDto = new SupplierDto(dbs.ID, dbs.Name, dbs.Address, dbs.Email, dbs.Phone, groups);
                suppliers.Add(SupplierDto);
            }

            return suppliers;
        }

        public List<SupplierDto> getAllSuppliers()
        {
            List<Supplier> suppliers = SupplierRepository.GetAll();

            var allSuppliers = new List<SupplierDto>();

            foreach (Supplier dbs in suppliers)
            {
                allSuppliers.Add(new SupplierDto(dbs.ID, dbs.Name, dbs.Address, dbs.Email, dbs.Phone));
            }

            return allSuppliers;
        }

        public SupplierDto GetById(int? id)
        {
            var dbs = SupplierRepository.GetById(id);

            var groups = new List<GroupDto>();
            foreach (Group dbg in dbs.Groups)
            {
                groups.Add(new GroupDto(dbg.ID, dbg.Name));
            }

            var SupplierDto = new SupplierDto(dbs.ID, dbs.Name, dbs.Address, dbs.Email, dbs.Phone, groups);

            return SupplierDto;
        }

        public SupplierDto Create()
        {
            var supplier = new SupplierDto();

            var allGroups = GroupRepository.GetAll();

            return supplier;
        }

        public void Add(SupplierDto supplier_in)
        {
            Supplier supplier = new Supplier(supplier_in.ID, supplier_in.Name, supplier_in.Address, supplier_in.Email, supplier_in.Phone);

            foreach(GroupDto groupDto in supplier_in.Groups)
            {
                supplier.Groups.Add(GroupRepository.GetById(groupDto.ID));
            }

            SupplierRepository.Add(supplier);
        }

        public void Edit(SupplierDto supplier_in)
        {
            Supplier supplier = SupplierRepository.GetById(supplier_in.ID);
            supplier.Name = supplier_in.Name;
            supplier.Address = supplier_in.Address;
            supplier.Email = supplier_in.Email;
            supplier.Phone = supplier_in.Phone;

            supplier.Groups.Clear();
            foreach (GroupDto groupDto in supplier_in.Groups)
            {
                supplier.Groups.Add(GroupRepository.GetById(groupDto.ID));
            }

            SupplierRepository.Update(supplier);
        }

        public void Delete(int id)
        {
            Supplier supplier = SupplierRepository.GetById(id);

            SupplierRepository.Remove(supplier);
        }
    }
}