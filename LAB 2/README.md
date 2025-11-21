# **Laboratory Work: Structural Design Patterns**
**FAF-232** Tatarintev Denis
**TMPS - Laboratory Work #2**

### Introduction / Motivation
This laboratory work continues the development of a Battlefield-style shooter system implemented in C#.  
Players can be **Assault** or **Sniper**, each having a primary weapon, secondary weapon, and gadget.
To improve the structure, readability, and scalability of the system, three **Structural Design Patterns** were implemented:
- **Decorator Pattern** - adds grouping behavior to Player objects  
- **Facade Pattern** - simplifies weapon loadout configuration  
- **Proxy Pattern** - ensures weapons assigned to Player match their role  
These patterns demonstrate how to create flexible object structures without rewriting code or breaking existing classes.

### Theoretical Background
Structural design patterns determine **how objects and classes can be combined** to create larger, flexible systems.

##### Patterns used in this work:
- **Decorator**
Allows attaching new responsibilities to an object dynamically, without modifying its original class.
- **Facade**
Provides a simplified interface to a complex subsystem by grouping multiple operations into a single method.
- **Proxy**
Controls access to another object - validates, restricts, or monitors operations before they reach the real object.

These patterns reduce coupling, improve extension possibilities, and follow the principle of *composition over inheritance*.


### Implementation & Explanation
Below are the implemented structural patterns and their role in the system.

### **1. Decorator Pattern - Player Group Assignment**

##### **Main Idea**
Each Player should automatically receive a unique **Group number** based on creation order:
- First → Group 1  
- Second → Group 2  
- Third → Group 3  
Instead of modifying `Player.cs`, the new behavior is added using a **Decorator**.
##### **Why Decorator?**
- Adds functionality dynamically  
- Player class stays unchanged  
- Can combine multiple decorators in the future  
##### **Code**  
**Location:** `patterns/Decorator/PlayerGroupDecorator.cs`
```csharp
public class PlayerGroupDecorator : IPlayer
{
    private static int counter = 0;
    private readonly IPlayer _inner;
    private readonly int _group;

    public PlayerGroupDecorator(IPlayer inner)
    {
        _inner = inner;
        counter++;
        _group = counter;
    }

    public int Group() => _group;

    public void Show()
    {
        Console.Write($"[Group {_group}] ");
        _inner.Show();
    }
}
```
### 2. Facade Pattern - Loadout Configuration
Instead of assigning:
`player.primary_weapon = ...`
`player.secondary_weapon = ...`
`player.gadget = ...`
We use a single class to apply a full loadout at once.

##### Why Facade?
- Simplifies object configuration
- Hides complex initialization
- Reduces repeated code

##### Code
Location: `patterns/Facade/LoadoutFacade.cs`

```csharp
public class LoadoutFacade
{
    public Primary_Weapon Primary;
    public Secondary_Weapon Secondary;
    public Gadget Gadget;

    public LoadoutFacade(Primary_Weapon p, Secondary_Weapon s, Gadget g)
    {
        Primary = p;
        Secondary = s;
        Gadget = g;
    }

    public void Apply(IPlayer player)
    {
        player.primary_weapon = Primary;
        player.secondary_weapon = Secondary;
        player.gadget = Gadget;
    }
}
```
### 3. Proxy Pattern - Role-Based Weapon Validation
##### Main Idea
When assigning weapons, Snipers and Assault players must only receive weapons matching their role.
Instead of placing validation logic inside Player.cs, a Proxy validates requests before they reach the real Player.
##### Why Proxy?
- Protects system from invalid states
- Separates validation logic
- Keeps Player class simple

##### Code
Location: `patterns/Proxy/PlayerProxy.cs`

```csharp
public class PlayerProxy
{
    private readonly IPlayer _player;

    public PlayerProxy(IPlayer player)
    {
        _player = player;
    }

    public void SetPrimary(Primary_Weapon w)
    {
        if (RoleRestrictions.IsValidPrimary(_player.role, w))
        {
            _player.primary_weapon = w;
            Console.WriteLine($"Primary set to {w}");
        }
        else
        {
            Console.WriteLine($"Invalid primary {w} for role {_player.role}");
        }
    }
}
```

### Results
The final console output demonstrates:
- Players created without patterns
- Players wrapped in a Decorator receiving correct group numbers
- Facade applying full loadouts cleanly
- Proxy blocking invalid weapon assignments
Each step is visible in the console, proving that all three patterns function correctly.
```powershell
After basic creation (no patterns applied)
Role: Assault, Primary: AK47, Secondary: Knife, Gadget: Grenade
Role: Sniper, Primary: HDR, Secondary: Glock, Gadget: Medkit
Role: Assault, Primary: AK47, Secondary: Knife, Gadget: Grenade

After applying Player_Group_Decorator (groups assigned)
[Group 1] Role: Assault, Primary: AK47, Secondary: Knife, Gadget: Grenade
Group: 1
[Group 2] Role: Sniper, Primary: HDR, Secondary: Glock, Gadget: Medkit
Group: 2
[Group 3] Role: Assault, Primary: AK47, Secondary: Knife, Gadget: Grenade
Group: 3

Applying loadouts (Facade)
[Group 1] Role: Assault, Primary: M4, Secondary: Knife, Gadget: Grenade
[Group 2] Role: Sniper, Primary: HDR, Secondary: Glock, Gadget: Medkit
[Group 3] Role: Assault, Primary: M4, Secondary: Knife, Gadget: Grenade

Trying invalid assignments via Player_Proxy (should be blocked)
[Proxy] Primary set to AK47 for role Assault
[Proxy] INVALID primary HDR for role Assault — assignment blocked.
[Proxy] INVALID secondary Knife for role Sniper — assignment blocked.
[Proxy] Primary set to AX50 for role Sniper

Final state after proxy validations
[Group 1] Role: Assault, Primary: AK47, Secondary: Knife, Gadget: Grenade
[Group 2] Role: Sniper, Primary: AX50, Secondary: Glock, Gadget: Medkit
[Group 3] Role: Assault, Primary: M4, Secondary: Knife, Gadget: Grenade
```
### Conclusions
The use of structural design patterns greatly improves the architecture of the shooter system:
- Decorator adds optional behavior without modifying core classes
- Facade simplifies complex configuration and centralizes loadout logic
- Proxy enforces rules and protects system integrity
The project is now more flexible, maintainable, and scalable for future labs.