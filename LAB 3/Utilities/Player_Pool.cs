using System;
using System.Collections.Generic;
using models;

namespace utilities
{
    public class Player_Pool
    {
        private readonly List<Player> pool = new List<Player>();
        private Player Create_Default_Player()
        {
            return new Player(Role.Assault, Role_Restrictions.Assault_Primary[0], Role_Restrictions.Assault_Secondary[0], Gadget.Grenade);
        }
        public Player Get()
        {
            if (pool.Count > 0)
            {
                Player p = pool[0];
                pool.RemoveAt(0);
                return p;
            }
            return Create_Default_Player();
        }
        public void Release(Player player)
        {
            if (player.role == Role.Sniper)
            {
                player.primary_weapon = Role_Restrictions.Sniper_Primary[0];
                player.secondary_weapon = Role_Restrictions.Sniper_Secondary[0];
                player.gadget = Gadget.Medkit;
            }
            else if (player.role == Role.Assault)
            {
                player.primary_weapon = Role_Restrictions.Assault_Primary[0];
                player.secondary_weapon = Role_Restrictions.Assault_Secondary[0];
                player.gadget = Gadget.Grenade;
            }
            pool.Add(player);
        }
    }
}