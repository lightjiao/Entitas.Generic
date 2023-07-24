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
                Console.WriteLine($"Hello {item.Get<Player>().Name}");
            }
        }
    }

    public class Program
    {
        private static Systems m_Systems;

        public static void Main(string[] args)
        {
            GameLoop().GetAwaiter().GetResult();
        }

        private async static Task GameLoop()
        {
            Contexts.Inst.Init<Game, InputScope>();

            var jack = GameCtx.Inst.CreateEntity();
            var player = jack.Add<Player>();
            player.Id = 1;
            player.Name = "Jack";

            m_Systems = new Feature().Add(new SayHelloSystem());
            
            m_Systems.Initialize();
            while (true)
            {
                m_Systems.Execute();
                m_Systems.Cleanup();

                await Task.Delay(500);
            }
        }
    }
}
