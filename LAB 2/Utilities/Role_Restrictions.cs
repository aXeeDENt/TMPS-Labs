using System.Linq;
using models;

namespace utilities
{
    public static class RoleRestrictions
    {
        public static Primary_Weapon[] SniperPrimary = { Primary_Weapon.HDR, Primary_Weapon.AX50, Primary_Weapon.AWP };
        public static Secondary_Weapon[] SniperSecondary = { Secondary_Weapon.Glock, Secondary_Weapon.FiveSeven, Secondary_Weapon.Renetti };

        public static Primary_Weapon[] AssaultPrimary = { Primary_Weapon.AK47, Primary_Weapon.M4, Primary_Weapon.KILO };
        public static Secondary_Weapon[] AssaultSecondary = { Secondary_Weapon.Knife, Secondary_Weapon.Sticks, Secondary_Weapon.Hammer };

        public static bool IsValidPrimary(Role r, Primary_Weapon w)
        {
            if (r == Role.Assault) return AssaultPrimary.Contains(w);
            if (r == Role.Sniper) return SniperPrimary.Contains(w);
            return false;
        }

        public static bool IsValidSecondary(Role r, Secondary_Weapon s)
        {
            if (r == Role.Assault) return AssaultSecondary.Contains(s);
            if (r == Role.Sniper) return SniperSecondary.Contains(s);
            return false;
        }
    }
}
