using System;
using models;

namespace patterns.Decorator
{
    // Decorator that adds Group() to a player and keeps a static counter.
    public class PlayerGroupDecorator : IPlayer
    {
        private static int counter = 0;
        private readonly IPlayer _inner;
        private readonly int _group;

        public PlayerGroupDecorator(IPlayer inner)
        {
            _inner = inner;
            counter++;
            _group = counter;
        }

        // Expose wrapped properties
        public Role role { get => _inner.role; set => _inner.role = value; }
        public Primary_Weapon primary_weapon { get => _inner.primary_weapon; set => _inner.primary_weapon = value; }
        public Secondary_Weapon secondary_weapon { get => _inner.secondary_weapon; set => _inner.secondary_weapon = value; }
        public Gadget gadget { get => _inner.gadget; set => _inner.gadget = value; }

        // Decorator-specific method
        public int Group() => _group;

        public void Show()
        {
            // Show group with player state
            Console.Write($"[Group {_group}] ");
            _inner.Show();
        }
    }
}
