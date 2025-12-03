using System;
using models;
using utilities;
namespace factories
{
    public static class Player_Factory
    {
        public static Player Create_Player(Role r)
        {
            if (r == Role.Assault) return new Player(r, Role_Restrictions.Assault_Primary[0], Role_Restrictions.Assault_Secondary[0], Gadget.Grenade);
            else return new Player(r, Role_Restrictions.Sniper_Primary[0], Role_Restrictions.Sniper_Secondary[0], Gadget.Medkit);
        }
    }
}
