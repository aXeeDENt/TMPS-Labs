using System.Collections.Generic;

namespace LAB1
{
    public class Player_Pool
    {
        private List<Player> player_pool = new List<Player>();

        public Player Get()
        {
            if (player_pool.Count != 0)
            {
                Player player = player_pool[0];
                player_pool.RemoveAt(0);
                return player;
            }
            return new Player(default, default, default, default);
        }

        public void Release(Player player)
        {
            if (!player_pool.Contains(player))
            {
                // Ensure role-specific weapons
                if (player.role == Role.Sniper)
                {
                    player.primary_weapon = RoleRestrictions.SniperPrimary[0];
                    player.secondary_weapon = RoleRestrictions.SniperSecondary[0];
                }
                else if (player.role == Role.Assault)
                {
                    player.primary_weapon = RoleRestrictions.AssaultPrimary[0];
                    player.secondary_weapon = RoleRestrictions.AssaultSecondary[0];
                }
                player_pool.Add(player);
            }
        }
    }
}