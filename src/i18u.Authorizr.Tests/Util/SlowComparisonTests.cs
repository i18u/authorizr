using System;
using System.Linq;
using i18u.Authorizr.Core.Util;
using NUnit.Framework;

namespace i18u.Authorizr.Tests.Util
{
    public class SlowComparisonTests
    {
        [Test]
        public void Collection_SlowEquals_Self()
        {
            var slowComparer = new SlowComparer();
            var itemListA = new[] { 1, 2, 3, 4, 5 };

            var result = slowComparer.Equals(itemListA, itemListA);
            Assert.IsTrue(result, "Slow Comparison failed between two lists that are the same.");
        }

        [Test]
        public void Collection_NotSlowEquals_Null()
        {
            var slowComparer = new SlowComparer();
            var itemListA = new[] { 1, 2, 3, 4, 5 };

            var result = slowComparer.Equals(itemListA, null);
            Assert.IsFalse(result, "Slow Comparison failed to detect the difference between a collection and null.");
        }

        [Test]
        public void Collection_NotSlowEquals_DifferentCollection()
        {
            var slowComparer = new SlowComparer();
            var itemListA = new[] { 1, 2, 3, 4, 5 };
            var itemListB = new[] { 6, 7, 8, 9, 0 };

            var result = slowComparer.Equals(itemListA, itemListB);
            Assert.IsFalse(result, "Slow Comparison failed to detect the difference between collections with differing items.");
        }

        [Test]
        public void Comparison_DoesNotThrowWhen_CollectionSizesDiffer()
        {
            var slowComparer = new SlowComparer();
            var itemListA = new[] { 1, 2, 3, 4, 5, 6 };
            var itemListB = new[] { 6, 7, 8, 9, 0 };

            Assert.DoesNotThrow(() =>
            {
                slowComparer.Equals(itemListA, itemListB);
            }, "Slow Comparison threw an exception when items differed in size.");
        }

        [Test]
        public void Comparison_DoesNotThrowWhen_CollectionSizesDiffer2()
        {
            var slowComparer = new SlowComparer();
            var itemListA = new[] { 1, 2, 3, 4, 5 };
            var itemListB = new[] { 5, 6, 7, 8, 9, 0 };

            Assert.DoesNotThrow(() =>
            {
                slowComparer.Equals(itemListA, itemListB);
            }, "Slow Comparison threw an exception when items differed in size.");
        }

        [Test]
        public void Comparison_DoesNotThrowWhen_ItemTypesDiffer()
        {
            var slowComparer = new SlowComparer();
            var itemListA = new[] { Tuple.Create(1), Tuple.Create(2), Tuple.Create(3) };
            var itemListB = new[] { "1", "2", "3" };

            Assert.DoesNotThrow(() =>
            {
                slowComparer.Equals<object>(itemListA, itemListB);
            }, "Slow Comparison does not handle item lists of differing types.");
        }

        [Test]
        public void Method_GetItemAtOrDefault_GetsCorrectItem()
        {
            var slowComparer = new SlowComparer();
            var itemList = new[] { 1, 2, 3, 4, 5 };
            var item = slowComparer.GetItemAtOrDefault(itemList, 2); // Points to 3

            Assert.That(item, Is.Not.Null, "Item was null when retrieving from collection using GetItemAtOrDefault.");
            Assert.That(item, Is.EqualTo(3), $"Item was wrong when retrieving from collection using GetItemAtOrDefault - Expected: 3, Actual: {item}");
        }

        [Test]
        public void Method_GetItemAtOrDefault_GetsDefault()
        {
            var slowComparer = new SlowComparer();
            var itemList = new[] { 1, 2, 3, 4, 5 };
            var item = slowComparer.GetItemAtOrDefault(itemList, 5); // Outside bounds

            Assert.That(item, Is.EqualTo(default(int)), "Item was not default(int) when retrieving from collection using GetItemAtOrDefault.");
        }

        [Test]
        public void Comparison_AllItemsCompared_WhenNoneEqual()
        {
            var slowComparer = new SlowComparer();
            var itemListA = new[] { 1, 2, 3, 4, 5 };
            var itemListB = new[] { 6, 7, 8, 9, 0 };
            var comparisonCount = 0;

            var counts = new[] { itemListA.Length, itemListB.Length };
            var largestCount = counts.Max();

            slowComparer.Compared += (sender, args) =>
            {
                comparisonCount++;
            };

            slowComparer.Equals(itemListA, itemListB);
            Assert.That(comparisonCount, Is.EqualTo(largestCount), "Slow comparisons must compare all elements in the provided lists.");
        }

        [Test]
        public void Comparison_AllItemsCompared_WhenAllEqual()
        {
            var slowComparer = new SlowComparer();
            var itemListA = new[] { 1, 2, 3, 4, 5 };
            var itemListB = new[] { 1, 2, 3, 4, 5 };
            var comparisonCount = 0;

            var counts = new[] { itemListA.Length, itemListB.Length };
            var largestCount = counts.Max();

            slowComparer.Compared += (sender, args) =>
            {
                comparisonCount++;
            };

            slowComparer.Equals(itemListA, itemListB);
            Assert.That(comparisonCount, Is.EqualTo(largestCount), "Slow comparisons must compare all elements in the provided lists.");
        }
    }
}