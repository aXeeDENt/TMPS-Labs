using System;
using models;
using utilities;
namespace patterns.Proxy
{
    public class Player_Proxy
    {
        private readonly IPlayer _player;

        public Player_Proxy(IPlayer player) { _player = player; }
        public void Set_Primary(Primary_Weapon w)
        {
            if (Role_Restrictions.Is_Valid_Primary(_player.role, w))
            {
                _player.primary_weapon = w;
                Console.WriteLine($"[Proxy] Primary set to {w} for role {_player.role}");
            }
            else { Console.WriteLine($"[Proxy] INVALID primary {w} for role {_player.role} — assignment blocked."); }
        }
        public void Set_Secondary(Secondary_Weapon s)
        {
            if (Role_Restrictions.Is_Valid_Secondary(_player.role, s))
            {
                _player.secondary_weapon = s;
                Console.WriteLine($"[Proxy] Secondary set to {s} for role {_player.role}");
            }
            else { Console.WriteLine($"[Proxy] INVALID secondary {s} for role {_player.role} — assignment blocked."); }
        }
        public void Set_Gadget(Gadget g)
        {
            _player.gadget = g;
            Console.WriteLine($"[Proxy] Gadget set to {g}");
        }
    }
}