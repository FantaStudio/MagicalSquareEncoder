using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MagicalSquare
{
    class MagicalSquareBase
    {
        int _side;
        int[,] _matrix;
        public int Side
        {
            get => _side;
            set
            {
                _side = value;
                if (value % 2 != 0)
                {
                    _matrix = OddSquareMethod(value);
                    return;
                }
                _matrix = EvenSquareMethod(value);
            }
        }

        public int MagicConst { get => (Side * (Side * Side + 1)) / 2; }

        public int [,] Matrix { get => _matrix; }

        public MagicalSquareBase(int side) => Side = side;

        public static int[,] EvenSquareMethod(int Side)
        {
            int[,] mainSquare = new int[Side, Side];
            if (Side % 4 >= 1) return mainSquare;

            // Заполняем квадрат полностью
            List<int> emptyCells = new List<int>();
            int num = 1;
            int i,j;
            for(i = 0; i < Side; i++)
            {
                for (j = 0; j < Side; j++)
                {
                    if ((i % 4 != j % 4) && (Side - 1 - j) % 4 != i % 4)
                        mainSquare[i, j] = num;
                    else
                        emptyCells.Add(num);
                    num++;
                }
            }

            int ind = emptyCells.Count-1;
            for(i = 0; i < Side; i++)
            {
                for(j = 0; j < Side; j++)
                {
                    if (mainSquare[i, j] == 0)
                    {
                        mainSquare[i, j] = emptyCells[ind];
                        ind--;
                    }
                }
            }
            return mainSquare;
        }

        public static int[,] OddSquareMethod(int Side)
        {
            int middle = Side / 2 + 1;
            int[,] mainSquare = new int[Side, Side];

            int x, y;
            int tempX1, tempY1, tempX2,tempY2;

            x = tempX1 = middle - 1;
            y = tempY1  = 0;

            tempX2 = Side;
            tempY2 = -1;

            for (int i = 0; i < Side * Side; i++)
            {
                // Основа
                mainSquare[y, x] = i+1;
                x++;
                y--;

                // Если упёрлись во временную середину первой части квадрата, смещаемся чуть ниже и перезаписываем середину
                if (x == tempX1 && y == tempY1)
                {
                    y += 2;
                    x--;
                    tempX1 = x;
                    tempY1 = y;
                }
                // Если упёрлись во временную середину второй части квадрата, смещаемся чуть ниже и перезаписываем середину
                else if (x == tempX2 && y == tempY2)
                {
                    y += 2;
                    x--;
                    tempX2 = x;
                    tempY2 = y;
                }
                else
                {

                    // Если упёрлись в потолок, то переходим вниз
                    if (y < 0)
                    {
                        y = Side - 1;
                    }
                    // Если упёрлись в стену, то переходим влево
                    if (x > Side - 1)
                    {
                        x = 0;
                    }
                }  
            }
            return mainSquare;
        }

        public DataTable GetDataTable()
        {
            var dt = new DataTable();
            for (var i = 0; i < Side; i++)
                dt.Columns.Add(new DataColumn("c" + i, typeof(string)));
            for (var i = 0; i < Side; i++)
            {
                var row = dt.NewRow();
                for (var c = 0; c < Side; c++)
                    row[c] = Matrix[i, c];
                dt.Rows.Add(row);
            }
            return dt;
        }
    }

    class MagicalSquareScrambler
    {
        static MagicalSquareBase _magicSquare;
        static MagicalSquareBase MagicSquare { get => _magicSquare; }

        public static int GetSideFromMessage(string message) => Convert.ToInt32(Math.Ceiling(Math.Sqrt(message.Length)));

        public static string Encode(string message)
        {
            // Создаём магический квадрат на основе длины сообщения
            int side = GetSideFromMessage(message);
            message = message.PadRight(side * side);
            _magicSquare = new MagicalSquareBase(side);

            // Кодируем строку по магическому квадрату
            string encodedStr = "";
            for(int i = 0; i < side; i++)
            {
                for (int j = 0; j < side; j++)
                    encodedStr += message[MagicSquare.Matrix[i, j] - 1];
            }
            return encodedStr;
        }

        public static string Decode(string message)
        {
            // Если магического квадрата нет, создаём новый
            if(MagicSquare == null || MagicSquare.Matrix.Length < message.Length)
                _magicSquare = new MagicalSquareBase(GetSideFromMessage(message));

            char[] decoded = new char[message.Length];
            int num = 0;
            for (int i = 0; i < MagicSquare.Side; i++)
            {
                for (int j = 0; j < MagicSquare.Side; j++)
                {
                    decoded[MagicSquare.Matrix[i, j] - 1] = message[num];
                    num++;
                }
            }
            return new string(decoded);
        }

        public static DataTable GetSquareTable(string message)
        {

            // Если магического квадрата нет, создаём новый
            int side = GetSideFromMessage(message);
            if (MagicSquare == null || MagicSquare.Matrix.Length < message.Length)
                _magicSquare = new MagicalSquareBase(side);
            char[,] matrix = new char[side, side];

            int i, j;
            var dt = new DataTable();
            for (i = 0; i < side; i++)
            {
                for (j = 0; j < side; j++)
                    matrix[i,j] = message[MagicSquare.Matrix[i, j] - 1];
            }
            for (i = 0; i < side; i++)
                dt.Columns.Add(new DataColumn("c" + i, typeof(string)));
            for (i = 0; i < side; i++)
            {
                var row = dt.NewRow();
                for (j = 0; j < side; j++)
                    row[j] = matrix[i, j];
                dt.Rows.Add(row);
            }
            return dt;
        }
    }
}
