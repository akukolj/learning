using System.Linq;
using SamuraiApp.Domain;
using  SamuraiApp.Data;

namespace Samurai.UI
{
    class Program
    {
        private static SamuraiContext _context = new SamuraiContext();
        static void Main(string[] args)
        {
            InsertSamurai();
            InsertMultipleSamurais();
            SimpleQuery();
        }

        private static void InsertSamurai()
        {
            var samurai = new SamuraiApp.Domain.Samurai {Name = "Aleksandra"};
            using (var context = new SamuraiContext())
            {
                context.Samurais.Add(samurai);
                context.SaveChanges();
            }
        }

        private static void InsertMultipleSamurais()
        {
            var samurai = new SamuraiApp.Domain.Samurai { Name = "Aleksandra" };
            var samurai2 = new SamuraiApp.Domain.Samurai {Name = "Jane"};
            using (var context = new SamuraiContext())
            {
                context.Samurais.AddRange(samurai,samurai2);
                context.SaveChanges();
            }
        }

        private static void SimpleQuery()
        {
            using (var context = new SamuraiContext())
            {
                var list =context.Samurais.ToList();
            }
        }

        private static void MoreQueries()
        {
            var samurais = _context.Samurais.FirstOrDefault(s => s.Name == "Aleksandra");
        }
    }
}
 