using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;

namespace RimDev.EFTestHelpers
{
    public abstract class InMemoryContextBase
    {
        protected ConcurrentDictionary<Type, object> DbSetCache { get; private set; } = new ConcurrentDictionary<Type, object>();

        public Func<Exception> ThrowException { get; set; }

        protected virtual DbSet<T> Set<T>() where T : class
        {
            return (DbSet<T>)DbSetCache.GetOrAdd(typeof(T), x => new InMemoryDbSet<T>(find: FindByIdentity));
        }

        public virtual void ResetDbSet<T>() where T : class
        {
            object set;
            DbSetCache.TryRemove(typeof(T), out set);
        }

        public virtual int SaveChanges()
        {
            if (ThrowException != null)
                throw ThrowException();

            return 0;
        }

        protected virtual T FindByIdentity<T>(IEnumerable<T> entity, object[] keyValues)
            where T : class
        {
            if (keyValues.Length > 1)
                throw new NotSupportedException("This implementation only supports single-valued IDs.");

            var keyValue = keyValues[0];

            var keyName = typeof(T).Name + "Id";
            var keyProperty = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .FirstOrDefault(x => string.Equals(keyName, x.Name) || string.Equals("Id", x.Name));

            if (keyProperty == null)
                throw new MissingMemberException("Could not find a property named '" + keyName + "'.");

            return entity.FirstOrDefault(x => Equals(keyProperty.GetValue(x, null), keyValue));
        }

    }
}
