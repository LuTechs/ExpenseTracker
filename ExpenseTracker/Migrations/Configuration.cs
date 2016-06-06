using ExpenseTracker.Models;

namespace ExpenseTracker.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public  class Configuration : DbMigrationsConfiguration<ExpenseTracker.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ExpenseTracker.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.
            context.Database.ExecuteSqlCommand("TRUNCATE TABLE EXPENSES");
            var rnd = new Random();
            for (int i = 1; i <= 100; i++)
            {
                var id = rnd.Next(1, 20);
                context.Expenses.AddOrUpdate(e => e.Id,
                    new Expense
                    {
                        Amount = 2 * i,
                        Title = "Buy sth " + i,
                        Description = "Description " + i,
                        Date = DateTime.UtcNow,

                    });
            }
            context.SaveChanges();
        }
    }
}
