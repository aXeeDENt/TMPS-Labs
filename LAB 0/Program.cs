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
    private string? dialogue;
    public void Initialize() => dialogue = "Hi, friend";
    public void Spawn() { Console.WriteLine($"{Name} has spawned saying '{dialogue}'"); }
}

public static class CharacterFactory
{
    private static readonly Dictionary<string, Func<ICharacter>> Character_Creators = new()
    {
        ["player"] = () => new Player(),
        ["enemy"] = () => new Enemy(),
        ["friend"] = () => new Friend()
    };

    public static ICharacter Create(string Character_Type)
    {
        if (Character_Creators.TryGetValue(Character_Type.ToLower(), out var creator)) { return creator(); }
        throw new ArgumentException($"Unknown character type '{Character_Type}'");
    }

    public static void RegisterCharacter(string type, Func<ICharacter> creator) { Character_Creators[type.ToLower()] = creator; }
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
        Console.WriteLine("Generating Player, Enemy, Friend and checking unknown type\n");
        
        CharacterSpawner.GenerateAndSpawn("Player");
        CharacterSpawner.GenerateAndSpawn("Enemy");
        CharacterSpawner.GenerateAndSpawn("Friend");
        CharacterFactory.RegisterCharacter("Boss", () => new Boss());
        CharacterSpawner.GenerateAndSpawn("Boss");
        CharacterSpawner.GenerateAndSpawn("Monster"); 
    }
}
public class Boss : ICharacter
{
    public string Name { get; } = "Boss";
    private int health;
    private int attack;
    public void Initialize() 
    { 
        health = 200; 
        attack = 30;
    }
    public void Spawn() { Console.WriteLine($"{Name} has spawned with {health} HP and {attack} attack!"); }
}