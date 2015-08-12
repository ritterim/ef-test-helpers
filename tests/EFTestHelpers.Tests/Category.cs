// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

using System.Collections.Generic;

namespace RimDev.EFTestHelpers.Tests
{
    public class Category
    {
        public Category()
        {
            Products = new List<Product>();
        }

        public Category(string id)
            : this()
        {
            Id = id;
        }

        public string Id { get; set; }
        public ICollection<Product> Products { get; set; }
        public string DetailedDescription { get; set; }
    }
}
