using System;
using Entitas;

namespace Entitas.Generic
{
    /// <summary>
    /// Context for given scope
    /// </summary>
    /// <typeparam name="TScope"></typeparam>
    public class ScopedCtx<TScope> : Context<Entity<TScope>> where TScope : IScope
    {
        public static ScopedCtx<TScope> Inst { get; private set; }

        public ScopedCtx(Func<IEntity, IAERC> aercFactory) : base(
            ComponentTypeManager<TScope>.TotalComponents,
            1,
            new ContextInfo(
                typeof(TScope).Name,
                ComponentTypeManager<TScope>.ComponentNames,
                ComponentTypeManager<TScope>.ComponentTypes
            ),
            aercFactory,
            () => new Entity<TScope>()
        )
        {
            Inst = this;
        }

        public TComponent CreateComp<TComponent>() where TComponent : IComponent, new()
        {
            if (GetComp<TComponent>() != null)
            {
                throw new Exception($"Unique Component {typeof(TComponent)} already exist");
            }

            var entity = CreateEntity();
            return entity.Add<TComponent>();
        }

        public Entity<TScope> GetEntity<TComponent>() where TComponent : IComponent, new()
        {
            return GetGroup(Matchers.For<TScope, TComponent>()).GetSingleEntity();
        }

        public TComponent GetComp<TComponent>() where TComponent : IComponent, new()
        {
            var entity = GetEntity<TComponent>();
            return entity != null ? entity.Get<TComponent>() : default;
        }

        public void SetComp<TComponent>(bool value) where TComponent : IComponent, new()
        {
            var entity = GetEntity<TComponent>();
            if (value && entity == null)
            {
                CreateEntity().Add<TComponent>();
                return;
            }

            if (value == false && entity != null)
            {
                entity.Destroy();
            }
        }

        public bool Has<TComponent>() where TComponent : IComponent, new()
        {
            return GetEntity<TComponent>() != null;
        }
    }
}