using System;
using System.Collections.Generic;
using System.Text;

namespace Tetris
{
    abstract class Figure
    {
        const int LENGTH = 4;
        protected Point[] points = new Point[LENGTH];

        public void Draw()
        {
            foreach (Point p in points)
            {
                p.Draw();
            }
        }

        public void TryMove(Direction dir)
        {
            Hide();
            var clone = Clone();
            Move(clone, dir);
            if (VerifyPosition(clone))
                points = clone;
            Draw();
        }

        public void TryRotate()
        {
            Hide();
            var clone = Clone();
            Rotate(clone);
            if (VerifyPosition(clone))
                points = clone;
            Draw();
        }

        private bool VerifyPosition(Point[] pList)
        {
            foreach(var p in pList)
            {
                if (p.x < 0 || p.y < 0 || p.x >= Console.BufferWidth || p.y >= Console.BufferHeight)
                    return false;
            }
            return true;
        }

        private Point[] Clone()
        {
            var newPoints = new Point[LENGTH];
            for(int i = 0; i < LENGTH; i++)
            {
                newPoints[i] = new Point(points[i]);
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
        //                if (p.x <= 0)
        //                    return;
        //                break;
        //            case Direction.RIGHT:
        //                if (p.x >= w)
        //                    return;
        //                break;
        //            case Direction.DOWN:
        //                if (p.y >= h)
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
            foreach (Point p in points)
            {
                p.Hide();
            }
        }

    }
}
