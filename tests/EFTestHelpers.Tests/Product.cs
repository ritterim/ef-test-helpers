// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

namespace RimDev.EFTestHelpers.Tests
{
    public class Product : ProductBase
    {
        public string CategoryId { get; set; }
        public Category Category { get; set; }
    }
}