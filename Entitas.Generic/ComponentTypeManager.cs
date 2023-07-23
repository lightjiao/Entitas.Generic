using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Entitas;

// ReSharper disable StaticMemberInGenericType
namespace Entitas.Generic
{
    /// <summary>
    /// Component type manager: manages component types per scope
    /// </summary>
    /// <typeparam name="TScope">Scope</typeparam>
    public static class ComponentTypeManager<TScope> where TScope : IScope
    {
        private static int _LastComponentIdx;
        private static readonly List<Type> _ComponentIdxType = new();

        public static int TotalComponents => _ComponentIdxType.Count;
        public static string[] ComponentNames { get; private set; }
        public static Type[] ComponentTypes { get; private set; }

        public static void AutoScan()
        {
            foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                foreach (var type in assembly.GetTypes())
                {
                    if (type.GetInterface(nameof(IComponent)) == null)
                    {
                        continue;
                    }

                    if (type.GetCustomAttribute(typeof(TScope)) == null)
                    {
                        continue;
                    }

                    Register(type);
                }
            }

            ComponentTypes = _ComponentIdxType.Select(x => x.GetGenericArguments()[1]).ToArray();
            ComponentNames = ComponentTypes.Select(x => x.Name).ToArray();
        }

        private static void Register(Type compType)
        {
            var componentType = typeof(ComponentIdx<,>);
            var genericType = componentType.MakeGenericType(typeof(TScope), compType);
            if (_ComponentIdxType.Contains(genericType))
            {
                return;
            }

            _ComponentIdxType.Add(genericType);

            var fieldInfo = genericType.GetField("Id", BindingFlags.Static | BindingFlags.Public);
            if (fieldInfo == null)
            {
                throw new Exception(string.Format("Type `{0}' does not contains `Id' field", genericType.Name));
            }

            fieldInfo.SetValue(null, _LastComponentIdx);
            _LastComponentIdx++;
        }
    }
}