﻿namespace Katalyst_TDD_Starter.Bags
{
    public class BagBelt
    {
        public List<StorageBag> Bags { get; set; } = new();

        public void AddBag(StorageBag input)
        {
            Bags.Add(input);
        }

        public void AddItem(string item)
        {
            Bags[0].Add(item);
        }
    }
}