using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


    class MapGenerator
    {
 
        public static int[,] GenerateStartPoints(int mapHeight, int mapWidth, int biomeCount)
        {

            int[,] matr = new int[mapHeight, mapWidth];
            int[,] coordinates = new int[biomeCount, 2];
           
            Random r = new Random();
            r.Next(0, mapHeight - 1);

            for (int i = 0; i < biomeCount; i++)
            {
                do
                {
                    coordinates[i, 0] = r.Next(0, mapHeight - 1);
                    coordinates[i, 1] = r.Next(0, mapWidth - 1);
                }
                while (CheckForMatches(coordinates, i));
            }

            for (int i = 0; i < biomeCount; i++)
            {
                matr[coordinates[i, 0], coordinates[i, 1]] = i + 1;
            }

            return matr;

        }

        static bool CheckForMatches(int[,] coordinates, int pointNum)
        {
            for (int i = 0; i < coordinates.GetLength(0); i++)
            {
                if (i != pointNum) if (coordinates[pointNum, 0] == coordinates[i, 0] || coordinates[pointNum, 1] == coordinates[i, 1]) return true;
            }
            return false;
        }

        static bool CheckForEnding(int[,] matr, int biomeCount, int NotFinishedCount) //Проверка, закончена ли генерация. 
        {
     //      return NotFinishedCount != 0?  false: true;
           for (int i = 0; i < matr.GetLength(0); i++)
           {
                for (int j = 0; j < matr.GetLength(1); j++)
               {
                  if ((matr[i, j] > -2) && (matr[i, j] < biomeCount + 1))
                   {
                        return false;
                   }
               }
           }
           return true;
        }

     public static int[,] GenerateMap(ref int[,] matr, int biomeCount)
        {
            int NotFinishedCount = matr.GetLength(0) * matr.GetLength(1);
            Random r = new Random();

            //step 1 Проверка, закончена ли генерация. 
            while (!CheckForEnding(matr, biomeCount, NotFinishedCount))
            {

                //step 2 Заполнение пустых точек рядом с биомами 

                for (int i = 0; i < matr.GetLength(0); i++)
                {
                    for (int j = 0; j < matr.GetLength(1); j++)
                    {
                        if (matr[i, j] > 0 && matr[i, j] < biomeCount + 1)
                        {
                            if (i != 0)
                            {
                                if (matr[i - 1, j] == 0) matr[i - 1, j] = -1;
                                if (j != 0) if (matr[i - 1, j - 1] == 0) matr[i - 1, j - 1] = -1;
                                if (j != matr.GetLength(1) - 1) if (matr[i - 1, j + 1] == 0) matr[i - 1, j + 1] = -1;
                            }

                            if (i != matr.GetLength(0) - 1)
                            {
                                if (matr[i + 1, j] == 0) matr[i + 1, j] = -1;
                                if (j != 0) if (matr[i + 1, j - 1] == 0) matr[i + 1, j - 1] = -1;
                                if (j != matr.GetLength(1) - 1) if (matr[i + 1, j + 1] == 0) matr[i + 1, j + 1] = -1;
                            }


                            if (j != 0)
                            {
                                if (matr[i, j - 1] == 0) matr[i, j - 1] = -1;

                            }

                            if (j != matr.GetLength(1) - 1)
                            {
                                if (matr[i, j + 1] == 0) matr[i, j + 1] = -1;
                            }


                        }
                    }
                }
                //step 3 Превращение тайлов в готовые 
                for (int i = 0; i < matr.GetLength(0); i++)
                {
                    for (int j = 0; j < matr.GetLength(1); j++)
                    {
                        if (matr[i, j] > 0 && matr[i, j] < biomeCount + 1)
                        {
                            matr[i, j] += biomeCount;
                            NotFinishedCount--;
                        }

                    }
                }
                //step 4 Борьба за спорные тайлы 
                //(за каждый тайл n-го биома вокруг спорной клетки, этому биому даются очки, и тайл становится частью биома с большим кол-вом очков чем у остальных) 
                int n = 2;
                for (int i = 0; i < matr.GetLength(0); i++)
                {
                    for (int j = 0; j < matr.GetLength(1); j++)
                    {
                        if (matr[i, j] == -1)
                        {
                            int[] biomes = new int[biomeCount];

                            if (i != 0)
                            {
                                if (matr[i - 1, j] > 0) { int biome = (matr[i - 1, j] > biomeCount) ? matr[i - 1, j] - biomeCount : matr[i - 1, j]; biomes[biome - 1] += r.Next(0, n); }
                                if (j != 0) if (matr[i - 1, j - 1] > 0) { int biome = (matr[i - 1, j - 1] > biomeCount) ? matr[i - 1, j - 1] - biomeCount : matr[i - 1, j - 1]; biomes[biome - 1] += r.Next(0, n); }
                                if (j != matr.GetLength(1) - 1) if (matr[i - 1, j + 1] > 0) { int biome = (matr[i - 1, j + 1] > biomeCount) ? matr[i - 1, j + 1] - biomeCount : matr[i - 1, j + 1]; biomes[biome - 1] += r.Next(0, n); }
                            }

                            if (i != matr.GetLength(0) - 1)
                            {
                                if (matr[i + 1, j] > 0) { int biome = (matr[i + 1, j] > biomeCount) ? matr[i + 1, j] - biomeCount : matr[i + 1, j]; biomes[biome - 1] += r.Next(0, n); }
                                if (j != 0) if (matr[i + 1, j - 1] > 0) { int biome = (matr[i + 1, j - 1] > biomeCount) ? matr[i + 1, j - 1] - biomeCount : matr[i + 1, j - 1]; biomes[biome - 1] += r.Next(0, n); }
                                if (j != matr.GetLength(1) - 1) if (matr[i + 1, j + 1] > 0) { int biome = (matr[i + 1, j + 1] > biomeCount) ? matr[i + 1, j + 1] - biomeCount : matr[i + 1, j + 1]; biomes[biome - 1] += r.Next(0, n); }
                            }


                            if (j != 0)
                            {
                                if (matr[i, j - 1] > 0) { int biome = (matr[i, j - 1] > biomeCount) ? matr[i, j - 1] - biomeCount : matr[i, j - 1]; biomes[biome - 1] += r.Next(0, n); }

                            }

                            if (j != matr.GetLength(1) - 1)
                            {
                                if (matr[i, j + 1] > 0) { int biome = (matr[i, j + 1] > biomeCount) ? matr[i, j + 1] - biomeCount : matr[i, j + 1]; biomes[biome - 1] += r.Next(0, n); }
                            }
                            if (!AllEquals(biomes)) { matr[i, j] = GetHighterValueIndex(biomes);  }

                        }
                    }
                }



            }
            return matr;

        }
        static int GetHighterValueIndex(int[] biomes)
        {

            int index = 0;
            for (int i = 0; i < biomes.GetLength(0); i++)
            {
                if (biomes[index] < biomes[i])  index = i; 
            }
            return index + 1;
        }

        static bool AllEquals(int[] biomes)
        {
            for (int i = 0; i < biomes.GetLength(0); i++)
            {
                if (biomes[0] != biomes[i]) return false;
            }
            return true;
        }

    }

