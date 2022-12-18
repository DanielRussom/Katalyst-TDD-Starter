﻿namespace Katalyst_TDD_Starter.Bags
{
    public class BagBelt
    {
        private List<Bag> _storedBags;

        public BagBelt()
        {
            _storedBags = new List<Bag>();
        }

        public List<Bag> GetBags()
        {
            return _storedBags;
        }
        
        public void AddBag(Bag bagToAdd)
        {
            _storedBags.Add(bagToAdd);
        }

        public void AddItem(Item itemToAdd)
        {
            throw new NotImplementedException();
        }

        public void Organise()
        {
            throw new NotImplementedException();
        }

        public int BagsCount()
        {
            throw new NotImplementedException();
        }

        public List<Item> ItemsInBag(int bagIndex)
        {
            throw new NotImplementedException();
        }
    }
}