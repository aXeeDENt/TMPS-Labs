using System.Collections.Generic;
using models;
using patterns.Decorator;
using patterns.Facade;
using patterns.Proxy;
using utilities;
using factories;

namespace client
{
    class Program
    {
        static void Main(string[] args)
        {
            var p1 = Player_Factory.Create_Player(Role.Assault);
            var p2 = Player_Factory.Create_Player(Role.Sniper);
            var p3 = Player_Factory.Create_Player(Role.Assault);
            Console.WriteLine("After basic creation (no patterns applied)");
            p1.Show();
            p2.Show();
            p3.Show();
            Console.WriteLine();
            var gp1 = new Player_Group_Decorator(p1);
            var gp2 = new Player_Group_Decorator(p2);
            var gp3 = new Player_Group_Decorator(p3);
            Console.WriteLine("After applying Player_Group_Decorator (groups assigned)");
            gp1.Show();
            Console.WriteLine($"Group: {gp1.Group()}");
            gp2.Show();
            Console.WriteLine($"Group: {gp2.Group()}");
            gp3.Show();
            Console.WriteLine($"Group: {gp3.Group()}");
            Console.WriteLine();
            var assault_Loadout = new Loadout_Facade(Primary_Weapon.M4, Secondary_Weapon.Knife, Gadget.Grenade);
            var sniper_Loadout = new Loadout_Facade(Primary_Weapon.HDR, Secondary_Weapon.Glock, Gadget.Medkit);
            
            Console.WriteLine("Applying loadouts (Facade)");
            assault_Loadout.Apply(gp1); 
            sniper_Loadout.Apply(gp2);
            assault_Loadout.Apply(gp3);
            gp1.Show(); gp2.Show(); gp3.Show();
            Console.WriteLine();

            Console.WriteLine("Trying invalid assignments via Player_Proxy (should be blocked)");
            var proxy1 = new Player_Proxy(gp1); 
            var proxy2 = new Player_Proxy(gp2); 
            proxy1.Set_Primary(Primary_Weapon.AK47);
            proxy1.Set_Primary(Primary_Weapon.HDR);
            proxy2.Set_Secondary(Secondary_Weapon.Knife);
            proxy2.Set_Primary(Primary_Weapon.AX50);
            Console.WriteLine();
            Console.WriteLine("Final state after proxy validations");
            gp1.Show();
            gp2.Show();
            gp3.Show();
        }
    }
}