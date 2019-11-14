using Alexa.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Alexa.Data.Model;
using Alexa.Data;
using System.Data.Entity;

namespace Alexa.Repository
{
    public class GroupRepository : IGroupRepository
    {
        private AlexaDbContext db = new AlexaDbContext();

        public List<Group> GetAll()
        {
            return db.Groups.ToList();
        }

        public Group GetById(int? id)
        {
            return db.Groups.Find(id);
        }

        public void Add(Group group)
        {
            db.Groups.Add(group);
            db.SaveChanges();
        }

        public void Update(Group group)
        {
            db.Entry(group).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Remove(Group group)
        {
            db.Groups.Remove(group);
            db.SaveChanges();
        }
    }
}
