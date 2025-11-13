namespace LAB1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Player sniper = new Player(Sniper, HDR, 5-7, Medkit);
            Player sniper1 = sniper.Prototype();

            Player assault = new Player(Assault, M4, Knife, Grenade);
            Player assault1 = assault.Prototype();
        }
    }
}