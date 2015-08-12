// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using Moq;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xunit;

namespace RimDev.EFTestHelpers.Tests
{
    public class MockableNonGenericDbSetTests
    {
        [Fact]
        public void Moq_DbSet_can_be_used_for_Find()
        {
            var mockSet = new Mock<DbSet>();
            var product = new Product();
            mockSet.Setup(m => m.Find(It.IsAny<object[]>())).Returns(product);

            Assert.Same(product, mockSet.Object.Find(1));
            mockSet.Verify(m => m.Find(1), Times.Once());
        }

        [Fact]
        public void Moq_DbSet_can_be_used_for_Add()
        {
            var mockSet = new Mock<DbSet>();
            var product = new Product();
            mockSet.Setup(m => m.Add(product)).Returns(product);

            Assert.Same(product, mockSet.Object.Add(product));
            mockSet.Verify(m => m.Add(product), Times.Once());
        }

        [Fact]
        public void Moq_DbSet_can_be_used_for_AddRange()
        {
            var mockSet = new Mock<DbSet>();
            var products = new[] { new Product { Id = 1 }, new Product { Id = 2 } };
            mockSet.Setup(m => m.AddRange(products)).Returns(products);

            Assert.Same(products, mockSet.Object.AddRange(products));
            mockSet.Verify(m => m.AddRange(products), Times.Once());
        }

        [Fact]
        public void Moq_DbSet_can_be_used_for_Attach()
        {
            var mockSet = new Mock<DbSet>();
            var product = new Product();
            mockSet.Setup(m => m.Attach(product)).Returns(product);

            Assert.Same(product, mockSet.Object.Attach(product));
            mockSet.Verify(m => m.Attach(product), Times.Once());
        }

        [Fact]
        public void Moq_DbSet_can_be_used_for_Remove()
        {
            var mockSet = new Mock<DbSet>();
            var product = new Product();
            mockSet.Setup(m => m.Remove(product)).Returns(product);

            Assert.Same(product, mockSet.Object.Remove(product));
            mockSet.Verify(m => m.Remove(product), Times.Once());
        }

        [Fact]
        public void Moq_DbSet_can_be_used_for_RemoveRange()
        {
            var mockSet = new Mock<DbSet>();
            var products = new[] { new Product { Id = 1 }, new Product { Id = 2 } };
            mockSet.Setup(m => m.RemoveRange(products)).Returns(products);

            Assert.Same(products, mockSet.Object.RemoveRange(products));
            mockSet.Verify(m => m.RemoveRange(products), Times.Once());
        }

        [Fact]
        public void Moq_DbSet_can_be_used_for_SqlQuery()
        {
            var mockSqlQuery = new Mock<DbSqlQuery>();
            var products = new[] { new Product { Id = 1 }, new Product { Id = 2 } };
            mockSqlQuery.Setup(m => m.GetEnumerator()).Returns(((IEnumerable)products).GetEnumerator());

            var mockSet = new Mock<DbSet>();
            var query = "not a real query";
            var parameters = new object[] { 1, 2 };
            mockSet.Setup(m => m.SqlQuery(query, parameters)).Returns(mockSqlQuery.Object);

            Assert.Equal(new[] { 1, 2 }, mockSet.Object.SqlQuery(query, parameters).OfType<Product>().Select(p => p.Id));
            mockSet.Verify(m => m.SqlQuery(query, parameters), Times.Once());
        }

        [Fact]
        public void Moq_DbSet_can_be_used_for_SqlQuery_with_AsNoTracking()
        {
            var mockSqlQuery = new Mock<DbSqlQuery>();
            mockSqlQuery.Setup(m => m.AsNoTracking()).Returns(mockSqlQuery.Object);
            var products = new[] { new Product { Id = 1 }, new Product { Id = 2 } };
            mockSqlQuery.Setup(m => m.GetEnumerator()).Returns(((IEnumerable)products).GetEnumerator());

            var mockSet = new Mock<DbSet>();
            var query = "not a real query";
            var parameters = new object[] { 1, 2 };
            mockSet.Setup(m => m.SqlQuery(query, parameters)).Returns(mockSqlQuery.Object);

            Assert.Equal(new[] { 1, 2 }, mockSet.Object.SqlQuery(query, parameters).AsNoTracking().OfType<Product>().Select(p => p.Id));
            mockSet.Verify(m => m.SqlQuery(query, parameters), Times.Once());
            mockSqlQuery.Verify(m => m.AsNoTracking(), Times.Once());
        }

#pragma warning disable 612, 618
        [Fact]
        public void Moq_DbSet_can_be_used_for_SqlQuery_with_AsStreaming()
        {
            var mockSqlQuery = new Mock<DbSqlQuery>();
            mockSqlQuery.Setup(m => m.AsStreaming()).Returns(mockSqlQuery.Object);
            var products = new[] { new Product { Id = 1 }, new Product { Id = 2 } };
            mockSqlQuery.Setup(m => m.GetEnumerator()).Returns(((IEnumerable)products).GetEnumerator());

            var mockSet = new Mock<DbSet>();
            var query = "not a real query";
            var parameters = new object[] { 1, 2 };
            mockSet.Setup(m => m.SqlQuery(query, parameters)).Returns(mockSqlQuery.Object);

            Assert.Equal(new[] { 1, 2 }, mockSet.Object.SqlQuery(query, parameters).AsStreaming().OfType<Product>().Select(p => p.Id));
            mockSet.Verify(m => m.SqlQuery(query, parameters), Times.Once());
            mockSqlQuery.Verify(m => m.AsStreaming(), Times.Once());
        }
#pragma warning restore 612, 618

