using System;
using System.Threading;

namespace Tetris
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(40, 30);
            Console.SetBufferSize(40, 30);
            Console.CursorVisible = false;

            FigureGenerator generator = new FigureGenerator(Console.BufferWidth / 2, 0, '*');
            Figure currentFigure = generator.GetNewFigure();

            while (true)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey();
                    HandleKey(currentFigure, key);
                }
            }
        }

        private static void HandleKey(Figure currentFigure, ConsoleKeyInfo key)
        {
            switch (key.Key)
            {
                case ConsoleKey.LeftArrow:
                    currentFigure.TryMove(Direction.LEFT);
                    break;
                case ConsoleKey.RightArrow:
                    currentFigure.TryMove(Direction.RIGHT);
                    break;
                case ConsoleKey.DownArrow:
                    currentFigure.TryMove(Direction.DOWN);
                    break;
            }
        }

        //static void FigureFall(out Figure fig, FigureGenerator gen)
        //{
        //    fig = gen.GetNewFigure();
        //    fig.Draw();

        //    for (int i = 0; i < 15; i++)
        //    {
        //        fig.Hide();
        //        fig.Move(Direction.DOWN);
        //        fig.Draw();
        //        Thread.Sleep(200);
        //    }
        //}
    }
}
