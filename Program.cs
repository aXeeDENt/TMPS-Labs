using System;
using System.Collections.Generic;

// ---------------------------------------------------------------------
// 1. Liskov Substitution Principle (LSP) and Open/Closed Principle (OCP)
// ---------------------------------------------------------------------

/// <summary>
/// ICharacter serves as the common base type.
/// LSP: Any concrete class implementing ICharacter is substitutable for it.
/// OCP: New character types can be added without modifying code that relies only on ICharacter (like CharacterSpawner).
/// </summary>
public interface ICharacter
{
    string Name { get; }
    void Initialize();
    void Spawn();
}

// ---------------------------------------------------------------------
// 2. Concrete Implementations (LSP in action)
// ---------------------------------------------------------------------

/// <summary>
/// Represents a Player Character.
/// </summary>
public class Player : ICharacter
{
    public string Name { get; } = "Hero";
    private int _health;

    public void Initialize() => _health = 100;
    
    // SRP: This method's sole concern is the Player's spawning setup.
    public void Spawn()
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"[PLAYER] {Name} has spawned with {_health} HP.");
        Console.ResetColor();
    }
}

/// <summary>
/// Represents an Enemy Character.
/// </summary>
public class Enemy : ICharacter
{
    public string Name { get; } = "Goblin";
    private int _attack;

    public void Initialize() => _attack = 15;
    
    // SRP: This method's sole concern is the Enemy's spawning setup.
    public void Spawn()
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"[ENEMY] {Name} has spawned, wielding {_attack} attack damage.");
        Console.ResetColor();
    }
}

/// <summary>
/// Represents a Friendly Non-Player Character (NPC).
/// </summary>
public class Friend : ICharacter
{
    public string Name { get; } = "Shopkeeper";
    private string _dialogue;

    public void Initialize() => _dialogue = "Welcome, adventurer!";
    
    // SRP: This method's sole concern is the Friend's spawning setup.
    public void Spawn()
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine($"[FRIEND] {Name} has spawned. Initial dialogue: '{_dialogue}'");
        Console.ResetColor();
    }
}

// ---------------------------------------------------------------------
// 3. Single Responsibility Principle (SRP)
// ---------------------------------------------------------------------

/// <summary>
/// SRP: This class is solely responsible for creating ICharacter objects (Instantiation logic).
/// </summary>
public static class CharacterFactory
{
    public static ICharacter Create(string characterType)
    {
        switch (characterType.ToLower())
        {
            case "player":
                return new Player();
            case "enemy":
                return new Enemy();
            case "friend":
                return new Friend();
            default:
                throw new ArgumentException($"Unknown character type: {characterType}");
        }
    }
}

/// <summary>
/// SRP: This class is solely responsible for orchestrating the generation and execution (Runtime logic).
/// </summary>
public class CharacterSpawner
{
    // OCP: This method is closed for modification but open for extension.
    // It works with ANY ICharacter implementation without needing to change.
    public static void GenerateAndSpawn(string type)
    {
        try
        {
            // 1. Creation (handled by CharacterFactory - SRP)
            ICharacter character = CharacterFactory.Create(type);
            
            // 2. Initialization (LSP: we can call Initialize on any ICharacter)
            character.Initialize();

            // 3. Execution (LSP: we can call Spawn on any ICharacter)
            character.Spawn();
        }
        catch (ArgumentException ex)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"Error: {ex.Message}");
            Console.ResetColor();
        }
    }
}


// ---------------------------------------------------------------------
// 4. Main Program
// ---------------------------------------------------------------------

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine("--- Game Character Generation Demo (3x SOLID Principles) ---\n");
        
        // Use the spawner to generate objects by type string
        CharacterSpawner.GenerateAndSpawn("Player");
        CharacterSpawner.GenerateAndSpawn("Enemy");
        CharacterSpawner.GenerateAndSpawn("Friend");
        
        Console.WriteLine("\n--- Adding a new character type (e.g., 'Boss') would only require:");
        Console.WriteLine("1. Creating a new Boss class (OCP/LSP)");
        Console.WriteLine("2. Modifying CharacterFactory (SRP change)");
        Console.WriteLine("3. CharacterSpawner remains untouched (OCP).");
        
        CharacterSpawner.GenerateAndSpawn("UnknownType");
    }
}
