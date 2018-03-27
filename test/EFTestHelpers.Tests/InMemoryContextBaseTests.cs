using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace RimDev.EFTestHelpers.Tests
{
    public class InMemoryContextBaseTests
    {
        public class DatabaseContext : InMemoryContextBase
        {
            public DbSet<Person> People => Set<Person>();
        }

        public class Person
        {
            public int Id { get; set; }
        }

        DatabaseContext Database { get; } = new DatabaseContext();

        [Fact]
        public void In_memory_context_can_be_created()
        {
            Assert.NotNull(Database);
        }

        [Fact]
        public void In_memory_context_can_add_entity()
        {
            var person = new Person();

            Database.People.Add(person);
            Database.SaveChanges();

            Assert.Equal(1, Database.People.Count());
            Assert.Equal(0, person.Id);
        }
    }
}
