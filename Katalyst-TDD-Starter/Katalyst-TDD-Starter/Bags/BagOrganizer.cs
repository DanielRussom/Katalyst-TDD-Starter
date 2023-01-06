﻿namespace Katalyst_TDD_Starter.Bags
{
    public interface IBagOrganizer
    {
        void Organize(List<IBag> expectedBags);
    }

    public class BagOrganizer : IBagOrganizer
    {
        public void Organize(List<IBag> bagsToOrganize)
        {
            var allItems = new List<Item>();
            foreach(var bag in bagsToOrganize)
            {
                allItems.AddRange(bag.TakeAllItems());
            }

            foreach(var item in allItems)
            {
                bagsToOrganize[1].AddItem(item);
            }
        }
    }
}