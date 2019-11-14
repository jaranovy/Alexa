using Alexa.Data.Model;
using System.Collections.Generic;

namespace Alexa.Repository.Interfaces
{
    public interface IGroupRepository
    {
        List<Group> GetAll();
        Group GetById(int? id);

        void Add(Group group);
        void Update(Group group);
        void Remove(Group group);
    }
}
