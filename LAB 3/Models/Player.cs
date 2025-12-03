using System;

namespace models
{
    public class Player : IPlayer
    {
        public Role role { get; set; }
        public Primary_Weapon primary_weapon { get; set; }
        public Secondary_Weapon secondary_weapon { get; set; }
        public Gadget gadget { get; set; }
        public int ArmorPlates { get; set; } = 3; 
        public bool IsAlive { get; set; } = true;

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

        public Player Clone() { return new Player(this.role, this.primary_weapon, this.secondary_weapon, this.gadget); }

        public virtual void Show() { Console.WriteLine($"Role: {role}, Primary: {primary_weapon}, Secondary: {secondary_weapon}, Gadget: {gadget}, Armor: {ArmorPlates} plates, Alive: {IsAlive}");}

        public void Choose_Primary(Primary_Weapon weapon)
        {
            if (utilities.Role_Restrictions.Is_Valid_Primary(this.role, weapon))
            {
                this.primary_weapon = weapon;
                Console.WriteLine($"Changed primary weapon to: {weapon}");
            }
            else { Console.WriteLine($"Invalid primary weapon {weapon} for role {role}"); }
        }

        public void Choose_Secondary(Secondary_Weapon weapon)
        {
            if (utilities.Role_Restrictions.Is_Valid_Secondary(this.role, weapon))
            {
                this.secondary_weapon = weapon;
                Console.WriteLine($"Changed secondary weapon to: {weapon}");
            }
            else { Console.WriteLine($"Invalid secondary weapon {weapon} for role {role}"); }
        }

        public void Choose_Gadget(Gadget gadget)
        {
            this.gadget = gadget;
            Console.WriteLine($"Changed gadget to: {gadget}");
        }

        public void TakeDamage(int damage) { Console.WriteLine($"Player takes {damage} damage. Current armor plates: {ArmorPlates}"); }
    }
}