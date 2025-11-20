using System;
using models;
using utilities;

namespace factories
{
    public static class PlayerFactory
    {
        // Create default players using valid defaults from RoleRestrictions
        public static Player CreatePlayer(Role r)
        {
            if (r == Role.Assault)
                return new Player(r, RoleRestrictions.AssaultPrimary[0], RoleRestrictions.AssaultSecondary[0], Gadget.Grenade);
            else
                return new Player(r, RoleRestrictions.SniperPrimary[0], RoleRestrictions.SniperSecondary[0], Gadget.Medkit);
        }
    }
}
