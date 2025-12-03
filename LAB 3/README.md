# **Laboratory Work: Behavioral Design Patterns**
**FAF-232** Tatarintev Denis
**TMPS - Laboratory Work #3**

### Introduction / Motivation
This laboratory work extends the Battlefield-style shooter system implemented in C# with three **Behavioral Design Patterns**.
While structural patterns focus on object composition, behavioral patterns concentrate on **communication and responsibility distribution between objects**.
The implemented patterns allow the system to:

- Handle armor damage sequentially through a chain of handlers

- Execute and undo player actions via keyboard commands

- Iterate through game items in a standardized way
These patterns make the system more interactive, flexible, and maintainable by defining clear communication protocols between objects.

### Theoretical Background
Behavioral design patterns define how objects interact and distribute responsibilities without tightly coupling them.

##### Patterns used in this work:
###### Chain of Responsibility
- Passes requests along a chain of handlers, where each handler decides whether to process the request or pass it to the next handler.

###### Command
- Encapsulates a request as an object, allowing parameterization, queuing, logging, and undoable operations.

###### Iterator
- Provides a way to access elements of a collection sequentially without exposing its underlying representation.

These patterns promote loose coupling, enhance flexibility, and make the system more responsive to user interactions.

### Implementation & Explanation
Below are the implemented behavioral patterns and their role in the system.


### **1. Chain of Responsibility Pattern - Armor Damage System**

##### **Main Idea**
When a Player takes damage, it should lose armor plates sequentially:

- 3 plates → hit 3rd plate → 2 plates remaining

- 2 plates → hit 2nd plate → 1 plate remaining

- 1 plate → hit 1st plate → 0 plates remaining

- 0 plates → player is killed
Instead of complex conditional logic in the Player class, damage is processed through a chain of specialized handlers.
##### **Why this pattern?**
- Decouples sender from receiver

- Allows dynamic chain configuration

- Each handler has a single responsibility

- Easy to add new damage types or effects
##### **Code**  
**Location:** `patterns/ChainOfResponsibility/Armor_Handler.cs`
```csharp
public interface IArmorHandler  
{  
    IArmorHandler SetNext(IArmorHandler handler);  
    bool HandleDamage(Player player, int damage);  
}  

public class ThirdPlateHandler : ArmorHandler  
{  
    public override bool HandleDamage(Player player, int damage)  
    {  
        if (player.ArmorPlates >= 3)  
        {  
            player.ArmorPlates = 2;  
            Console.WriteLine($"  Third plate destroyed! Remaining plates: {player.ArmorPlates}");  
            return true;  
        }  
        return base.HandleDamage(player, damage);  
    }  
}  
```
### 2. Command Pattern - Keyboard Action System
Player actions (changing weapons/gadgets) should be triggered by keyboard inputs with support for undo operations.
Instead of direct method calls, actions are encapsulated as command objects that can be executed, queued, and undone.

##### Why Command?
- Decouples invoker from receiver

- Supports undo/redo functionality

- Enables command queuing and logging

- Simplifies key binding configuration

##### Code
Location: `patterns/Command/Player_Command.cs` and `patterns/Command/Command_Invoker.cs`

```csharp
public interface ICommand  
{  
    void Execute();  
    void Undo();  
}  

public class ChoosePrimaryCommand : PlayerCommand  
{  
    public override void Execute()  
    {  
        _previousWeapon = _player.primary_weapon;  
        _player.Choose_Primary(_newWeapon);  
    }  

    public override void Undo()  
    {  
        Console.WriteLine($"Undoing: Changing primary back to {_previousWeapon}");  
        _player.primary_weapon = _previousWeapon;  
    }  
}  

public class CommandInvoker  
{  
    private Dictionary<char, ICommand> _keyBindings = new Dictionary<char, ICommand>();  

    public void BindKey(char key, ICommand command)  
    {  
        _keyBindings[key] = command;  
    }  

    public void PressKey(char key)  
    {  
        if (_keyBindings.TryGetValue(key, out ICommand command))  
        {  
            command.Execute();  
            _commandHistory.Push(command);  
        }  
    }  
}  
```
### 3. Iterator Pattern - Loadout Collection Navigation
##### Main Idea
The system should be able to iterate through all available weapons and gadgets without exposing the underlying collection structure.
Different iteration strategies can be applied to different collections (primary weapons, secondary weapons, gadgets).
##### Why this?
- Provides uniform access to different collections

- Hides collection implementation details

- Supports multiple simultaneous traversals

- Simplifies client code

##### Code
Location: `patterns/Iterator/Loadout_Iterator.cs` and `patterns/Iterator/Loadout_Aggregate.cs`

