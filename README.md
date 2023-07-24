## Waht is this?
Inspired by [Entitas.Generic](https://github.com/yosadchyi/Entitas.Generic), and I make API more simple. Work perfectly with Native Entitas VisualDebug

> If you use Unity 2022.2 or more, Entitas VisualDebug may have BUG, see this: [sschmid/Entitas#1067 (comment)](https://github.com/sschmid/Entitas/issues/1067#issuecomment-1623734894)

## How to install?
Download and put this project in your unity Asset directory.

## Code sample 
-  There is a simple .Net6 sample in Entitas.Generic.Sample directory.

## Use guide
- Install [Entitas](https://github.com/sschmid/Entitas) before this.

- Copy this Entitas.Generic directory into your project.

- Copy `EntitasMeta.cs` file into your project if you want.

- ❗Notice: if you want use `EntityIndex`, you need Add or Replace component like below:
  ```csharp
  var entity = GameCtx.Inst.CreateEntity();

  var playerComp = entity.Create<Player>();
  playerComp.Id = 1,
  playerComp.Name = "Jack";
  
  entity.Replace(playerComp);
  ```

- Get a component if it is unique ( get the first entity in group )
  ```csharp
  var theOne = GameCtx.Inst.GetComp<Player>();
  ```

- Flag a empty component if it is unique
  ```csharp
  GameCtx.Inst.SetComp<Player>();
  ```

- You need create and init Entity Index manually(Luckly it is simple and don't need change frequently)

## TODO:

- [ ] Create and init Entity Index by code generator.
