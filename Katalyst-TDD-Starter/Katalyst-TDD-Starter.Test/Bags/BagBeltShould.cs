﻿using Katalyst_TDD_Starter.Bags;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Katalyst_TDD_Starter.Test.Bags
{
    [TestClass]
    public class BagBeltShould
    {
        private readonly BagBelt _underTest;
        private readonly Item _testItem = new("TestItem", ItemCategory.Cloth);
        private readonly Mock<IBag> _bagWithSpace = new();
        private readonly Mock<IBag> _bagWithoutSpace = new();
        private readonly Mock<IBagOrganizer> bagsOrganizer = new();

        public BagBeltShould()
        {
            _underTest = new BagBelt(bagsOrganizer.Object);
            _bagWithSpace.Setup(x => x.HasSpace()).Returns(true);
            _bagWithoutSpace.Setup(x => x.HasSpace()).Returns(false);
        }

        [TestMethod]
        public void Start_with_no_bags()
        {
            Assert.IsFalse(_underTest.GetBags().Any());
        }

        [TestMethod]
        public void Add_bags()
        {
            _underTest.AddBag(_bagWithSpace.Object);
            _underTest.AddBag(_bagWithoutSpace.Object);

            var bags = _underTest.GetBags();

            Assert.IsTrue(bags.Contains(_bagWithSpace.Object));
            Assert.IsTrue(bags.Contains(_bagWithoutSpace.Object));
            Assert.AreEqual(2, bags.Count);
        }

        [TestMethod]
        public void Add_item_to_empty_bag()
        {
            _underTest.AddBag(_bagWithSpace.Object);

            _underTest.AddItem(_testItem);

            _bagWithSpace.Verify(x => x.AddItem(_testItem), Times.Once);
        }

        [TestMethod]
        public void Add_item_to_second_bag_when_first_bag_is_full()
        {
            _underTest.AddBag(_bagWithoutSpace.Object);
            _underTest.AddBag(_bagWithSpace.Object);
            
            _underTest.AddItem(_testItem);

            _bagWithoutSpace.Verify(x => x.AddItem(_testItem), Times.Never);
            _bagWithSpace.Verify(x => x.AddItem(_testItem), Times.Once);
        }

        [TestMethod]
        public void Not_add_item_when_all_bags_are_full()
        {
            _underTest.AddBag(_bagWithoutSpace.Object);

            var exception = Assert.ThrowsException<BagException>(() => _underTest.AddItem(_testItem));
            
            _bagWithoutSpace.Verify(x => x.AddItem(_testItem), Times.Never);
            Assert.AreEqual("All bags are full, no more items can be added!", exception.Message);
        }

        [TestMethod]
        public void Tell_the_organizer_to_organize_bags()
        {
            var expectedBags = new List<IBag> { _bagWithoutSpace.Object, _bagWithSpace.Object };
            _underTest.AddBag(_bagWithoutSpace.Object);
            _underTest.AddBag(_bagWithSpace.Object);

            _underTest.Organize();

            bagsOrganizer.Verify(x => x.Organize(expectedBags), Times.Once);
        }

        [TestMethod]
        public void Get_all_items_in_bag()
        {
            _bagWithoutSpace.Setup(x => x.GetItems()).Returns(new List<Item> { _testItem });
            _underTest.AddBag(_bagWithSpace.Object);
            _underTest.AddBag(_bagWithoutSpace.Object);

            var actualItems = _underTest.GetItemsInBag(1);

            Assert.AreEqual(1, actualItems.Count);
            Assert.AreEqual(_testItem, actualItems[0]);
        }
    }
}
