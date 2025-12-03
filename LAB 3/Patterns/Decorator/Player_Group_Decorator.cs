using System;
using models;
namespace patterns.Decorator
{
    public class Player_Group_Decorator : IPlayer
    {
        private static int counter = 0;
        private readonly IPlayer _inner;
        private readonly int _group;
        public Player_Group_Decorator(IPlayer inner)
        {
            _inner = inner;
            counter++;
            _group = counter;
        }
        public Role role { get => _inner.role; set => _inner.role = value; }
        public Primary_Weapon primary_weapon { get => _inner.primary_weapon; set => _inner.primary_weapon = value; }
        public Secondary_Weapon secondary_weapon { get => _inner.secondary_weapon; set => _inner.secondary_weapon = value; }
        public Gadget gadget { get => _inner.gadget; set => _inner.gadget = value; }
        public int Group() => _group;
        public void Show()
        {
            Console.Write($"[Group {_group}] ");
            _inner.Show();
        }
    }
}