// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Collections;
using System.Collections.Generic;

namespace RimDev.EFTestHelpers.Tests.Extensions
{
    public static class IEnumerableExtentions
    {
        /// <summary>
        /// Creates a <see cref="List{T}" /> from the <see cref="IEnumerable" />.
        /// </summary>
        /// <typeparam name="T"> The type that the elements will be cast to. </typeparam>
        /// <returns> A <see cref="List{T}" /> that contains elements from the input sequence. </returns>
        public static List<T> ToList<T>(this IEnumerable source)
        {
            var list = new List<T>();
            var enumerator = source.GetEnumerator();
            try
            {
                while (enumerator.MoveNext())
                {
                    list.Add((T)enumerator.Current);
                }
            }
            finally
            {
                var asDisposable = enumerator as IDisposable;
                if (asDisposable != null)
                {
                    asDisposable.Dispose();
                }
            }
            return list;
        }
    }
}