using Alexa.Service.Dtos;
using System.Collections.Generic;

namespace Alexa.Service.Interfaces
{
    public interface IGroupService
    {
        List<GroupDto> GetAll();
        List<GroupDto> getAllGroups();
        GroupDto GetById(int? id);

        GroupDto Create();
        void Add(GroupDto group);
        void Edit(GroupDto group);
        void Delete(int id);
    }
}