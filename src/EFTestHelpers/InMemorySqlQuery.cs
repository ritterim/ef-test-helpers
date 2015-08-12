// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using System.Collections.Generic;
using System.Data.Entity.Infrastructure;

namespace RimDev.EFTestHelpers
{
    public class InMemorySqlQuery<T> : DbSqlQuery<T>, IDbAsyncEnumerable<T>
        where T : class
    {
        private readonly IEnumerable<T> _data;

        public InMemorySqlQuery(IEnumerable<T> data)
        {
            _data = data;
        }

        public override IEnumerator<T> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        public IDbAsyncEnumerator<T> GetAsyncEnumerator()
        {
            return new InMemoryDbAsyncEnumerator<T>(_data.GetEnumerator());
        }

        public override string ToString()
        {
            return "An in-memory SqlQuery";
        }
    }
}