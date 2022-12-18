﻿using Katalyst_TDD_Starter.Bags;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Katalyst_TDD_Starter.Test.Bags
{
    [TestClass]
    public class BagBeltShould
    {
        [TestMethod]
        public void Start_with_no_bags()
        {
            var underTest = new BagBelt();

            Assert.IsFalse(underTest.GetBags().Any());
        }

        [TestMethod]
        public void Add_bags()
        {
            var underTest = new BagBelt();
            var firstBagToAdd = new Bag();
            var secondBagToAdd = new Bag(ItemCategory.Metal);

            underTest.AddBag(firstBagToAdd);
            underTest.AddBag(secondBagToAdd);

            var bags = underTest.GetBags();
            Assert.IsTrue(bags.Contains(firstBagToAdd));
            Assert.IsTrue(bags.Contains(secondBagToAdd));
            Assert.AreEqual(2, bags.Count);
        }

        // Add item to empty bags -> Put item in first bag
        // Add item to empty bag with different category -> Put in first bag regardless of category
        // Add item to bags where first bag is full -> Put in second bag
        // Add item to bags where all are full -> Don't add item. Throw exception?
    }
}
