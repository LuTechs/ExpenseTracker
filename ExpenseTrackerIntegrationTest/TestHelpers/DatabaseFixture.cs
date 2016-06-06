using System;
using System.Data.Entity.Migrations;
using ExpenseTracker.Automapper;
using ExpenseTracker.Models;

namespace ExpenseTracker.IntergrationTest.TestHelpers
{
    public class DatabaseFixture : IDisposable
    {
        public ApplicationDbContext DbContext;

        public DatabaseFixture()
        {
            SetupDatabase();
            SetupMapper();
        }

        private static void SetupMapper()
        {
            AutoMapper.Mapper.Initialize(e =>
            {
                e.AddProfile<ExpenseToExpenseViewModelProfile>();
                e.AddProfile<ExpenseViewModeltoExpenseProfile>();
            });
        }

        private void SetupDatabase()
        {
            DbContext = new ApplicationDbContext();
            var configuration = new ExpenseTracker.Migrations.Configuration();
            var migrator = new DbMigrator(configuration);
            migrator.Update();
        }


        public void Dispose()
        {
            DbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE EXPENSES");
            DbContext.Dispose();
        }
    }
}