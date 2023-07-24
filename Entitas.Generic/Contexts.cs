using Entitas;
using System.Diagnostics;

namespace Entitas.Generic
{
    public class Contexts
    {
        private static Contexts _Inst;
        public static Contexts Inst => _Inst ??= new Contexts();

        public void Init<TS1, TS2>() where TS1 : IScope where TS2 : IScope
        {
            InitScope<TS1>();
            InitScope<TS2>();
        }

        private void InitScope<TScope>() where TScope : IScope
        {
            ComponentTypeManager<TScope>.AutoScan();
            var context = new ScopedCtx<TScope>(AERCFactories.SafeAERCFactory);

            InitScopeObserver(context);
        }

        private void InitScopeObserver(Entitas.IContext context)
        {
#if UNITY_EDITOR
            if (UnityEngine.Application.isPlaying)
            {
                var observer = new Entitas.VisualDebugging.Unity.ContextObserver(context);
                UnityEngine.Object.DontDestroyOnLoad(observer.gameObject);
            }
        }
#endif
        }
    }
}