using Alexa.Data.Model;
using System.Collections.Generic;
using System.Linq;

namespace Alexa.Tests.Repositories
{
    public class AlexaDbContextTest
    {
        public List<Group> Groups;
        public List<Supplier> Suppliers;

        public AlexaDbContextTest()
        {
            Groups = new List<Group>
            {
              new Group { ID = 0, Name = "Cleaners" },
              new Group { ID = 1, Name = "Office supply (paper, envelopes, etc)" },
              new Group { ID = 2, Name = "Telephone service" },
              new Group { ID = 3, Name = "Security" }
            };

            Suppliers = new List<Supplier>
            {
                new Supplier
                {
                    ID = 0,
                    Name = "Uklízečky.cz",
                    Address = "Václavské náměstí 1, Praha",
                    Email = "objednavky@uklizecky.cz",
                    Phone = "123 456 789",
                    Groups = new List<Group>() { Groups.Single(g => g.ID == 0) }
                },

                new Supplier
                {
                    ID = 1,
                    Name = "Papírna.cz",
                    Address = "Náměstí Míru 1, Praha",
                    Email = "objednavky@papirna.cz",
                    Phone = "234 567 891",
                    Groups = new List<Group>() { Groups.Single(g => g.ID == 1) }
                },

                new Supplier
                {
                    ID = 2,
                    Name = "Telefonisti.cz",
                    Address = "Karlovo náměstí 1, Praha",
                    Email = "objednavky@telefonisti.cz",
                    Phone = "345 678 912",
                    Groups = new List<Group>() { Groups.Single(g => g.ID == 2) }
                },

                new Supplier
                {
                    ID = 3,
                    Name = "Bezpečáci.cz",
                    Address = "Palackeho náměstí 1, Praha",
                    Email = "objednavky@bezpecaci.cz",
                    Phone = "456 789 123",
                    Groups = new List<Group>() { Groups.Single(g => g.ID == 3) }
                },

                new Supplier
                {
                    ID = 4,
                    Name = "Ferda Mravenec",
                    Address = "Hrad I. nádvoří č. p. 1, Praha",
                    Email = "ferda@mravenec.cz",
                    Phone = "567 891 234",
                    Groups = new List<Group>() {Groups.Single(g => g.ID == 0),
                        Groups.Single(g => g.ID == 1),
                        Groups.Single(g => g.ID == 2),
                        Groups.Single(g => g.ID == 3) }
                }
            };

            Groups.Single(g => g.ID == 0).Suppliers = new List<Supplier>();
            Groups.Single(g => g.ID == 0).Suppliers.Add(Suppliers.Single(s => s.ID == 0));
            Groups.Single(g => g.ID == 0).Suppliers.Add(Suppliers.Single(s => s.ID == 4));

            Groups.Single(g => g.ID == 1).Suppliers = new List<Supplier>();
            Groups.Single(g => g.ID == 1).Suppliers.Add(Suppliers.Single(s => s.ID == 1));
            Groups.Single(g => g.ID == 1).Suppliers.Add(Suppliers.Single(s => s.ID == 4));

            Groups.Single(g => g.ID == 2).Suppliers = new List<Supplier>();
            Groups.Single(g => g.ID == 2).Suppliers.Add(Suppliers.Single(s => s.ID == 2));
            Groups.Single(g => g.ID == 2).Suppliers.Add(Suppliers.Single(s => s.ID == 4));

            Groups.Single(g => g.ID == 3).Suppliers = new List<Supplier>();
            Groups.Single(g => g.ID == 3).Suppliers.Add(Suppliers.Single(s => s.ID == 3));
            Groups.Single(g => g.ID == 3).Suppliers.Add(Suppliers.Single(s => s.ID == 4));
        }
    }
}
