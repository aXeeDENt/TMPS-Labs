namespace LAB1
{
    public class Player
    {
        public Role role { get; set; }
        public Primary_Weapon primary_weapon { get; set; }
        public Secondary_Weapon secondary_weapon { get; set; }
        public Gadget gadget { get; set; }

        public Player(Role role, Primary_Weapon primary_weapon, Secondary_Weapon secondary_weapon, Gadget gadget)
        {
            this.role = role;
            this.primary_weapon = primary_weapon;
            this.secondary_weapon = secondary_weapon;
            this.gadget = gadget;
        }

        public Player Prototype()
        {
            return new Player(this.role, this.primary_weapon, this.secondary_weapon, this.gadget);
        }
    }
}