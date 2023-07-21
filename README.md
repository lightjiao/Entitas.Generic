## Waht is this?
Inspired by [Entitas.Generic](https://github.com/yosadchyi/Entitas.Generic), and I make API more simple. Work perfectly with Native Entitas VisualDebug

> If you use Unity 2022.2 or more, Entitas VisualDebug may have BUG, see this: [sschmid/Entitas#1067 (comment)](https://github.com/sschmid/Entitas/issues/1067#issuecomment-1623734894)

## How to install?
Download and put this project in your unity Asset directory

## Use sample
- Install [Entitas](https://github.com/sschmid/Entitas) before this.

- Declare context interface in your code

  ```csharp
  public class Game : Attribute, IScope
  {
  }
  ```

- Declare static context class in your code for convenience use

  ```csharp
  public static class GameCtx
  {
      public static ScopedCtx<Game> Inst => ScopedCtx<Game>.Inst;
  }
  ```

- Declare Component

  ```csharp
  [Game]
  public class Player : IComponent 
  {
      public int Id;
      public string Name;
  }
  ```

- Declare System like the Native Entitas.

- Init Entitas

  ```csharp
  Contexts.Inst.Init<Game>();
  ```

- Create Entity and add component

  ```csharp
  // create a empty component
  var entity = GameCtx.Inst.CreateEntity();
  entity.Add<Player>();
  
  ....
  // create a component with value, effect the Group event and EntityIndex
  var entity = GameCtx.Inst.CreateEntity();
  var playerComp = entity.Create<Player>();
  playerComp.Id = 1,
  playerComp.Name = "Jack";
  entity.Add<Player>(playerComp);
  ```

- Replace component:

  ```csharp
  var entity = GameCtx.Inst.CreateEntity();
  entity.Add<Player>();
  ...
  var playerComp = entity.Create<Player>();
  playerComp.Id = 1,
  playerComp.Name = "Jack";
  entity.Replace<Player>(playerComp);
  ```

- Use group and match:

  ```csharp
  var group = GameCtx.Inst.GetGroup(Matchers.For<Game, Player>());
  ```

- Get a component if it is unique ( get the first entity in group )

  ```csharp
  var theOne = GameCtx.Inst.GetComp<Player>();
  ```

- Flag a empty component if it is unique

  ```csharp
  GameCtx.Inst.SetComp<Player>();
  ```

- You need create and init Entity Index manually



## TODO:

- [ ] Create and init Entity Index by code generator.