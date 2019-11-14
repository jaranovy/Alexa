using System.Collections.Generic;
using Alexa.Service.Dtos;
using Alexa.Data.Model;
using Alexa.Service.Interfaces;
using Alexa.Repository.Interfaces;
using Alexa.Repository;

namespace Alexa.Service
{
    public class GroupService : IGroupService
    {
        private ISupplierRepository SupplierRepository = new SupplierRepository();
        private IGroupRepository GroupRepository = new GroupRepository();

        public GroupService()
        {
            SupplierRepository = new SupplierRepository();
            GroupRepository = new GroupRepository();
        }

        public GroupService(ISupplierRepository sr, IGroupRepository gr)
        {
            SupplierRepository = sr;
            GroupRepository = gr;
        }

        public List<GroupDto> GetAll()
        {
            var groups = new List<GroupDto>();

            List<Group> Groups = GroupRepository.GetAll();

            foreach (Group dbg in Groups)
            {
                var GroupDto = new GroupDto(dbg.ID, dbg.Name);
                groups.Add(GroupDto);
            }

            return groups;
        }

        public List<GroupDto> getAllGroups()
        {
            List<Group> groups = GroupRepository.GetAll();

            var allGroups = new List<GroupDto>();

            foreach (Group dbg in groups)
            {
                allGroups.Add(new GroupDto(dbg.ID, dbg.Name));
            }

            return allGroups;
        }

        public GroupDto GetById(int? id)
        {
            Group dbg = GroupRepository.GetById(id);

            var suppliers = new List<SupplierDto>();

            foreach (Supplier dbs in dbg.Suppliers )
            {
                suppliers.Add(new SupplierDto(dbs.ID, dbs.Name, dbs.Address, dbs.Email, dbs.Phone));
            }

            var GroupDto = new GroupDto(dbg.ID, dbg.Name, suppliers);

            return GroupDto;
        }

        public GroupDto Create()
        {
            var group = new GroupDto();

            return group;
        }

        public void Add(GroupDto group_in)
        {
            var group = new Group(group_in.ID, group_in.Name);

            foreach (SupplierDto supplierDto in group_in.Suppliers)
            {
                group.Suppliers.Add(SupplierRepository.GetById(supplierDto.ID));
            }

            GroupRepository.Add(group);
        }

        public void Edit(GroupDto group_in)
        {
            Group group = GroupRepository.GetById(group_in.ID);
            group.Name = group_in.Name;

            group.Suppliers.Clear();
            foreach (SupplierDto SupplierDto in group_in.Suppliers)
            {
                group.Suppliers.Add(SupplierRepository.GetById(SupplierDto.ID));
            }

            GroupRepository.Update(group);
        }

        public void Delete(int id)
        {
            Group group = GroupRepository.GetById(id);

            GroupRepository.Remove(group);
        }
    }
}