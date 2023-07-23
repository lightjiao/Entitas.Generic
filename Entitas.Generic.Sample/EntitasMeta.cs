using Entitas;
using Entitas.Generic;

namespace Sample
{
    public class Game : Attribute, IScope
    {
    }
    public class InputScope : Attribute, IScope
    {
    }

    public static class GameCtx
    {
        public static ScopedCtx<Game> Inst => ScopedCtx<Game>.Inst;
    }

    public class GameMatchers
    {
        public static IMatcher<Entity<Game>> Get<TComponent>() where TComponent : IComponent, new()
        {
            return Matchers.Get<Game, TComponent>();
        }

        public static IAllOfMatcher<Entity<Game>> AllOf(params IMatcher<Entity<Game>>[] matchers)
        {
            return Matchers.AllOf(matchers);
        }

        public static IAnyOfMatcher<Entity<Game>> AnyOf(params IMatcher<Entity<Game>>[] matchers)
        {
            return Matchers.AnyOf(matchers);
        }
    }
}
