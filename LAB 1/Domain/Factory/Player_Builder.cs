namespace LAB1
{
    public abstract class PlayerBuilder
    {
        protected Player player;

        public Player GetPlayer() => player;

        public abstract void SetRole();
        public abstract void SetPrimaryWeapon();
        public abstract void SetSecondaryWeapon();
        public abstract void SetGadget();
    }
}