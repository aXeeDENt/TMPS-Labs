using System;
using models; 

namespace patterns.ChainOfResponsibility
{
    public interface IArmorHandler
    {
        IArmorHandler SetNext(IArmorHandler handler);
        bool HandleDamage(Player player, int damage);
    }

    public abstract class ArmorHandler : IArmorHandler
    {
        private IArmorHandler _nextHandler;

        public IArmorHandler SetNext(IArmorHandler handler)
        {
            _nextHandler = handler;
            return handler;
        }

        public virtual bool HandleDamage(Player player, int damage)
        {
            if (_nextHandler != null) { return _nextHandler.HandleDamage(player, damage); }
            return false; 
        }
    }

    public class ThirdPlateHandler : ArmorHandler
    {
        public override bool HandleDamage(Player player, int damage)
        {
            if (player.ArmorPlates >= 3)
            {
                player.ArmorPlates = 2;
                Console.WriteLine($"  Third plate destroyed! Remaining plates: {player.ArmorPlates}");
                return true;
            }
            return base.HandleDamage(player, damage);
        }
    }

    public class SecondPlateHandler : ArmorHandler
    {
        public override bool HandleDamage(Player player, int damage)
        {
            if (player.ArmorPlates >= 2)
            {
                player.ArmorPlates = 1;
                Console.WriteLine($"  Second plate destroyed! Remaining plates: {player.ArmorPlates}");
                return true;
            }
            return base.HandleDamage(player, damage);
        }
    }

    public class FirstPlateHandler : ArmorHandler
    {
        public override bool HandleDamage(Player player, int damage)
        {
            if (player.ArmorPlates >= 1)
            {
                player.ArmorPlates = 0;
                Console.WriteLine($"  First plate destroyed! No armor left!");
                return true;
            }
            return base.HandleDamage(player, damage);
        }
    }

    public class PlayerKillHandler : ArmorHandler
    {
        public override bool HandleDamage(Player player, int damage)
        {
            if (player.ArmorPlates <= 0)
            {
                player.IsAlive = false;
                Console.WriteLine($"  Player has been killed! No armor remaining.");
                return true;
            }
            return base.HandleDamage(player, damage);
        }
    }

    public static class ArmorChainBuilder
    {
        public static IArmorHandler BuildChain()
        {
            var thirdPlate = new ThirdPlateHandler();
            var secondPlate = new SecondPlateHandler();
            var firstPlate = new FirstPlateHandler();
            var killHandler = new PlayerKillHandler();

            thirdPlate.SetNext(secondPlate).SetNext(firstPlate).SetNext(killHandler);
            return thirdPlate;
        }
    }
}