﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Tetris
{
    public class Point
    {
        public int x;
        public int y;
        public char c;

        public void Draw()
        { 
            Console.SetCursorPosition(x, y);
            Console.Write(c);
        }

        public Point(int a, int b, char sym)
        {
            x = a;
            y = b;
            c = sym;
        }

        public Point() { }

        public void Move(Direction dir)
        {
            switch (dir)
            {
                case Direction.LEFT:
                    x -= 1;
                    break;
                case Direction.RIGHT:
                    x += 1;
                    break;
                case Direction.DOWN:
                    y += 1;
                    break;
            }
        }

        public void Hide()
        {
            Console.SetCursorPosition(x, y);
            Console.Write(' ');
        }
    }
}
