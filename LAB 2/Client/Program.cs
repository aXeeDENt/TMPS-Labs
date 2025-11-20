using System;
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
            Console.WriteLine("=== Structural Patterns Lab (Battlefield-like) ===\n");

            // 1) Create players using factory (basic Player objects)
            var p1 = PlayerFactory.CreatePlayer(Role.Assault);
            var p2 = PlayerFactory.CreatePlayer(Role.Sniper);
            var p3 = PlayerFactory.CreatePlayer(Role.Assault);

            Console.WriteLine("-- After basic creation (no patterns applied) --");
            p1.Show();
            p2.Show();
            p3.Show();
            Console.WriteLine();

            // 2) Decorator: assign group IDs dynamically by wrapping
            var gp1 = new PlayerGroupDecorator(p1);
            var gp2 = new PlayerGroupDecorator(p2);
            var gp3 = new PlayerGroupDecorator(p3);

            Console.WriteLine("-- After applying PlayerGroupDecorator (groups assigned) --");
            gp1.Show();
            Console.WriteLine($"Group: {gp1.Group()}");
            gp2.Show();
            Console.WriteLine($"Group: {gp2.Group()}");
            gp3.Show();
            Console.WriteLine($"Group: {gp3.Group()}");
            Console.WriteLine();

            // 3) Facade: apply loadouts via a single object
            var assaultLoadout = new LoadoutFacade(Primary_Weapon.M4, Secondary_Weapon.Knife, Gadget.Grenade);
            var sniperLoadout = new LoadoutFacade(Primary_Weapon.HDR, Secondary_Weapon.Glock, Gadget.Medkit);

            Console.WriteLine("-- Applying loadouts (Facade) --");
            assaultLoadout.Apply(gp1); // gp1 wraps p1
            sniperLoadout.Apply(gp2);
            assaultLoadout.Apply(gp3);
            gp1.Show(); gp2.Show(); gp3.Show();
            Console.WriteLine();

            // 4) Proxy: validate / control weapon assignment
            Console.WriteLine("-- Trying invalid assignments via PlayerProxy (should be blocked) --");

            var proxy1 = new PlayerProxy(gp1); // gp1 is Assault wrapped -> proxy enforces role rules
            var proxy2 = new PlayerProxy(gp2); // gp2 is Sniper wrapped

            // Valid assignment (assault primary)
            proxy1.SetPrimary(Primary_Weapon.AK47);
            // Invalid assignment (assault trying to get HDR - sniper primary)
            proxy1.SetPrimary(Primary_Weapon.HDR);

            // Invalid secondary for sniper (sniper trying to get Knife)
            proxy2.SetSecondary(Secondary_Weapon.Knife);
            // Valid sniper primary
            proxy2.SetPrimary(Primary_Weapon.AX50);

            Console.WriteLine();
            Console.WriteLine("-- Final state after proxy validations --");
            gp1.Show();
            gp2.Show();
            gp3.Show();
            Console.WriteLine();

            // 5) Demonstrate PlayerPool (optional: reuse/reset)
            Console.WriteLine("-- Demonstrating PlayerPool reuse --");
            var pool = new PlayerPool();
            var reused = pool.Get();
            reused.Show();
            pool.Release(reused);
            Console.WriteLine("Released player back to pool (defaults reset).");
            var reused2 = pool.Get();
            reused2.Show();

            Console.WriteLine("\n=== End ===");
        }
    }
}
