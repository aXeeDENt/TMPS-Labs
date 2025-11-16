# LABORATORY WORK 1 - Creational Design Patterns
**Author:** Tatarintev Denis (FAF-232) 
**Topic:** Creational Design Patterns  

### 1. Objectives

1. Study and understand the main Creational Design Patterns.  
2. Choose a domain, define the core classes/entities, and determine the needed instantiation mechanisms.  
3. Implement at least 3 Creational Patterns inside a sample project.  
4. Create a clean modular codebase demonstrating these patterns in action.
### 2. Tasks

1. Choose an OO programming language (**C#**) and a simple structure (no frameworks).  
2. Select a domain - FPS-style game player loadouts.  
3. Define models such as `Player`, `Role`, `Weapon`, `Gadget`.  
4. Implement at least **3 Creational Design Patterns**:  
   - **Builder Pattern**  
   - **Prototype Pattern**  
   - **Object Pool Pattern**  
5. Organize the project into modules:  
   - **Client**  
   - **Domain**  
   - **Factory**  
   - **Modules**

### 3. Theoretical Background

In software engineering, **Creational Design Patterns** focus on how objects are created.  
Direct object creation (`new`) often leads to:  
- code duplication  
- tightly coupled modules  
- difficulty in extending or reusing logic  
- performance issues  

Creational patterns solve these problems by optimizing, abstracting or controlling object creation.

Examples of creational patterns include:  
- **Singleton**  
- **Builder**  
- **Prototype**  
- **Object Pooling**  
- **Factory Method**  
- **Abstract Factory**

### 4. Theory of Implemented Patterns

#### 4.1 Builder Pattern

**Purpose:** Separates the *construction* of a complex object from its *representation*.  
Allows creating different variations of an object step-by-step.

**Why used here:**  
A `Player` has:  
- role  
- primary weapon  
- secondary weapon  
- gadget  

Different character types (Sniper, Assault) require different configurations.  
Builder makes this clean and maintainable.

#### 4.2 Prototype Pattern

**Purpose:** Creates new objects by cloning an existing one.  
Useful when creating objects through constructors is expensive or when identical objects are needed.

**Why used here:**  
Players with identical configurations should be duplicated easily without rebuilding from scratch.

### 4.3 Object Pool Pattern

**Purpose:** Reuses objects instead of creating/destroying them repeatedly.  
Improves performance and memory usage.

**Why used here:**  
Players can be “returned” to a pool and reused just like pooled game objects in real game engines.

### 5. Main Idea of the Code

The project simulates an FPS-style game where a `Player` has:  
- a **role** (Assault / Sniper)  
- a **primary weapon**  
- a **secondary weapon**  
- a **gadget**

The program demonstrates how to build, clone, and reuse `Player` objects using three creational patterns:

- Builder creates configured loadouts  
- Prototype duplicates a configured player  
- Object Pool reuses player objects efficiently  

The `Program.cs` shows all three patterns in action.

### 6. Pattern Integrations (With Code Snippets)

#### 6.1 Builder Pattern Integration

**Description:**  
The Director orchestrates the building steps, while SniperBuilder and AssaultBuilder define specific configurations.

```csharp
Director director = new Director();
SniperBuilder sniperBuilder = new SniperBuilder();
director.SetBuilder(sniperBuilder);
sniperBuilder.CreateNewPlayer();
Player builtSniper = director.BuildPlayer();
public override void SetRole() => player.role = Role.Sniper;
public override void SetPrimaryWeapon() =>
    player.primary_weapon = RoleRestrictions.SniperPrimary[rnd.Next(...)];
builder.SetRole();
builder.SetPrimaryWeapon();
builder.SetSecondaryWeapon();
builder.SetGadget();
return builder.GetPlayer();
```

#### 6.2 Prototype Pattern Integration

**Description:**
Player can produce a clone of itself using `Prototype()`.

```cs
Player sniper = new Player(Role.Sniper, Primary_Weapon.HDR, Secondary_Weapon.FiveSeven, Gadget.Medkit);
Player sniper_Clone = sniper.Prototype();

public Player Prototype()
{
    return new Player(this.role, this.primary_weapon, this.secondary_weapon, this.gadget);
}

sniper.Show();
sniper_Clone.Show();
```
#### 6.3 Object Pool Pattern Integration

**Description:**
`Player_Pool` stores and reuses `Player` objects by resetting them and returning them to the pool.
```cs
Player_Pool pool = new Player_Pool();
pool.Release(builtSniper);
pool.Release(builtAssault);

Player p1 = pool.Get();
Player p2 = pool.Get();

if (pool.Count > 0)
{
    Player p = pool[0];
    pool.RemoveAt(0);
    return p;
}
return CreateDefaultPlayer();

pool.Add(player);
```
### 7. Conclusion

This project demonstrates three core **Creational Design Patterns** Builder, Prototype, and Object Pool within a practical domain of game character loadouts.

- **Builder** provides a clear, modular way to construct complex Player objects using predefined roles.

- **Prototype** enables fast duplication of configured players without reconstructing them.

- **Object Pool** optimizes performance by reusing player objects instead of creating new ones each time.

The result is a clean, extendable, and efficient architecture that illustrates how different creational patterns solve different creation-related problems.
The project meets all lab requirements, uses clearly separated modules, and provides a functional demonstration of creational design principles in practice.
