using System;
using models;
using patterns.ChainOfResponsibility;
using patterns.Command;
using patterns.Iterator;

namespace client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("DEMONSTRATION OF NEW BEHAVIORAL PATTERNS\n");

            var player1 = new Player(Role.Assault, Primary_Weapon.AK47, Secondary_Weapon.Knife, Gadget.Grenade);
            var player2 = new Player(Role.Sniper, Primary_Weapon.HDR, Secondary_Weapon.Glock, Gadget.Medkit);
            
            Console.WriteLine("INITIAL PLAYER STATES:");
            player1.Show();
            player2.Show();
            Console.WriteLine();

            Console.WriteLine("1. CHAIN OF RESPONSIBILITY PATTERN\n");
            Console.WriteLine("Armor Damage System - Each hit destroys one armor plate:");
            
            var armorChain = ArmorChainBuilder.BuildChain();
            
            Console.WriteLine("\nPlayer 1 (Assault) - Starting with 3 armor plates:");
            player1.Show();
            
            Console.WriteLine("\nTaking damage sequence:");
            for (int i = 1; i <= 5; i++)
            {
                Console.WriteLine($"\nHit #{i}:");
                if (!player1.IsAlive)
                {
                    Console.WriteLine("  Player is already dead!");
                    break;
                }
                armorChain.HandleDamage(player1, 1);
                Console.WriteLine($"  Result: {player1.ArmorPlates} plates remaining, Alive: {player1.IsAlive}");
            }
            
            Console.WriteLine("\n\nPlayer 2 (Sniper) - Starting with 1 armor plate:");
            player2.ArmorPlates = 1;
            player2.IsAlive = true;
            player2.Show();
            
            Console.WriteLine("\nTaking damage sequence:");
            for (int i = 1; i <= 3; i++)
            {
                Console.WriteLine($"\nHit #{i}:");
                if (!player2.IsAlive)
                {
                    Console.WriteLine("  Player is already dead!");
                    break;
                }
                armorChain.HandleDamage(player2, 1);
                Console.WriteLine($"  Result: {player1.ArmorPlates} plates remaining, Alive: {player2.IsAlive}");
            }
            Console.WriteLine();

            Console.WriteLine("2. COMMAND PATTERN\n");
            Console.WriteLine("Key binding system for weapon/gadget selection:");
            
            var commandInvoker = new CommandInvoker();
            
            var commandPlayer = new Player(Role.Assault);
            Console.WriteLine("Initial player state:");
            commandPlayer.Show();
            
            commandInvoker.BindKey('1', new ChoosePrimaryCommand(commandPlayer, Primary_Weapon.M4));
            commandInvoker.BindKey('2', new ChooseSecondaryCommand(commandPlayer, Secondary_Weapon.Sticks));
            commandInvoker.BindKey('G', new ChooseGadgetCommand(commandPlayer, Gadget.Armor));
            commandInvoker.BindKey('Q', new ChoosePrimaryCommand(commandPlayer, Primary_Weapon.KILO));
            commandInvoker.BindKey('W', new ChoosePrimaryCommand(commandPlayer, Primary_Weapon.HDR)); // Invalid for assault
            
            commandInvoker.ShowKeyBindings();
            
            Console.WriteLine("\nSimulating keyboard input:");
            Console.WriteLine("Press: 1 (Change primary to M4)");
            commandInvoker.PressKey('1');
            commandPlayer.Show();
            
            Console.WriteLine("\nPress: 2 (Change secondary to Sticks)");
            commandInvoker.PressKey('2');
            commandPlayer.Show();
            
            Console.WriteLine("\nPress: G (Change gadget to Armor)");
            commandInvoker.PressKey('G');
            commandPlayer.Show();
            
            Console.WriteLine("\nPress: W (Try to change to HDR - invalid for Assault)");
            commandInvoker.PressKey('W');
            commandPlayer.Show();
            
            Console.WriteLine("\nPress: Q (Change primary to KILO)");
            commandInvoker.PressKey('Q');
            commandPlayer.Show();
            
            Console.WriteLine("\nUNDO FUNCTIONALITY");
            Console.WriteLine("Undoing last command (Q -> back to M4):");
            commandInvoker.UndoLastCommand();
            commandPlayer.Show();
            
            Console.WriteLine("\nUndoing another command (Armor -> back to Grenade):");
            commandInvoker.UndoLastCommand();
            commandPlayer.Show();
            
            Console.WriteLine("\nTrying to undo with empty history:");
            commandInvoker.UndoLastCommand();
            commandInvoker.UndoLastCommand();
            commandInvoker.UndoLastCommand(); 
            Console.WriteLine();

            Console.WriteLine("3. ITERATOR PATTERN\n");
            Console.WriteLine("Iterating through all available game items:");
            LoadoutCollectionViewer.ViewAllLoadouts();
            Console.WriteLine("Iterating through role-specific weapons:");
            
            Console.WriteLine("\nASSAULT ROLE WEAPONS");
            Console.WriteLine("Primary weapons available for Assault:");
            var assaultPrimaryIterator = new PrimaryWeaponIterator(utilities.Role_Restrictions.Assault_Primary);
            int assaultCount = 1;
            while (assaultPrimaryIterator.HasNext()) { Console.WriteLine($"  {assaultCount++}. {assaultPrimaryIterator.Next()}"); }
            
            Console.WriteLine("\nSecondary weapons available for Assault:");
            var assaultSecondaryIterator = new SecondaryWeaponIterator(utilities.Role_Restrictions.Assault_Secondary);
            int assaultSecCount = 1;
            while (assaultSecondaryIterator.HasNext()) { Console.WriteLine($"  {assaultSecCount++}. {assaultSecondaryIterator.Next()}"); }
            
            Console.WriteLine("\n--- SNIPER ROLE WEAPONS ---");
            Console.WriteLine("Primary weapons available for Sniper:");
            var sniperPrimaryIterator = new PrimaryWeaponIterator(utilities.Role_Restrictions.Sniper_Primary);
            int sniperCount = 1;
            while (sniperPrimaryIterator.HasNext()) { Console.WriteLine($"  {sniperCount++}. {sniperPrimaryIterator.Next()}"); }
            
            Console.WriteLine("\nSecondary weapons available for Sniper:");
            var sniperSecondaryIterator = new SecondaryWeaponIterator(utilities.Role_Restrictions.Sniper_Secondary);
            int sniperSecCount = 1;
            while (sniperSecondaryIterator.HasNext()) { Console.WriteLine($"  {sniperSecCount++}. {sniperSecondaryIterator.Next()}"); }
            
            Console.WriteLine("\nUSING AGGREGATES");
            var gadgetAggregate = new GadgetAggregate();
            foreach (Gadget gadget in Enum.GetValues(typeof(Gadget)))
            { gadgetAggregate.AddItem(gadget); }
            
            Console.WriteLine($"Total gadgets in game: {gadgetAggregate.Count}");
            var gadgetIterator = gadgetAggregate.CreateIterator();
            Console.WriteLine("All gadgets:");
            while (gadgetIterator.HasNext())
            { Console.WriteLine($"  - {gadgetIterator.Next()}"); }
            
            Console.WriteLine("\nDEMONSTRATION COMPLETE");
        }
    }
}