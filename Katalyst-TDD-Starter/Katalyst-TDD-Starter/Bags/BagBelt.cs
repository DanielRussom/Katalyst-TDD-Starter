﻿using Katalyst_TDD_Starter.Test.Bags;

namespace Katalyst_TDD_Starter.Bags
{
    public class BagBelt
    {
        public List<StorageBag> Bags { get; set; } = new();

        public void AddBag(StorageBag input)
        {
            Bags.Add(input);
        }

        public void AddItem(Item item)
        {
            var openBags = Bags.Where(x => x.HasSpace()).ToList();
            if (openBags.Any())
            {
                openBags.First().Add(item);
                return;
            }
        }

        public void OrganiseBags()
        {
            var itemCategories = Enum.GetValues(typeof(ItemCategory));

            foreach(ItemCategory category in itemCategories)
            {
                var unsortedItems = Bags[0].Items.Where(x => x.Category == category).ToList();
                var categoryBag = Bags.Where(x => x.Category == category).FirstOrDefault();

                if (categoryBag != null)
                {
                    foreach (var item in unsortedItems)
                    {
                        categoryBag.Items.Add(item);
                        Bags[0].Remove(item);
                    }
                }
            }
        }
    }
}