using System.Linq;
using models;
namespace utilities
{
    public static class Role_Restrictions
    {
        public static Primary_Weapon[] Sniper_Primary = { Primary_Weapon.HDR, Primary_Weapon.AX50, Primary_Weapon.AWP };
        public static Secondary_Weapon[] Sniper_Secondary = { Secondary_Weapon.Glock, Secondary_Weapon.FiveSeven, Secondary_Weapon.Renetti };
        public static Primary_Weapon[] Assault_Primary = { Primary_Weapon.AK47, Primary_Weapon.M4, Primary_Weapon.KILO };
        public static Secondary_Weapon[] Assault_Secondary = { Secondary_Weapon.Knife, Secondary_Weapon.Sticks, Secondary_Weapon.Hammer };
        public static bool Is_Valid_Primary(Role r, Primary_Weapon w)
        {
            if (r == Role.Assault) return Assault_Primary.Contains(w);
            if (r == Role.Sniper) return Sniper_Primary.Contains(w);
            return false;
        }
        public static bool Is_Valid_Secondary(Role r, Secondary_Weapon s)
        {
            if (r == Role.Assault) return Assault_Secondary.Contains(s);
            if (r == Role.Sniper) return Sniper_Secondary.Contains(s);
            return false;
        }
    }
}