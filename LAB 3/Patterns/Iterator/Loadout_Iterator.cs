using System;
using System.Collections;
using System.Collections.Generic;
using models; 

namespace patterns.Iterator
{
    public interface ILoadoutIterator
    {
        bool HasNext();
        string Next();
        void Reset();
    }

    public class PrimaryWeaponIterator : ILoadoutIterator
    {
        private Primary_Weapon[] _weapons;
        private int _position = 0;

        public PrimaryWeaponIterator(Primary_Weapon[] weapons) {  _weapons = weapons; }

        public bool HasNext() { return _position < _weapons.Length; }

        public string Next()
        {
            if (HasNext()) { return $"Primary Weapon: {_weapons[_position++]}"; }
            throw new InvalidOperationException("No more elements");
        }

        public void Reset() { _position = 0; }
    }

    public class SecondaryWeaponIterator : ILoadoutIterator
    {
        private Secondary_Weapon[] _weapons;
        private int _position = 0;

        public SecondaryWeaponIterator(Secondary_Weapon[] weapons) { _weapons = weapons; }

        public bool HasNext() { return _position < _weapons.Length; }

        public string Next()
        {
            if (HasNext()) { return $"Secondary Weapon: {_weapons[_position++]}"; }
            throw new InvalidOperationException("No more elements");
        }

        public void Reset() { _position = 0; }
    }
    public class GadgetIterator : ILoadoutIterator
    {
        private Gadget[] _gadgets;
        private int _position = 0;

        public GadgetIterator(Gadget[] gadgets) { _gadgets = gadgets; }

        public bool HasNext() { return _position < _gadgets.Length; }

        public string Next()
        {
            if (HasNext()) { return $"Gadget: {_gadgets[_position++]}"; }
            throw new InvalidOperationException("No more elements");
        }

        public void Reset() { _position = 0; }
    }
}