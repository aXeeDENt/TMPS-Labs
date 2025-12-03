using System;
using models; 

namespace patterns.Command
{
    public interface ICommand
    {
        void Execute();
        void Undo();
    }

    public abstract class PlayerCommand : ICommand
    {
        protected Player _player;
        protected string _description;

        protected PlayerCommand(Player player, string description)
        {
            _player = player;
            _description = description;
        }

        public abstract void Execute();
        public abstract void Undo();
    }

    public class ChoosePrimaryCommand : PlayerCommand
    {
        private Primary_Weapon _newWeapon;
        private Primary_Weapon _previousWeapon;

        public ChoosePrimaryCommand(Player player, Primary_Weapon weapon) 
            : base(player, $"Change primary to {weapon}") { _newWeapon = weapon; }

        public override void Execute()
        {
            _previousWeapon = _player.primary_weapon;
            _player.Choose_Primary(_newWeapon);
        }

        public override void Undo()
        {
            Console.WriteLine($"Undoing: Changing primary back to {_previousWeapon}");
            _player.primary_weapon = _previousWeapon;
        }
    }

    public class ChooseSecondaryCommand : PlayerCommand
    {
        private Secondary_Weapon _newWeapon;
        private Secondary_Weapon _previousWeapon;

        public ChooseSecondaryCommand(Player player, Secondary_Weapon weapon) 
            : base(player, $"Change secondary to {weapon}") { _newWeapon = weapon; }

        public override void Execute()
        {
            _previousWeapon = _player.secondary_weapon;
            _player.Choose_Secondary(_newWeapon);
        }

        public override void Undo()
        {
            Console.WriteLine($"Undoing: Changing secondary back to {_previousWeapon}");
            _player.secondary_weapon = _previousWeapon;
        }
    }

    public class ChooseGadgetCommand : PlayerCommand
    {
        private Gadget _newGadget;
        private Gadget _previousGadget;

        public ChooseGadgetCommand(Player player, Gadget gadget) 
            : base(player, $"Change gadget to {gadget}") { _newGadget = gadget; }

        public override void Execute()
        {
            _previousGadget = _player.gadget;
            _player.Choose_Gadget(_newGadget);
        }

        public override void Undo()
        {
            Console.WriteLine($"Undoing: Changing gadget back to {_previousGadget}");
            _player.gadget = _previousGadget;
        }
    }
}