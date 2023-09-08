using System;
using System.Collections.Generic;
using System.Linq;

namespace Knapsack_Solution
{
    class Program
    {
        private static List<Item> _Items = new List<Item>();
        private static Bag _Bag = new Bag();
        static void Main(string[] args)
        {
            


            Bag bag = new Bag();
            int numberOfItemsToAdd = 2;

            for (int s = 1; s <= numberOfItemsToAdd; s++)
            {
                Console.Write($"Please enter the weight for item {s}: ");
                var sWeight = Console.ReadLine();
                var intWeight = int.Parse(sWeight);

                Console.Write($"Please enter the value for item {s}: ");
                var sValue = Console.ReadLine();
                var intValue = int.Parse(sValue);

                bag.Items.Add(new Item(intWeight, intValue));
                bag.Items.Add(new Item() { Weight = intWeight, Value = intValue });

                Console.WriteLine();
            }

            Console.Write($"Please enter the value for weight capacity: ");
            var weightCapacity = Console.ReadLine();
            var intCapacity = int.Parse(weightCapacity); // int.TryParse()
            bag.Capacity = intCapacity;

            Console.WriteLine("Items");
            foreach (var item in bag.Items)
            {
                Console.WriteLine($"Weight = {item.Weight} Value = {item.Value}");
            }

            Console.WriteLine($"Capacity = {bag.Capacity}");

            OutputEfficiency(bag);
            bag.OutputEfficiency();
            OutputBagContents(bag);
            Add5Items();
            _Bag.Capacity = SetCapacity();
        }

        private static void FillBag()
        {
            //Work out what our algorithm is
            //Add the items which are of the most value to the bag to the weight limit

            var orderedItems = _Items.OrderByDescending(item => item.Ratio);

            foreach (var item in orderedItems)
            {
                if (_Bag.RemainingCapacity >= item.Weight)
                {
                    _Bag.AddItem(item);
                }
            }
        }

        private static void OutputBagContents(Bag bag)
        {
            Console.WriteLine($"Items in the bag = {bag.Capacity}");
        }

        private static void OutputEfficiency(Bag bag)
        {          
            Console.WriteLine($"Remaining Capacity = {bag.RemainingCapacity}");
        }

        private static int SetCapacity()
        {
            return SetInteger("Capacity");
        }

        private static void Add5Items()
        {
            for (var itemNumber = 1; itemNumber <=5; itemNumber++)
            {
                Console.WriteLine(string.Format("Item {0}:", itemNumber));

                var weight = AddItemWeight();
                var value = AddItemValue();
                _Items.Add(new Item(weight, value));

            }
        }

        private static int AddItemWeight()
        {
            return SetInteger("\tWeight");
        }

        private static int AddItemValue()
        {
            return SetInteger("\tValue");
        }

        private static int SetInteger(string name)
        {
            Console.Write(string.Format("{0}: ", name));

            if (!int.TryParse(Console.ReadLine(), out int result))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(string.Format("\t{0} must be an integer!", name));
                Console.ForegroundColor = ConsoleColor.White;

                SetInteger(name);
            }

            return result;
        }
    }

 

    public class Item
    {
        public Item()
        {

        }

        public Item(int weight, int value)
        {
            Weight = weight;
            Value = value;
        }

        public int Weight { get; set; }
        public int Value { get; set; }

        public decimal Ratio => Convert.ToDecimal(Weight) / Convert.ToDecimal(Value);
    }

    public class Bag
    {
        public int Capacity { get; set; }
        public List<Item> Items { get; set; }

        public Bag()
        {
            Items = new List<Item>();
        }

        public int RemainingCapacity => Capacity - Items.Sum(item => item.Weight);

        public void AddItems(List<Item> items)
        {
            if (RemainingCapacity - items.Sum(item => item.Weight) < 0)
            {
                throw new Exception("Item cannot be added or weight would be exceeded");
            }
            
            Items = items;
        }

        public void OutputEfficiency()
        {
            Console.WriteLine($"Remaining Capacity = {RemainingCapacity}");
        }

        internal void AddItem(Item item)
        {
            throw new NotImplementedException();
        }
    }
}
