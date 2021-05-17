using System;
using System.Threading;
using System.Timers;
using Microsoft.SmallBasic.Library;

// !!!!! Херово работает и виснет - какой-то конфликт в библиотете SmartBasic
//       Наверное она не поддерживает многопоточность ???

// - зависает в функции OnTimedEvent() здесь:
//     var result = currentFigure.TryMove(Direction.DOWN);
//     - из функции не выходит никогда, если в момент обработки была нажата кнопка и
//       произошёл вход в функцию обратного вызова GraphicsWindow_KeyDown(), которая похоже
//       плохо поддерживает стандартную многопоточность... Monitor.Enter(object)
//       - зависание происходит при вызове функции Monitor.Enter(object) в обработчике кнопок?
// https://docs.microsoft.com/ru-ru/dotnet/api/system.threading.monitor.enter?view=netframework-4.7.2
// подвисание происходит в:
// - Hide() если была нажата кнопка
// - Draw() если была нажата кнопка
// gameOver = ProcessResult(result, ref currentFigure);


namespace Tetris
{
    class Program
    {
        const int TIMER_INTERVAL = 500;
        static System.Timers.Timer timer;
        static Object lockObject = new object();

        static bool gameOver = false;
        static Figure currentFigure;
        static FigureGenerator generator = new FigureGenerator(Field.Width / 2, 0);

        static void Main(string[] args)
        {
            DrawerProvider.Drawer.InitField();
            currentFigure = generator.GetNewFigure();
            SetTimer();
            GraphicsWindow.KeyDown += GraphicsWindow_KeyDown;
        }

        private static void GraphicsWindow_KeyDown()
        {
            Console.WriteLine("Key_0");
            Monitor.Enter(lockObject);
            Console.WriteLine("Key_1");
            var result = HandleKey(currentFigure, GraphicsWindow.LastKey);
            if (GraphicsWindow.LastKey == "Down")
                gameOver = ProcessResult(result, ref currentFigure);
            Console.WriteLine("Key_2");
            Monitor.Exit(lockObject);
            Console.WriteLine("Key_3");
        }

        private static void SetTimer()
        {
            // Create a timer with a two second interval.
            timer = new System.Timers.Timer(TIMER_INTERVAL);
            // Hook up the Elapsed event for the timer. 
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine("Timer_0");
            Monitor.Enter(lockObject);
            Console.WriteLine("Timer_1");
            var result = currentFigure.TryMove(Direction.DOWN, true);
            Console.WriteLine("Timer_11");
            gameOver = ProcessResult(result, ref currentFigure);
            Console.WriteLine("Timer_12");
            if (gameOver)
                timer.Stop();
            Console.WriteLine("Timer_2");
            Monitor.Exit(lockObject);
            Console.WriteLine("Timer_3");
        }

        private static bool ProcessResult(Result result, ref Figure currentFigure)
        {
            if (result == Result.HEAP_STRIKE || result == Result.DOWN_BORDER_STRIKE)
            {
                Field.AddFigure(currentFigure);
                Field.TryDeleteLines();

                if (currentFigure.IsOnTop())
                {
                    // timer.Elapsed -= OnTimedEvent;
                    DrawerProvider.Drawer.WriteGameOver();
                    return true;
                }
                else
                {
                    currentFigure = generator.GetNewFigure();
                    return false;
                }
            }
            else
                return false;
        }

        private static Result HandleKey(Figure f, String key)
        {
            switch (key)
            {
                case "Left":
                    return f.TryMove(Direction.LEFT);
                case "Right":
                    return f.TryMove(Direction.RIGHT);
                case "Down":
                    return f.TryMove(Direction.DOWN);
                case "Space":
                    return f.TryRotate();
            }
            return Result.SUCCESS;
        }
    }
}
