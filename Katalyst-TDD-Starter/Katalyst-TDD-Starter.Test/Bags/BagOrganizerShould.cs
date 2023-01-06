﻿using Katalyst_TDD_Starter.Bags;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace Katalyst_TDD_Starter.Test.Bags
{
    [TestClass]
    public class BagOrganizerShould
    {
        private readonly Mock<IBag> _defaultBag;
        private readonly Mock<IBag> _clothBag;
        private readonly Item _clothItem;

        public BagOrganizerShould()
        {
            _defaultBag = new Mock<IBag>();
            _defaultBag.Setup(x => x.GetCategory()).Returns(ItemCategory.NotSpecified);
            _defaultBag.Setup(x => x.HasSpace()).Returns(true);
            _clothBag = new Mock<IBag>(); 
            _clothBag.Setup(x => x.GetCategory()).Returns(ItemCategory.Cloth);
            _clothBag.Setup(x => x.TakeAllItems()).Returns(new List<Item>());
            _clothBag.Setup(x => x.HasSpace()).Returns(true);

            _clothItem = new Item("ClothItem", ItemCategory.Cloth);
        }

        [TestMethod]
        public void Take_all_items_out_from_bags()
        {
            var underTest = new BagOrganizer();
            _defaultBag.Setup(x => x.TakeAllItems()).Returns(new List<Item>());
            var bags = new List<IBag> { _defaultBag.Object, _clothBag.Object };

            underTest.Organize(bags);

            _defaultBag.Verify(x => x.TakeAllItems(), Times.Once);
            _clothBag.Verify(x => x.TakeAllItems(), Times.Once);
        }

        [TestMethod]
        public void Move_single_cloth_item_into_first_slot_cloth_bag()
        {
            var underTest = new BagOrganizer();
            _defaultBag.Setup(x => x.TakeAllItems()).Returns(new List<Item> { _clothItem });
            var bags = new List<IBag> { _clothBag.Object, _defaultBag.Object };

            underTest.Organize(bags);

            _defaultBag.Verify(x => x.AddItem(It.IsAny<Item>()), Times.Never);
            _clothBag.Verify(x => x.AddItem(_clothItem), Times.Once());
        }

        [TestMethod]
        public void Move_single_cloth_item_into_second_slot_cloth_bag()
        {
            var underTest = new BagOrganizer();
            _defaultBag.Setup(x => x.TakeAllItems()).Returns(new List<Item> { _clothItem });
            var bags = new List<IBag> { _defaultBag.Object, _clothBag.Object };

            underTest.Organize(bags);

            _defaultBag.Verify(x => x.AddItem(It.IsAny<Item>()), Times.Never);
            _clothBag.Verify(x => x.AddItem(_clothItem), Times.Once());
        }

        [TestMethod]
        public void Put_cloth_item_into_first_bag()
        {
            var underTest = new BagOrganizer();
            _defaultBag.Setup(x => x.TakeAllItems()).Returns(new List<Item> { _clothItem });
            var bags = new List<IBag> { _defaultBag.Object };

            underTest.Organize(bags);

            _defaultBag.Verify(x => x.AddItem(_clothItem), Times.Once);
        }

        [TestMethod]
        public void Put_cloth_item_into_first_bag_with_space()
        {
            var underTest = new BagOrganizer();
            _defaultBag.Setup(x => x.TakeAllItems()).Returns(new List<Item> { _clothItem });
            _defaultBag.Setup(x => x.HasSpace()).Returns(false);
            var herbBag = new Mock<IBag>();
            herbBag.Setup(x => x.GetCategory()).Returns(ItemCategory.Herb);
            herbBag.Setup(x => x.TakeAllItems()).Returns(new List<Item>());
            herbBag.Setup(x => x.HasSpace()).Returns(true);
            var bags = new List<IBag> { _defaultBag.Object, herbBag.Object };

            underTest.Organize(bags);

            _defaultBag.Verify(x => x.AddItem(It.IsAny<Item>()), Times.Never);
            herbBag.Verify(x => x.AddItem(_clothItem), Times.Once);
        }

        // What still needs doing?
        //TODO Don't move items from first bag (Only one bag? First bag is same category?)
        //TODO Don't put into full bag
        //TODO Moving multiple items
        //TODO Alphabetize takenItems instead of making bags do it?
        //TODO Item Categories other than Cloth
        //TODO Move organisation into a seperate class? Or make SortableItemList? 
    }
}
