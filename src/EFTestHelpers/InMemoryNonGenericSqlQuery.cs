// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;

namespace RimDev.EFTestHelpers
{
    public class InMemoryNonGenericSqlQuery<T> : DbSqlQuery, IEnumerable, IDbAsyncEnumerable
        where T : class
    {
        private readonly IEnumerable<T> _data;

        public InMemoryNonGenericSqlQuery(IEnumerable<T> data)
        {
            _data = data;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        public IDbAsyncEnumerator GetAsyncEnumerator()
        {
            return new InMemoryDbAsyncEnumerator<T>(_data.GetEnumerator());
        }

        public override string ToString()
        {
            return "An in-memory SqlQuery";
        }
    }
}