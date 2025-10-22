public interface ICharacter
{
    string Name { get; }
    void Initialize();
    void Spawn();
}
public class Player : ICharacter
{
    public string Name { get; } = "Player";
    private int health;
    public void Initialize() => health = 100;
    public void Spawn() { Console.WriteLine($"{Name} has spawned with {health} HP."); }
}
public class Enemy : ICharacter
{
    public string Name { get; } = "Enemy";
    private int attack;
    public void Initialize() => attack = 15;
    public void Spawn() { Console.WriteLine($"{Name} has spawned, with {attack} attack damage."); }
}
public class Friend : ICharacter
{
    public string Name { get; } = "Friend";
    private string ?dialogue;
    public void Initialize() => dialogue = "Hi, friend";
    public void Spawn() { Console.WriteLine($"{Name} has spawned saying '{dialogue}'"); }
}
public static class CharacterFactory
{
    public static ICharacter Create(string Character_Type)
    {
        switch (Character_Type.ToLower())
        {
            case "player": return new Player();
            case "enemy": return new Enemy();
            case "friend": return new Friend();
            default: throw new ArgumentException($"Unknown character type '{Character_Type}'");
        }
    }
}
public class CharacterSpawner
{
    public static void GenerateAndSpawn(string type)
    {
        try
        {
            ICharacter character = CharacterFactory.Create(type);
            character.Initialize();
            character.Spawn();
        }
        catch (ArgumentException ex) { Console.WriteLine($"Error: {ex.Message}"); }
    }
}
public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Generating Player, Enemy, Friend and checkin unknown type\n");
        CharacterSpawner.GenerateAndSpawn("Player");
        CharacterSpawner.GenerateAndSpawn("Enemy");
        CharacterSpawner.GenerateAndSpawn("Friend");
        CharacterSpawner.GenerateAndSpawn("Boss");
    }
}
