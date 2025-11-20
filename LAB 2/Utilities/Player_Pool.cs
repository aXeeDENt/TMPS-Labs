using System;
using System.Collections.Generic;
using models;

namespace utilities
{
    // Simple object pool (adapted from your original idea).
    public class PlayerPool
    {
        private readonly List<Player> pool = new List<Player>();

        private Player CreateDefaultPlayer()
        {
            // default is Assault defaults
            return new Player(Role.Assault, RoleRestrictions.AssaultPrimary[0], RoleRestrictions.AssaultSecondary[0], Gadget.Grenade);
        }

        public Player Get()
        {
            if (pool.Count > 0)
            {
                Player p = pool[0];
                pool.RemoveAt(0);
                return p;
            }
            return CreateDefaultPlayer();
        }

        public void Release(Player player)
        {
            // Reset to role default before reusing
            if (player.role == Role.Sniper)
            {
                player.primary_weapon = RoleRestrictions.SniperPrimary[0];
                player.secondary_weapon = RoleRestrictions.SniperSecondary[0];
                player.gadget = Gadget.Medkit;
            }
            else if (player.role == Role.Assault)
            {
                player.primary_weapon = RoleRestrictions.AssaultPrimary[0];
                player.secondary_weapon = RoleRestrictions.AssaultSecondary[0];
                player.gadget = Gadget.Grenade;
            }
            pool.Add(player);
        }
    }
}