        [Fact]
        public void Moq_DbSet_can_be_used_for_Create()
        {
            var mockSet = new Mock<DbSet>();
            var product = new Product();
            mockSet.Setup(m => m.Create()).Returns(product);

            Assert.Same(product, mockSet.Object.Create());
            mockSet.Verify(m => m.Create(), Times.Once());
        }

        [Fact]
        public void Moq_DbSet_can_be_used_for_generic_Create()
        {
            var mockSet = new Mock<DbSet>();
            var product = new FeaturedProduct();
            mockSet.Setup(m => m.Create(typeof(FeaturedProduct))).Returns(product);

            Assert.Same(product, mockSet.Object.Create(typeof(FeaturedProduct)));
            mockSet.Verify(m => m.Create(typeof(FeaturedProduct)), Times.Once());
        }

        [Fact]
        public void Moq_DbSet_can_be_used_for_Local()
        {
            var mockSet = new Mock<DbSet>();
            var mockLocal = new Mock<IList>();
            mockSet.Setup(m => m.Local).Returns(mockLocal.Object);

            Assert.Same(mockLocal.Object, mockSet.Object.Local);
            mockSet.Verify(m => m.Local, Times.Once());
        }

        [Fact]
        public void Moq_DbSet_can_be_used_for_ToString()
        {
            var mockSet = new Mock<DbSet>();
            mockSet.Setup(m => m.ToString()).Returns("Hello World!");

            Assert.Equal("Hello World!", mockSet.Object.ToString());
            mockSet.Verify(m => m.ToString(), Times.Once());
        }

        [Fact]
        public void Moq_DbSet_can_be_used_for_ToString_on_SqlQuery()
        {
            var mockSqlQuery = new Mock<DbSqlQuery>();
            mockSqlQuery.Setup(m => m.ToString()).Returns("Hello World!");

            Assert.Equal("Hello World!", mockSqlQuery.Object.ToString());
            mockSqlQuery.Verify(m => m.ToString(), Times.Once());
        }

        [Fact]
        public async Task Moq_DbSet_can_be_used_for_FindAsync()
        {
            var mockSet = new Mock<DbSet>();
            var product = new Product();
            mockSet.Setup(m => m.FindAsync(It.IsAny<object[]>())).Returns(Task.FromResult<object>(product));

            Assert.Same(product, await mockSet.Object.FindAsync(1));
            mockSet.Verify(m => m.FindAsync(1), Times.Once());
        }

        [Fact]
        public async Task Moq_DbSet_can_be_used_for_async_SqlQuery()
        {
            var mockSqlQuery = new Mock<DbSqlQuery> { CallBase = true };
            var products = new[] { new Product { Id = 1 }, new Product { Id = 2 } };
            mockSqlQuery.As<IDbAsyncEnumerable>()
                .Setup(m => m.GetAsyncEnumerator())
                .Returns(new InMemoryDbAsyncEnumerator<Product>(((IEnumerable<Product>)products).GetEnumerator()));

            var mockSet = new Mock<DbSet>();
            var query = "not a real query";
            var parameters = new object[] { 1, 2 };
            mockSet.Setup(m => m.SqlQuery(query, parameters)).Returns(mockSqlQuery.Object);

            Assert.Equal(
                new[] { 1, 2 }, (await mockSet.Object.SqlQuery(query, parameters).ToListAsync()).OfType<Product>().Select(p => p.Id));
            mockSet.Verify(m => m.SqlQuery(query, parameters), Times.Once());
        }

        // Works around an issue with Moq that won't let the explicit implementation of IQueryable<T> on DbSet
        // be configured even when using As().
        public abstract class MockableDbSetWithIQueryable<T> : DbSet, IQueryable<T>, IDbAsyncEnumerable<T>
            where T : class
        {
            public abstract IEnumerator<T> GetEnumerator();
            public abstract Expression Expression { get; }
            public abstract IQueryProvider Provider { get; }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }

            public abstract IDbAsyncEnumerator<T> GetAsyncEnumerator();

            IDbAsyncEnumerator IDbAsyncEnumerable.GetAsyncEnumerator()
            {
                return GetAsyncEnumerator();
            }
        }
    }
}