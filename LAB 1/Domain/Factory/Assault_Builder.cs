using System;

namespace LAB1
{
    public class AssaultBuilder : PlayerBuilder
    {
        private Random rnd = new Random();

        public AssaultBuilder()
        {
            player = new Player(Role.Assault,
                RoleRestrictions.AssaultPrimary[0],
                RoleRestrictions.AssaultSecondary[0],
                Gadget.Grenade);
        }

        public override void SetRole() => player.role = Role.Assault;
        public override void SetPrimaryWeapon() => player.primary_weapon = RoleRestrictions.AssaultPrimary[rnd.Next(RoleRestrictions.AssaultPrimary.Length)];
        public override void SetSecondaryWeapon() => player.secondary_weapon = RoleRestrictions.AssaultSecondary[rnd.Next(RoleRestrictions.AssaultSecondary.Length)];
        public override void SetGadget() => player.gadget = Gadget.Grenade;
    }
}