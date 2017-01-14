using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricksPlayer
{
    class Movement
    {
        public Boolean isFirst { get; set; }
        public int[] nextMove { get; set; }

        public Movement(Boolean isFirst)
        {
            nextMove = new int[4] { -1, -1, -1, -1};
            this.isFirst = isFirst;
        }

        public int[] MakeMove()
        {
            nextMove = new int[4] { -1, -1, -1, -1 };
            if (this.isFirst)
            {
                return playingAsFirst();
            }
            else
            {
                return playingAsSecond();

            }
        }

        private Boolean blockField()
        {
            int[] block = Board.blockable[Board.blockable.Count - 1];//najnowszy blokowalny
            Board.blockable.RemoveAt(Board.blockable.Count - 1);//usuwam go

            Field temp = Board.MyBoard[block[0], block[1]];
            int[] nowy = whichIsFree(temp, block, new int[] { -1, -1 }); // dwie pierwsze wsp

          

            Field temp2 = Board.MyBoard[nowy[0], nowy[1]];
            

            int[] nowy2 = whichIsFree(temp2, nowy, block);// dwie drugie wsp

            if (nowy2 != null)
            {
                nextMove[0] = nowy[0];
                nextMove[1] = nowy[1];
                nextMove[2] = nowy2[0];
                nextMove[3] = nowy2[1];
                return true;
            }
            else
            {
                return false;
            }

            
            }

        private int[] whichIsFree(Field temp, int[] nowy, int[] block)
        {
            if (temp != null)
            {
                if (temp.isUpperFree)
                {
                    if(block[0] != nowy[0] - 1)
                        return new int[] { nowy[0] - 1, nowy[1] };                    
                   
                }
                if (temp.isRightFree)
                {
                    if (block[1] != nowy[1] + 1)
                        return new int[] { nowy[0], nowy[1] + 1 };          
                    
                }
                if (temp.isBottomFree)
                {
                    if (block[0] != nowy[0] + 1 )
                        return new int[] { nowy[0] + 1, nowy[1] };                                     
                }
                if (temp.isLeftFree)
                {
                    if ( block[1] != nowy[1] - 1)
                        return new int[] { nowy[0], nowy[1] - 1 };                                  
                }
            }
            
            return null;

        }



        private int[] playingAsSecond()
        {
            int[] move = new int[] {
            Board.Size - Board.lastMove[0] - 1,
            Board.Size - Board.lastMove[1] - 1,
            Board.Size - Board.lastMove[2] - 1,
            Board.Size - Board.lastMove[3] - 1  };

           if(move[0] == Board.Middle && move[1] == Board.Middle)
            {
                this.isFirst = false;
                return playingAsFirst();
            }

            if (move[2] == Board.Middle && move[3] == Board.Middle)
            {
                this.isFirst = false;
                return playingAsFirst();
            }

            return move;
            //return move[0]  + 1  + " " + (move[1] + 1) + " " + (move[2] + 1) + " " + (move[3] + 1);

        }

        private int[] playingAsFirst()
        {

            if(Board.numberOfFreeFields < 20)
            {
                if((Board.numberOfFreeFields / 2) % 2 == 0)
                {
                    if (Board.blockable.Count > 0)
                    {
                        if(blockField())
                            return nextMove;
                    }
                }
            }
            
            int moore = 1;            

            for (int i = moore; i <= Board.Middle; i++)
            {
                Field temp = searchForFreeField(i);
                if(temp != null)
                {
                    if(temp.isUpperFree)
                    {
                        nextMove[2] = nextMove[0] - 1;
                        nextMove[3] = nextMove[1];
                        break;
                    }
                    if (temp.isRightFree)
                    {
                        nextMove[2] = nextMove[0];
                        nextMove[3] = nextMove[1] + 1;
                        break;
                    }
                    if (temp.isBottomFree)
                    {
                        nextMove[2] = nextMove[0] + 1;
                        nextMove[3] = nextMove[1];
                        break;
                    }
                    if (temp.isLeftFree)
                    {
                        nextMove[2] = nextMove[0];
                        nextMove[3] = nextMove[1] - 1;
                        break;
                    }
                }
            }

            return nextMove;
            //return nextMove[0]+1  + " " + (nextMove[1]+1) +   " " + (nextMove[2]+1) + " " + (nextMove[3]+1) ;
        }

        private Field searchForFreeField(int moore)
        {
            
            for (int i = -moore; i < moore; i++)
            {
                if(!Board.MyBoard[Board.Middle - moore, Board.Middle + i].isOccupied)//gora
                {
                    nextMove[0] = Board.Middle - moore;
                    nextMove[1] = Board.Middle + i;
                    return Board.MyBoard[Board.Middle - moore, Board.Middle + i];
                }

                if (!Board.MyBoard[Board.Middle + i, Board.Middle + moore].isOccupied)//prawo
                {
                    nextMove[0] = Board.Middle + i;
                    nextMove[1] = Board.Middle + moore;
                    return Board.MyBoard[Board.Middle + i, Board.Middle + moore];
                }

                if (!Board.MyBoard[Board.Middle + moore, Board.Middle + i].isOccupied)//dół
                {
                    nextMove[0] = Board.Middle + moore;
                    nextMove[1] = Board.Middle + i;
                    return Board.MyBoard[Board.Middle + moore, Board.Middle + i];
                }
                                
                if(!Board.MyBoard[Board.Middle + i, Board.Middle - moore].isOccupied)//lewo
                {
                    nextMove[0] = Board.Middle + i;
                    nextMove[1] = Board.Middle - moore;
                    return Board.MyBoard[Board.Middle + i, Board.Middle - moore];
                }
            }
            return null;
        
        }
    }
}
