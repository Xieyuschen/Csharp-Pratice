using System;
using System.Collections;
using System.Text;

namespace Advance_Csharp
{
    class Class1
    {
        public class Game
        {
            private IEnumerator _circle;
            private IEnumerator _cross;
            public Game()
            {
                _circle = Circle();
                _cross =Cross();
            }
            private int _move;
            const int maxMoves = 8;


            public IEnumerator Cross()
            {
                while (true)
                {
                    Console.WriteLine($"Cross,move {_move};");
                    _move++;
                    yield return _circle;
                }
            }
            public IEnumerator Circle()
            {
                while (true)
                {
                    Console.WriteLine($"Circle,move {_move};");
                    _move++;
                    yield return _cross;
                }
            }

        }
        public static void Main()
        {
            Game game = new Game();
            //像这种game.Cross()并没有进入函数
            IEnumerator enumerator = game.Cross();
            //MoveNext是如何定义的？
            while (enumerator.MoveNext())
            {
                enumerator = enumerator.Current as IEnumerator;
            }
        }
    }
}
