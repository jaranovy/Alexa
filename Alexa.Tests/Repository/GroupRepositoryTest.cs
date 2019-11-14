using System.Collections.Generic;
using System.Linq;
using Alexa.Data.Model;
using Alexa.Repository.Interfaces;
using Alexa.Tests.Repositories;

namespace Alexa.Tests.Repository
{
    public class GroupRepositoryTest : IGroupRepository
    {
        private AlexaDbContextTest db;

        public GroupRepositoryTest(AlexaDbContextTest db)
        {
            this.db = db;
        }

        public List<Group> GetAll()
        {
            return db.Groups;
        }

        public Group GetById(int? id)
        {
            return db.Groups.Single(s => s.ID == id.GetValueOrDefault(0));
        }

        public void Add(Group group)
        {
            db.Groups.Add(group);
        }

        public void Remove(Group group)
        {
            var db_group = db.Groups.Single(s => s.ID == group.ID);
            db.Groups.Remove(db_group);
        }

        public void Update(Group group)
        {
            var db_group = db.Groups.Single(s => s.ID == group.ID);
            db_group.Name = group.Name;

            db_group.Suppliers.Clear();
            foreach (Supplier supplier in group.Suppliers)
            {
                db_group.Suppliers.Add(db.Suppliers.Single(item => item.ID == supplier.ID));
            }
        }
    }
}
