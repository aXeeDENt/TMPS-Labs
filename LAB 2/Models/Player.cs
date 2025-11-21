using System;

namespace models
{
    public class Player : IPlayer
    {
        public Role role { get; set; }
        public Primary_Weapon primary_weapon { get; set; }
        public Secondary_Weapon secondary_weapon { get; set; }
        public Gadget gadget { get; set; }

        public Player(Role role,
                      Primary_Weapon primary_weapon = Primary_Weapon.AK47,
                      Secondary_Weapon secondary_weapon = Secondary_Weapon.Knife,
                      Gadget gadget = Gadget.Grenade)
        {
            this.role = role;
            this.primary_weapon = primary_weapon;
            this.secondary_weapon = secondary_weapon;
            this.gadget = gadget;
        }

        public Player Clone()
        {
            return new Player(this.role, this.primary_weapon, this.secondary_weapon, this.gadget);
        }

        public virtual void Show()
        {
            Console.WriteLine($"Role: {role}, Primary: {primary_weapon}, Secondary: {secondary_weapon}, Gadget: {gadget}");
        }
    }
}