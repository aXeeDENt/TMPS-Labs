namespace LAB1
{
    public class Player_Pool
    {
        private readonly List<Player> pool = new List<Player>();
        private Player CreateDefaultPlayer()
        { return new Player(Role.Assault, RoleRestrictions.AssaultPrimary[0], RoleRestrictions.AssaultSecondary[0], Gadget.Grenade); }
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