namespace Entitas.Generic
{
    /// <summary>
    /// Generic Entity
    /// </summary>
    /// <typeparam name="TScope">Scope of entity</typeparam>
    public class Entity<TScope> : Entitas.Entity where TScope : IScope
    {
        public TComponent Add<TComponent>() where TComponent : IComponent, new()
        {
            var index = ComponentIdx<TScope, TComponent>.Id;
            var component = CreateComponent<TComponent>(index);
            AddComponent(index, component);
            return Get<TComponent>();
        }
        
        public Entity<TScope> Add<TComponent>(TComponent comp) where TComponent : IComponent, new()
        {
            var index = ComponentIdx<TScope, TComponent>.Id;
            AddComponent(index, comp);
            return this;
        } 
        
        public Entity<TScope> Replace<TComponent>(TComponent comp) where TComponent : IComponent, new()
        {
            var index = ComponentIdx<TScope, TComponent>.Id;
            ReplaceComponent(index, comp);
            return this;
        }

        public TComponent Create<TComponent>() where TComponent : IComponent, new()
        {
            return CreateComponent<TComponent>(ComponentIdx<TScope, TComponent>.Id);
        }

        public Entity<TScope> Remove<TComponent>() where TComponent : IComponent, new()
        {
            RemoveComponent(ComponentIdx<TScope, TComponent>.Id);
            return this;
        }

        public TComponent Get<TComponent>() where TComponent : IComponent, new()
        {
            return (TComponent)GetComponent(ComponentIdx<TScope, TComponent>.Id);
        }

        public bool Has<TComponent>() where TComponent : IComponent, new()
        {
            return HasComponent(ComponentIdx<TScope, TComponent>.Id);
        }
    }
}