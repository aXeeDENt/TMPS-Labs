using System;

namespace LAB1
{
    public class SniperBuilder : PlayerBuilder
    {
        private Random rnd = new Random();

        public SniperBuilder()
        {
            player = new Player(Role.Sniper,
                RoleRestrictions.SniperPrimary[0],
                RoleRestrictions.SniperSecondary[0],
                Gadget.Medkit);
        }

        public override void SetRole() => player.role = Role.Sniper;
        public override void SetPrimaryWeapon() => player.primary_weapon = RoleRestrictions.SniperPrimary[rnd.Next(RoleRestrictions.SniperPrimary.Length)];
        public override void SetSecondaryWeapon() => player.secondary_weapon = RoleRestrictions.SniperSecondary[rnd.Next(RoleRestrictions.SniperSecondary.Length)];
        public override void SetGadget() => player.gadget = Gadget.Medkit;
    }
}