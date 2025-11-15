using LAB1;

namespace LAB1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Prototype Pattern
            Player sniper = new Player(Role.Sniper, Primary_Weapon.HDR, Secondary_Weapon.FiveSeven, Gadget.Medkit);
            Player sniperClone = sniper.Prototype();

            Player assault = new Player(Role.Assault, Primary_Weapon.M4, Secondary_Weapon.Knife, Gadget.Grenade);
            Player assaultClone = assault.Prototype();

            sniper.Show();
            sniperClone.Show();
            assault.Show();
            assaultClone.Show();

            // Builder Pattern
            SniperBuilder sniperBuilder = new SniperBuilder();
            sniperBuilder.SetPrimaryWeapon();
            sniperBuilder.SetSecondaryWeapon();
            Player builtSniper = sniperBuilder.GetPlayer();
            builtSniper.Show();

            AssaultBuilder assaultBuilder = new AssaultBuilder();
            assaultBuilder.SetPrimaryWeapon();
            assaultBuilder.SetSecondaryWeapon();
            Player builtAssault = assaultBuilder.GetPlayer();
            builtAssault.Show();

            // Object Pool Pattern
            Player_Pool pool = new Player_Pool();
            pool.Release(builtSniper);
            pool.Release(builtAssault);

            Player p1 = pool.Get();
            Player p2 = pool.Get();

            p1.Show();
            p2.Show();
        }
    }
}