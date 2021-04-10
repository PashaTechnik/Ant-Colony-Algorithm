using System;
using System.Drawing;
using System.IO;

namespace Ant_Colony_Algorithm
{
    public class Cities
    {
        //массив коррдинат городов
        public Point[] Coordinate;
        

        public Cities(int N, string Path)
        {
            int i = 0;
            Coordinate = new Point[N];
            using (StreamReader sr = new StreamReader(Path, System.Text.Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] point = line.Split(" ");
                    Coordinate[i].X = (int)double.Parse(point[0]);
                    Coordinate[i].Y = (int)double.Parse(point[1]);
                    i++;
                }
            }
            // Coordinate[0].X = 1;
            // Coordinate[0].Y = 1;
            //
            // Coordinate[1].X = 2;
            // Coordinate[1].Y = 2;
            //
            // Coordinate[2].X = 5;
            // Coordinate[2].Y = 5;
            //
            // Coordinate[3].X = 3;
            // Coordinate[3].Y = 5;

        }
    }
}