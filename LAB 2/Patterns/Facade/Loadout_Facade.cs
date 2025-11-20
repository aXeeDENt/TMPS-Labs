using System;
using models;

namespace patterns.Facade
{
    // Facade to set all three loadout items at once
    public class LoadoutFacade
    {
        public Primary_Weapon Primary;
        public Secondary_Weapon Secondary;
        public Gadget Gadget;

        public LoadoutFacade(Primary_Weapon p, Secondary_Weapon s, Gadget g)
        {
            Primary = p;
            Secondary = s;
            Gadget = g;
        }

        // Apply loadout directly to an IPlayer (Facade hides details)
        public void Apply(models.IPlayer player)
        {
            player.primary_weapon = Primary;
            player.secondary_weapon = Secondary;
            player.gadget = Gadget;
        }
    }
}
