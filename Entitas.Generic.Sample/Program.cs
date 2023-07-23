using Entitas;
using Entitas.Generic;

namespace Sample
{
    [Game]
    public class Player : IComponent
    {
        public int Id;
        public string Name;
    }

    public class SayHelloSystem : IExecuteSystem
    {
        public void Execute()
        {
            var group = GameCtx.Inst.GetGroup(GameMatchers.Get<Player>());
            foreach (var item in group.GetEntities())
            {
                Console.WriteLine($"Hello {item.Get<Player>}");
            }
        }
    }

    internal static class Program
    {
        private static Systems m_Systems;

        public static async void Main(string[] args)
        {
            Contexts.Inst.Init<Game, InputScope>();

            m_Systems = new Feature().Add(new SayHelloSystem());

            await Task.Run(GameLoop);
        }

        private async static void GameLoop()
        {
            m_Systems.Initialize();
            while (true)
            {
                m_Systems.Execute();
                m_Systems.Cleanup();

                await Task.Yield();
            }
        }
    }
}