```csharp
public interface ILoadoutIterator  
{  
    bool HasNext();  
    string Next();  
    void Reset();  
}  

public class PrimaryWeaponIterator : ILoadoutIterator  
{  
    public bool HasNext()  
    {  
        return _position < _weapons.Length;  
    }  

    public string Next()  
    {  
        return $"Primary Weapon: {_weapons[_position++]}";  
    }  
}  

public class LoadoutCollectionViewer  
{  
    public static void ViewAllLoadouts()  
    {  
        var primaryIterator = primaryAggregate.CreateIterator();  
        while (primaryIterator.HasNext())  
        {  
            Console.WriteLine($"  - {primaryIterator.Next()}");  
        }  
    }  
}  
```

### Results
The final console output demonstrates:

- **Chain of Responsibility** processing armor damage through sequential plate destruction

- **Command** pattern executing and undoing player actions via keyboard inputs

- **Iterator** pattern navigating through all available game items and role-specific weapons
```powershell
DEMONSTRATION OF NEW BEHAVIORAL PATTERNS  

INITIAL PLAYER STATES:  
Role: Assault, Primary: AK47, Secondary: Knife, Gadget: Grenade, Armor: 3 plates, Alive: True  
Role: Sniper, Primary: HDR, Secondary: Glock, Gadget: Medkit, Armor: 3 plates, Alive: True  

1. CHAIN OF RESPONSIBILITY PATTERN  

Armor Damage System - Each hit destroys one armor plate:  

Player 1 (Assault) - Starting with 3 armor plates:  
Role: Assault, Primary: AK47, Secondary: Knife, Gadget: Grenade, Armor: 3 plates, Alive: True  

Taking damage sequence:  

Hit #1:  
  Third plate destroyed! Remaining plates: 2  
  Result: 2 plates remaining, Alive: True  

Hit #2:  
  Second plate destroyed! Remaining plates: 1  
  Result: 1 plates remaining, Alive: True  

Hit #3:  
  First plate destroyed! No armor left!  
  Result: 0 plates remaining, Alive: True  

Hit #4:  
  Player has been killed! No armor remaining.  
  Result: 0 plates remaining, Alive: False  

2. COMMAND PATTERN  

Key binding system for weapon/gadget selection:  
Initial player state:  
Role: Assault, Primary: AK47, Secondary: Knife, Gadget: Grenade, Armor: 3 plates, Alive: True  

Current Key Bindings  
Key '1': Change primary to M4  
Key '2': Change secondary to Sticks  
Key 'G': Change gadget to Armor  
 

Simulating keyboard input:  
Press: 1 (Change primary to M4)  
Changed primary weapon to: M4  
Press: 2 (Change secondary to Sticks)  
Changed secondary weapon to: Sticks  
Press: G (Change gadget to Armor)  
Changed gadget to: Armor  

Undoing last command (Armor -> back to Grenade):  
Undoing: Changing gadget back to Grenade  

3. ITERATOR PATTERN  

Iterating through all available game items:  

VIEWING ALL LOADOUT OPTIONS (Iterator Pattern)
Primary Weapons (6 total):  
  - Primary Weapon: AK47  
  - Primary Weapon: M4  
  - Primary Weapon: KILO  
  - Primary Weapon: AX50  
  - Primary Weapon: AWP  
  - Primary Weapon: HDR  

Secondary Weapons (6 total):  
  - Secondary Weapon: Knife  
  - Secondary Weapon: Sticks  
  - Secondary Weapon: Hammer  
  - Secondary Weapon: FiveSeven  
  - Secondary Weapon: Glock  
  - Secondary Weapon: Renetti  

Gadgets (3 total):  
  - Gadget: Grenade  
  - Gadget: Medkit  
  - Gadget: Armor  

ASSAULT ROLE WEAPONS 
Primary weapons available for Assault:  
  1. Primary Weapon: AK47  
  2. Primary Weapon: M4  
  3. Primary Weapon: KILO  

Secondary weapons available for Assault:  
  1. Secondary Weapon: Knife  
  2. Secondary Weapon: Sticks  
  3. Secondary Weapon: Hammer  

DEMONSTRATION COMPLETE 
```
### Conclusions
The implementation of behavioral design patterns significantly enhances the interactivity and flexibility of the shooter system:

- **Chain of Responsibility** creates a modular damage system where each armor plate is handled by a dedicated component

- **Command** enables flexible input handling with undo capabilities, making the system more responsive to player actions

- **Iterator** provides a standardized way to navigate game collections, simplifying future additions of new item types

These patterns demonstrate how to manage object interactions in a decoupled, maintainable way, preparing the system for more complex gameplay mechanics in future developments.