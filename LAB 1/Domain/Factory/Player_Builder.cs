namespace LAB1
{
    public abstract class PlayerBuilder
    {
        protected Player player;
        public void CreateNewPlayer()
        { player = new Player(Role.Assault, Primary_Weapon.AK47, Secondary_Weapon.Knife, Gadget.Grenade); }
        public Player GetPlayer() => player;
        public abstract void SetRole();
        public abstract void SetPrimaryWeapon();
        public abstract void SetSecondaryWeapon();
        public abstract void SetGadget();
    }
}