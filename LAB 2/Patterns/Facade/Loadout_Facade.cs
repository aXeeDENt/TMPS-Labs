using System;
using models;
namespace patterns.Facade
{
    public class Loadout_Facade
    {
        public Primary_Weapon Primary;
        public Secondary_Weapon Secondary;
        public Gadget Gadget;
        public Loadout_Facade(Primary_Weapon p, Secondary_Weapon s, Gadget g)
        {
            Primary = p;
            Secondary = s;
            Gadget = g;
        }
        public void Apply(models.IPlayer player)
        {
            player.primary_weapon = Primary;
            player.secondary_weapon = Secondary;
            player.gadget = Gadget;
        }
    }
}