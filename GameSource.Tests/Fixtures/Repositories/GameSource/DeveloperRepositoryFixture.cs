using AutoFixture;
using GameSource.Infrastructure;
using GameSource.Infrastructure.Repositories.GameSource;
using GameSource.Models.GameSource;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace GameSource.Tests.Fixtures.Repositories.GameSource
{
    public class DeveloperRepositoryFixture
    {
        public DeveloperRepository developerRepo;
        public Mock<GameSource_DBContext> mockContext;
        public Mock<DbSet<Developer>> mockDbSet;
        public DbContextOptions<GameSource_DBContext> options;
        public IFixture fixture;

        public DeveloperRepositoryFixture()
        {
            options = new DbContextOptionsBuilder<GameSource_DBContext>().UseSqlServer().Options;
            mockContext = new Mock<GameSource_DBContext>(options);
            mockDbSet = new Mock<DbSet<Developer>>();
            developerRepo = new DeveloperRepository(mockContext.Object);

            fixture = new Fixture();
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>()
                .ToList()
                .ForEach(b => fixture.Behaviors.Remove(b));
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        public Mock<DbSet<Developer>> SetupMockDbSet(IEnumerable<Developer> developerList)
        {
            var mocks = developerList.AsQueryable();
            var mockDbSet = new Mock<DbSet<Developer>>();

            mockDbSet.As<IQueryable<Developer>>().Setup(m => m.Provider).Returns(new TestAsyncQueryProvider<Developer>(mocks.Provider));
            mockDbSet.As<IQueryable<Developer>>().Setup(m => m.Expression).Returns(mocks.Expression);
            mockDbSet.As<IQueryable<Developer>>().Setup(m => m.ElementType).Returns(mocks.ElementType);
            mockDbSet.As<IQueryable<Developer>>().Setup(m => m.GetEnumerator()).Returns(mocks.GetEnumerator());
            mockDbSet.As<IAsyncEnumerable<Developer>>().Setup(x => x.GetAsyncEnumerator(It.IsAny<CancellationToken>())).Returns(new TestAsyncEnumerator<Developer>(mocks.GetEnumerator()));

            return mockDbSet;
        }

        internal class TestAsyncQueryProvider<TEntity> : IAsyncQueryProvider
        {
            private readonly IQueryProvider _inner;

            internal TestAsyncQueryProvider(IQueryProvider inner)
            {
                _inner = inner;
            }

            public IQueryable CreateQuery(Expression expression)
            {
                return new TestAsyncEnumerable<TEntity>(expression);
            }

            public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
            {
                return new TestAsyncEnumerable<TElement>(expression);
            }

            public object Execute(Expression expression)
            {
                return _inner.Execute(expression);
            }

            public TResult Execute<TResult>(Expression expression)
            {
                return _inner.Execute<TResult>(expression);
            }

            public IAsyncEnumerable<TResult> ExecuteAsync<TResult>(Expression expression)
            {
                return new TestAsyncEnumerable<TResult>(expression);
            }

            TResult IAsyncQueryProvider.ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
            {
                throw new System.NotImplementedException();
            }
        }

        internal class TestAsyncEnumerable<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
        {
            public TestAsyncEnumerable(IEnumerable<T> enumerable)
                : base(enumerable)
            { }

            public TestAsyncEnumerable(Expression expression)
                : base(expression)
            { }

            public IAsyncEnumerator<T> GetAsyncEnumerator()
            {
                return new TestAsyncEnumerator<T>(this.AsEnumerable().GetEnumerator());
            }

            IQueryProvider IQueryable.Provider
            {
                get { return new TestAsyncQueryProvider<T>(this); }
            }
            public IAsyncEnumerator<T> GetEnumerator()
            {
                return GetAsyncEnumerator();
            }

            public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default)
            {
                throw new System.NotImplementedException();
            }
        }

        internal class TestAsyncEnumerator<T> : IAsyncEnumerator<T>
        {
            private readonly IEnumerator<T> _inner;

            public TestAsyncEnumerator(IEnumerator<T> inner)
            {
                _inner = inner;
            }

            public void Dispose()
            {
                _inner.Dispose();
            }

            public T Current
            {
                get { return _inner.Current; }
            }
            public Task<bool> MoveNext(CancellationToken cancellationToken)
            {
                return Task.FromResult(_inner.MoveNext());
            }

            public ValueTask<bool> MoveNextAsync()
            {
                throw new System.NotImplementedException();
            }

            public ValueTask DisposeAsync()
            {
                throw new System.NotImplementedException();
            }
        }
    }
}
