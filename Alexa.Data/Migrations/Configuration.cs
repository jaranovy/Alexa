namespace Alexa.Data.Migrations
{
    using Alexa.Data.Model;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AlexaDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AlexaDbContext context)
        {
            var groups = new List<Group>
            {
              new Group { Name = "Cleaners" },
              new Group { Name = "Office supply (paper, envelopes, etc)" },
              new Group { Name = "Telephone service" },
              new Group { Name = "Security" }
            };

            groups.ForEach(group => context.Groups.AddOrUpdate(g => g.ID, group));
            context.SaveChanges();

            var suppliers = new List<Supplier>
            {
                new Supplier
                {
                    Name = "Uklízeèky.cz",
                    Address = "Václavské námìstí 1, Praha",
                    Email = "objednavky@uklizecky.cz",
                    Phone = "123 456 789",
                    Groups = new List<Group>() {groups.Single(g => g.Name == "Cleaners") }
                },

                new Supplier
                {
                    Name = "Papírna.cz",
                    Address = "Námìstí Míru 1, Praha",
                    Email = "objednavky@papirna.cz",
                    Phone = "234 567 891",
                    Groups = new List<Group>() {groups.Single(g => g.Name == "Office supply (paper, envelopes, etc)") }
                },

                new Supplier
                {
                    Name = "Telefonisti.cz",
                    Address = "Karlovo námìstí 1, Praha",
                    Email = "objednavky@telefonisti.cz",
                    Phone = "345 678 912",
                    Groups = new List<Group>() {groups.Single(g => g.Name == "Telephone service") }
                },

                new Supplier
                {
                    Name = "Bezpeèáci.cz",
                    Address = "Palackeho námìstí 1, Praha",
                    Email = "objednavky@bezpecaci.cz",
                    Phone = "456 789 123",
                    Groups = new List<Group>() {groups.Single(g => g.Name == "Security") }
                },

                new Supplier
                {
                    Name = "Ferda Mravenec",
                    Address = "Hrad I. nádvoøí è. p. 1, Praha",
                    Email = "ferda@mravenec.cz",
                    Phone = "567 891 234",
                    Groups = new List<Group>() {groups.Single(g => g.Name == "Cleaners"),
                        groups.Single(g => g.Name == "Office supply (paper, envelopes, etc)"),
                        groups.Single(g => g.Name == "Telephone service"),
                        groups.Single(g => g.Name == "Security") }
                }
            };

            suppliers.ForEach(supplier => context.Suppliers.AddOrUpdate(s => s.ID, supplier));
            context.SaveChanges();
        }
    }
}
