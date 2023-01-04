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
        private readonly Item _testItem = new("TestItem", ItemCategory.Cloth);
        private readonly Mock<IBagOrganizer> bagsOrganizer = new();
        private readonly BagBelt _underTest;
        private readonly Mock<IBag> _bagWithSpace = new();
        private readonly Mock<IBag> _bagWithoutSpace = new();

        public BagBeltShould()
        {
            _underTest = new BagBelt(bagsOrganizer.Object);
            _bagWithSpace.Setup(x => x.HasSpace()).Returns(true);
            _bagWithSpace.Setup(x => x.TakeAllItems()).Returns(new List<Item>());

            _bagWithoutSpace.Setup(x => x.HasSpace()).Returns(false);
            _bagWithoutSpace.Setup(x => x.TakeAllItems()).Returns(new List<Item>());

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
        public void Tell_the_organizer_to_organise_bags()
        {
            var expectedBags = new List<IBag> { _bagWithoutSpace.Object, _bagWithSpace.Object };
            _underTest.AddBag(_bagWithoutSpace.Object);
            _underTest.AddBag(_bagWithSpace.Object);

            _underTest.Organise();

            bagsOrganizer.Verify(x => x.Organize(expectedBags), Times.Once);
        }

        //[TestMethod]
        //public void Tell_bags_to_organise()
        //{
        //    _underTest.AddBag(_bagWithoutSpace.Object);
        //    _underTest.AddBag(_bagWithSpace.Object);

        //    _underTest.Organise();

        //    _bagWithoutSpace.Verify(x => x.Organise(), Times.Once);
        //    _bagWithSpace.Verify(x => x.Organise(), Times.Once);
        //}

        //[TestMethod]
        //public void Move_cloth_item_into_second_slot_cloth_bag()
        //{
        //    var clothItem = new Item(string.Empty, ItemCategory.Cloth);

        //    var defaultBag = CreateBag(ItemCategory.NotSpecified);
        //    defaultBag.Setup(x => x.TakeAllItems()).Returns(new List<Item> { clothItem });
        //    _underTest.AddBag(defaultBag.Object);
        //    _underTest.AddBag(defaultBag.Object);
        //    var clothBag = CreateBag(ItemCategory.Cloth);
        //    _underTest.AddBag(clothBag.Object);

        //    _underTest.Organise();

        //    defaultBag.Verify(x => x.TakeAllItems(), Times.Once);
        //    clothBag.Verify(x => x.AddItem(clothItem), Times.Once);
        //}

        //[TestMethod]
        //public void Move_cloth_item_into_third_slot_cloth_bag()
        //{
        //    var clothItem = new Item(string.Empty, ItemCategory.Cloth);

        //    var defaultBag = CreateBag(ItemCategory.NotSpecified);
        //    defaultBag.Setup(x => x.TakeAllItems()).Returns(new List<Item> { clothItem });
        //    _underTest.AddBag(defaultBag.Object);
        //    var herbBag = CreateBag(ItemCategory.Herb);
        //    _underTest.AddBag(herbBag.Object);
        //    var clothBag = CreateBag(ItemCategory.Cloth);
        //    _underTest.AddBag(clothBag.Object);

        //    _underTest.Organise();

        //    defaultBag.Verify(x => x.TakeAllItems(), Times.Once);
        //    herbBag.Verify(x => x.AddItem(It.IsAny<Item>()), Times.Never);
        //    clothBag.Verify(x => x.AddItem(clothItem), Times.Once);
        //}

        //private static Mock<IBag> CreateBag(ItemCategory category)
        //{
        //    var bag = new Mock<IBag>();
        //    bag.Setup(x => x.HasSpace()).Returns(true);
        //    bag.Setup(x => x.GetCategory()).Returns(category);
        //    return bag;
        //}

        //[TestMethod]
        //public void Not_move_item_into_full_bag()
        //{
        //    var clothItem = new Item(string.Empty, ItemCategory.Cloth);
        //    _bagWithoutSpace.Setup(x => x.GetCategory()).Returns(ItemCategory.Cloth);
        //    var defaultBag = new Mock<IBag>();
        //    defaultBag.Setup(x => x.GetCategory()).Returns(ItemCategory.NotSpecified);
        //    defaultBag.Setup(x => x.TakeAllItems()).Returns(new List<Item> { clothItem });
        //    _underTest.AddBag(defaultBag.Object);
        //    _underTest.AddBag(_bagWithoutSpace.Object);

        //    _underTest.Organise();

        //    defaultBag.Verify(x => x.TakeAllItems(), Times.Once);
        //    _bagWithoutSpace.Verify(x => x.AddItem(It.IsAny<Item>()), Times.Never);
        //}

        //// What still needs doing?
        ////TODO Don't move items from first bag (Only one bag? First bag is same category?)
        ////TODO Don't put into full bag
        ////TODO Moving multiple items
        ////TODO Alphabetize takenItems instead of making bags do it?
        ////TODO Item Categories other than Cloth
        ////TODO Move organisation into a seperate class? Or make SortableItemList? 
    }
}
