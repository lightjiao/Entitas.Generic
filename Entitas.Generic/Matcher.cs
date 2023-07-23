using Entitas;

namespace Entitas.Generic
{
    public class Matcher<TScope, TComponent> where TScope : IScope where TComponent : IComponent, new()
    {
        private static IMatcher<Entity<TScope>> _cached;

        public static IMatcher<Entity<TScope>> Instance
        {
            get
            {
                if (_cached == null)
                {
                    var matcher = (Matcher<Entity<TScope>>)Matcher<Entity<TScope>>.AllOf(ComponentIdx<TScope, TComponent>.Id);
                    matcher.componentNames = ComponentTypeManager<TScope>.ComponentNames;
                    _cached = matcher;
                }

                return _cached;
            }
        }
    }

    public class Matchers
    {
        public static IMatcher<Entity<TScope>> Get<TScope, TComponent>() where TScope : IScope where TComponent : IComponent, new()
        {
            return Matcher<TScope, TComponent>.Instance;
        }

        public static IAllOfMatcher<Entity<TScope>> AllOf<TScope>(params IMatcher<Entity<TScope>>[] matchers) where TScope : IScope
        {
            return Matcher<Entity<TScope>>.AllOf(matchers);
        }

        public static IAnyOfMatcher<Entity<TScope>> AnyOf<TScope>(params IMatcher<Entity<TScope>>[] matchers) where TScope : IScope
        {
            return Matcher<Entity<TScope>>.AnyOf(matchers);
        }
    }
}