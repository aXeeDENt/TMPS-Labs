using System;
using System.Collections.Generic;
using models; 

namespace patterns.Iterator
{
    public interface ILoadoutAggregate
    {
        ILoadoutIterator CreateIterator();
        void AddItem(object item);
        int Count { get; }
    }

    public class PrimaryWeaponAggregate : ILoadoutAggregate
    {
        private List<Primary_Weapon> _weapons = new List<Primary_Weapon>();

        public void AddItem(object item) { if (item is Primary_Weapon weapon) { _weapons.Add(weapon); } }

        public ILoadoutIterator CreateIterator() { return new PrimaryWeaponIterator(_weapons.ToArray()); }

        public int Count => _weapons.Count;
    }

    public class SecondaryWeaponAggregate : ILoadoutAggregate
    {
        private List<Secondary_Weapon> _weapons = new List<Secondary_Weapon>();

        public void AddItem(object item) { if (item is Secondary_Weapon weapon) {  _weapons.Add(weapon); } }

        public ILoadoutIterator CreateIterator() { return new SecondaryWeaponIterator(_weapons.ToArray());}

        public int Count => _weapons.Count;
    }

    public class GadgetAggregate : ILoadoutAggregate
    {
        private List<Gadget> _gadgets = new List<Gadget>();

        public void AddItem(object item) { if (item is Gadget gadget) { _gadgets.Add(gadget); } }

        public ILoadoutIterator CreateIterator() {  return new GadgetIterator(_gadgets.ToArray()); }

        public int Count => _gadgets.Count;
    }

    public class LoadoutCollectionViewer
    {
        public static void ViewAllLoadouts()
        {
            Console.WriteLine("\nVIEWING ALL LOADOUT OPTIONS (Iterator Pattern)");
            
            var primaryAggregate = new PrimaryWeaponAggregate();
            foreach (Primary_Weapon weapon in Enum.GetValues(typeof(Primary_Weapon))){primaryAggregate.AddItem(weapon);}
            
            var primaryIterator = primaryAggregate.CreateIterator();
            Console.WriteLine($"Primary Weapons ({primaryAggregate.Count} total):");
            while (primaryIterator.HasNext()){Console.WriteLine($"  - {primaryIterator.Next()}");}

            var secondaryAggregate = new SecondaryWeaponAggregate();
            foreach (Secondary_Weapon weapon in Enum.GetValues(typeof(Secondary_Weapon))) { secondaryAggregate.AddItem(weapon); }
            
            var secondaryIterator = secondaryAggregate.CreateIterator();
            Console.WriteLine($"\nSecondary Weapons ({secondaryAggregate.Count} total):");
            while (secondaryIterator.HasNext()) { Console.WriteLine($"  - {secondaryIterator.Next()}"); }

            var gadgetAggregate = new GadgetAggregate();
            foreach (Gadget gadget in Enum.GetValues(typeof(Gadget))) { gadgetAggregate.AddItem(gadget); }
            
            var gadgetIterator = gadgetAggregate.CreateIterator();
            Console.WriteLine($"\nGadgets ({gadgetAggregate.Count} total):");
            while (gadgetIterator.HasNext()) { Console.WriteLine($"  - {gadgetIterator.Next()}"); }
        }
    }
}