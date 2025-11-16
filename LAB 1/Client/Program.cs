namespace LAB1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Prototype Pattern:");
            Player sniper = new Player(Role.Sniper, Primary_Weapon.HDR, Secondary_Weapon.FiveSeven, Gadget.Medkit);
            Player sniper_Clone = sniper.Prototype();
            Player assault = new Player(Role.Assault, Primary_Weapon.M4, Secondary_Weapon.Knife, Gadget.Grenade);
            Player assault_Clone = assault.Prototype();
            sniper.Show();
            sniper_Clone.Show();
            assault.Show();
            assault_Clone.Show();
            Console.WriteLine(" ");

            Console.WriteLine("Builder Pattern:");
            Director director = new Director();
            SniperBuilder sniperBuilder = new SniperBuilder();
            director.SetBuilder(sniperBuilder);
            //sniperBuilder.CreateNewPlayer();   
            Player builtSniper = director.BuildPlayer();
            builtSniper.Show();
            AssaultBuilder assaultBuilder = new AssaultBuilder();
            director.SetBuilder(assaultBuilder);
            assaultBuilder.CreateNewPlayer(); 
            Player builtAssault = director.BuildPlayer();
            builtAssault.Show();
            Console.WriteLine(" ");

            Console.WriteLine("Object Pool Pattern:");
            Player_Pool pool = new Player_Pool();
            pool.Release(builtSniper);
            pool.Release(builtAssault);
            Player p1 = pool.Get();
            Player p2 = pool.Get();
            p1.Show();
            p2.Show();
        }
    }
}