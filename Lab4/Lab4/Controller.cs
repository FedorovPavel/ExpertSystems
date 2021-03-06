﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    class Controller
    {
        private int C = 5;
        private double Eps = 10e-3;
        private double M = 2;
        private List<Point> startCentroid = new List<Point>();
        private List<Point> vectorPoint = new List<Point>();
        private List<List<double>> matrix = new List<List<double>>();
        
        public Controller()
        {
            initClustering();
        }

        public void Add(Point point)
        {
            this.vectorPoint.Add(point);
            this.matrix.Add(new List<double>(this.C));
            for (int I = 0; I < this.C; I++)
            {
                this.matrix[this.matrix.Count - 1].Add(0);
            }
            
            return;
        }

        public void Clear()
        {
            this.vectorPoint.Clear();
            this.startCentroid.Clear();
            this.matrix.Clear();
            initClustering();
        }

        private void initMatrix()
        {
            for (int I = 0; I < this.matrix.Count; I++)
            {
                for (int J = 0; J < this.matrix[I].Count; J++)
                {
                    matrix[I][J] = 0;
                }
            }

            matrix[0][0] = 1;
            matrix[1][1] = 1;
            matrix[2][2] = 1;
            matrix[3][3] = 1;
            matrix[4][4] = 1;
        }

        public List<List<double>> Calculate()
        {
            initMatrix();
            bool state = true;
            List<double> distance = new List<double>(this.C);
            double prev = FindMaxU();
            double current = prev;
            while(state) 
            {
                prev = current;
                CorrectCenterOfCluster();
                RefreshMatrix();

                current = FindMaxU();
                if (Math.Abs(current - prev) < this.Eps)
                {
                    state = false;
                }
            }

            
            return matrix;
        }

        public List<Point> getVectorPoint()
        {
            return vectorPoint;
        }

        //public double MinFunc()
        //{
        //    double res = 0;
        //    for (int I = 0; I < this.C; I++)
        //    {
        //        for (int K = 0; K < this.vectorPoint.Count; K++)
        //        {
        //            res += Math.Pow(matrix[I][K], M) * Distance(this.vectorPoint[I], this.startCentroid[K]);
        //        }
        //    }

        //    return res;
        //}

        private void RefreshMatrix()
        {
            for (int K = 0; K < this.vectorPoint.Count; K++) 
            {
                for (int I = 0; I < this.C; I++)
                {
                    double diveder = 0;
                    double top = 0;
                    double bottom = 0;
                    for (int J = 0; J < this.C; J++)
                    {
                        top = Distance(this.vectorPoint[K], this.startCentroid[I]);
                        bottom = Distance(this.vectorPoint[K], this.startCentroid[J]);
                        if (bottom != 0)
                            diveder += Math.Pow((top / bottom), (2.0 / (M - 1)));
                    }

                    if (diveder != 0)
                        matrix[K][I] = 1 / diveder;
                    else
                        matrix[K][I] = 1;
                }
            }
        }

        private double Distance(Point x, Point c)
        {
            double a = Math.Pow(x.AR - c.AR, 2);
            double b = Math.Pow(x.DR - c.DR, 2);
            double y = Math.Sqrt(a + b);
            double res = Math.Sqrt(Math.Pow(y, 2));
            return res;
        }

        private void CorrectCenterOfCluster()
        {
            for (int I = 0; I < this.startCentroid.Count; I++)
            {
                double x = 0;
                double y = 0;
                double del = 0;
                for (int K = 0; K < this.vectorPoint.Count; K++)
                {
                    x += (Math.Pow(this.matrix[K][I], M) * this.vectorPoint[K].AR);
                    y += (Math.Pow(this.matrix[K][I], M) * this.vectorPoint[K].DR);
                    del += Math.Pow(this.matrix[K][I], M);
                }


                this.startCentroid[I].AR = x / del;
                this.startCentroid[I].DR = y / del; 
            }

            return;
        }

        private double FindMaxU()
        {
            double res = matrix[0][0];
            for (int K = 0; K < this.vectorPoint.Count; K++)
            {
                for (int J = 0; J < this.C; J++)
                {
                    if (res < matrix[K][J])
                    {
                        res = matrix[K][J];
                    }
                }
            }

            return res;
        }
        private void initClustering()
        {
            //  No
            this.startCentroid.Add(new Point(20, 80));
            this.vectorPoint.Add(new Point(20, 80));
            this.matrix.Add(new List<double>(C));
            //  Low
            this.startCentroid.Add(new Point(45, 60));
            this.vectorPoint.Add(new Point(45, 60));
            this.matrix.Add(new List<double>(C));
            //  Minor
            this.startCentroid.Add(new Point(55, 35));
            this.vectorPoint.Add(new Point(55, 35));
            this.matrix.Add(new List<double>(C));
            //  Average
            this.startCentroid.Add(new Point(65, 15));
            this.vectorPoint.Add(new Point(65, 15));
            this.matrix.Add(new List<double>(C));
            //  High
            this.startCentroid.Add(new Point(85, 0));
            this.vectorPoint.Add(new Point(85, 0));
            this.matrix.Add(new List<double>(C));

            for (int I = 0; I < this.C; I++)
            {
                for (int J = 0; J < this.C; J++)
                {
                    if (I != J)
                        this.matrix[I].Add(0);
                    else
                        this.matrix[I].Add(1);
                }
            }
            return;
        }
    }
}
