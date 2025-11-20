using System;
using models;
using utilities;

namespace patterns.Proxy
{
    // Proxy wraps an IPlayer and validates assignments before applying them.
    public class PlayerProxy
    {
        private readonly IPlayer _player;

        public PlayerProxy(IPlayer player)
        {
            _player = player;
        }

        public void SetPrimary(Primary_Weapon w)
        {
            if (RoleRestrictions.IsValidPrimary(_player.role, w))
            {
                _player.primary_weapon = w;
                Console.WriteLine($"[Proxy] Primary set to {w} for role {_player.role}");
            }
            else
            {
                Console.WriteLine($"[Proxy] INVALID primary {w} for role {_player.role} — assignment blocked.");
            }
        }

        public void SetSecondary(Secondary_Weapon s)
        {
            if (RoleRestrictions.IsValidSecondary(_player.role, s))
            {
                _player.secondary_weapon = s;
                Console.WriteLine($"[Proxy] Secondary set to {s} for role {_player.role}");
            }
            else
            {
                Console.WriteLine($"[Proxy] INVALID secondary {s} for role {_player.role} — assignment blocked.");
            }
        }

        public void SetGadget(Gadget g)
        {
            _player.gadget = g;
            Console.WriteLine($"[Proxy] Gadget set to {g}");
        }
    }
}
