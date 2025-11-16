namespace LAB1
{
    public class Director
    {
        private PlayerBuilder builder;

        public void SetBuilder(PlayerBuilder builder)
        { this.builder = builder; }

        public Player BuildPlayer()
        {
            builder.CreateNewPlayer();
            builder.SetRole();
            builder.SetPrimaryWeapon();
            builder.SetSecondaryWeapon();
            builder.SetGadget();
            return builder.GetPlayer();
        }
    }
}