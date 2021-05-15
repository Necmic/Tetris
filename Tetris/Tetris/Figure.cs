using System;
using System.Collections.Generic;
using System.Text;

namespace Tetris
{
    abstract class Figure
    {
        const int LENGTH = 4;
        public Point[] Points = new Point[LENGTH];

        public void Draw()
        {
            foreach (Point p in Points)
            {
                p.Draw();
            }
        }

        public Result TryMove(Direction dir)
        {
            Hide();
            var clone = Clone();
            Move(clone, dir);

            var result = VerifyPosition(clone);
            if (result == Result.SUCCESS)
                Points = clone;

            Draw();
            return result;
        }

        public Result TryRotate()
        {
            Hide();
            var clone = Clone(); 
            Rotate(clone);

            var result = VerifyPosition(clone);
            if (result == Result.SUCCESS)
                Points = clone;

            Draw();
            return result;
        }

        private Result VerifyPosition(Point[] newPoints)
        {
            foreach(var p in newPoints)
            {
                if (p.Y >= Field.Height)
                    return Result.DOWN_BORDER_STRIKE;

                if (p.X >= Field.Width || p.X < 0 || p.Y < 0)
                    return Result.BORDER_STRIKE;

                if (Field.CheckStrike(p))
                    return Result.HEAP_STRIKE;
            }
            return Result.SUCCESS;
        }

        private Point[] Clone()
        {
            var newPoints = new Point[LENGTH];
            for(int i = 0; i < LENGTH; i++)
            {
                newPoints[i] = new Point(Points[i]);
            }
            return newPoints;
        }

        public void Move(Point[] pList, Direction dir)
        {
            foreach (var p in pList)
            {
                p.Move(dir);
            }
        }

        //public void TryMove(Direction dir)
        //{
        //    // Моя реализация контроля границ окна
        //    int w = Console.BufferWidth - 1;
        //    int h = Console.BufferHeight - 1;
        //    foreach (Point p in points)
        //    {
        //        switch (dir)
        //        {
        //            case Direction.LEFT:
        //                if (p.X <= 0)
        //                    return;
        //                break;
        //            case Direction.RIGHT:
        //                if (p.X >= w)
        //                    return;
        //                break;
        //            case Direction.DOWN:
        //                if (p.Y >= h)
        //                    return;
        //                break;
        //        }
        //    }
        //    Hide();
        //    foreach (Point p in points)
        //    {
        //        p.Move(dir);
        //    }
        //    Draw();
        //}

        public abstract void Rotate(Point[] pList);

        public void Hide()
        {
            foreach (Point p in Points)
            {
                p.Hide();
            }
        }

    }
}
