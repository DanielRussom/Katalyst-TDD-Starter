﻿using Katalyst_TDD_Starter.Bags;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;

namespace Katalyst_TDD_Starter.Test.Bags
{
    [TestClass]
    public class BagBeltShould
    {
        private readonly Item _testItem = new("TestItem", ItemCategory.Cloth);
        private readonly BagBelt _underTest = new();
        private readonly Mock<IBag> _bagWithSpace = new();
        private readonly Mock<IBag> _bagWithoutSpace = new();

        public BagBeltShould()
        {
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
            //TODO Can we take the exception without asserting?
            _bagWithoutSpace.Verify(x => x.AddItem(_testItem), Times.Never);
            Assert.AreEqual("All bags are full, no more items can be added!", exception.Message);
        }

        [TestMethod]
        public void Tell_bags_to_organise()
        {
            var bag = new Mock<IBag>();
            _underTest.AddBag(bag.Object);

            _underTest.Organise();

            bag.Verify(x => x.Organise(), Times.Once);
        }
        // Test for multiple bags
        // Bag belt should... do something to move items into correct bag

    }
}
