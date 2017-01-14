using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricksPlayer
{
   static class Board
    {
        public static int Size { get; set; }
        public static Field[,] MyBoard { get; set; }
        public static List<int[]> blockable { get; set; }
        public static int numberOfFreeFields { get; set; }
        public static int[] lastMove { get; set; }
        public static int Middle { get; set; }


        public static void newBoard(int size)
        {
            Size = size;
            numberOfFreeFields = size * size;
            MyBoard = new Field[size,size];
            Middle = Size / 2;
            blockable = new List<int[]>();

            for (int i = 0; i < MyBoard.GetLength(0); i++)
            {
                for (int j = 0; j < MyBoard.GetLength(1); j++)
                {
                    MyBoard[i, j] = new Field();

                    if (i == 0)
                    {
                        MyBoard[i, j].isUpperFree = false ;
                    }
                    if (j == 0)
                    {
                        MyBoard[i, j].isLeftFree = false ;
                    }
                    if (i == MyBoard.GetLength(0) - 1)
                    {
                        MyBoard[i, j].isBottomFree = false ;
                    }
                    if (j == MyBoard.GetLength(1) - 1)
                    {
                        MyBoard[i, j].isRightFree = false ;
                    }
                   
                }
            }

            //foreach (var item in MyBoard)
            //{
            //    Console.WriteLine(item.ToString());
            //}
                       
        }

        private static void update(int[] moves)
        {
            if (moves[0] - 1 >= 0)
            {
                MyBoard[moves[0] - 1, moves[1]].isBottomFree = false;//góra
                if(update_list(MyBoard[moves[0] - 1, moves[1]]))
                {
                    blockable.Add(new int[] { moves[0] - 1, moves[1] });
                }
            }
            if (moves[0] + 1 < Size)
            {
                MyBoard[moves[0] + 1, moves[1]].isUpperFree = false;//dół
                if(update_list(MyBoard[moves[0] + 1, moves[1]]))
                {
                    blockable.Add(new int[] { moves[0] + 1, moves[1] });
                }
            }
            if (moves[1] - 1 >= 0)
            {
                MyBoard[moves[0], moves[1] - 1].isRightFree = false;//lewo
                if(update_list(MyBoard[moves[0], moves[1] - 1]))
                {
                    blockable.Add(new int[] { moves[0], moves[1] - 1 });
                }
            }
            if (moves[1] + 1 < Size)
            {
                MyBoard[moves[0], moves[1] + 1].isLeftFree = false;//prawo
                if(update_list(MyBoard[moves[0], moves[1] + 1]))
                {
                    blockable.Add(new int[] { moves[0], moves[1] + 1 });
                }
            }
            if (moves[2] - 1 >= 0)
            {
                MyBoard[moves[2] - 1, moves[3]].isBottomFree = false;//góra
                if(update_list(MyBoard[moves[2] - 1, moves[3]]))
                {
                    blockable.Add(new int[] { moves[2] - 1, moves[3] });
                }
            }
            if (moves[2] + 1 < Size)
            {
                MyBoard[moves[2] + 1, moves[3]].isUpperFree = false;//dół
                if(update_list(MyBoard[moves[2] + 1, moves[3]]))
                {
                    blockable.Add(new int[] {moves[2] + 1, moves[3] });

                }
            }
            if (moves[3] - 1 >= 0)
            {
                MyBoard[moves[2], moves[3] - 1].isRightFree = false;//lewo
                if(update_list(MyBoard[moves[2], moves[3] - 1]))
                {
                    blockable.Add(new int[] { moves[2], moves[3] - 1 });
                }
            }
            if (moves[3] + 1 < Size)
            {
                MyBoard[moves[2], moves[3] + 1].isLeftFree = false;//prawo
                if(update_list(MyBoard[moves[2], moves[3] + 1]))
                {
                    blockable.Add(new int[] { moves[2], moves[3] + 1 });
                }
            }
        }

        /// <summary>
        /// Sprawdzam, czy pole jest blokowalne
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        private static Boolean update_list(Field field)
        {
            if (field.isBlockable())
            {
                if (field.isOccupied)
                {
                    numberOfFreeFields--;
                    return false;
                }
                return true;
            }        
            return false;
                    
        }  
        /// <summary>
        /// Odświeżam, liste blokowalnych pól
        /// </summary>
        public static void blockableRefresh()
        {
            for (int i = 0; i <  blockable.Count; i++)
            {
                if (!MyBoard[blockable[i][0], blockable[i][1]].isBlockable())
                {
                    blockable.RemoveAt(i);
                    i--;
                }                    
            }
        }

        public static void saveNewMove(int[] moves)
        {

            try
            {
                blockableRefresh();
                update(moves);                
                lastMove = moves;
                MyBoard[moves[0], moves[1]].OccupyField();
                numberOfFreeFields--;

                MyBoard[moves[2], moves[3]].OccupyField();
                numberOfFreeFields--;
            }
            catch (NullReferenceException)
            {
                Console.WriteLine("NULLLLLLLLLL");
            }
        }


    }
}
