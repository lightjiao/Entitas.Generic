﻿using Entitas;

namespace Entitas.Generic
{
    /// <summary>
    /// ComponentType class holds information about component type id in static property.
    /// </summary>
    /// <typeparam name="TScope">Scope</typeparam>
    /// <typeparam name="TComponent">Component type</typeparam>
    internal class ComponentIdx<TScope, TComponent> where TScope : IScope where TComponent : IComponent, new()
    {
        public static int Id;
    }
}