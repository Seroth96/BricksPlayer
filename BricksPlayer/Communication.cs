using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BricksPlayer
{
    class Communication
    {
        public String Line { get; set; }

        public void Ping()
        {
            Console.WriteLine("PONG");
        }

        public Movement First()
        {
            return new Movement(isFirst: true);
        }

        public int[] ToInt(String input)
        {
            string[] temp = input.Split(' ');
            int[] moves = new int[] {
                Int32.Parse(temp[1]) - 1,
                Int32.Parse(temp[0]) - 1,
                Int32.Parse(temp[3]) - 1,
                Int32.Parse(temp[2]) - 1 };

            return moves;
        }

        public void saveNewMove(int[] moves)
        {
            //string[] temp = input.Split(' ');
            //int[] moves = new int[] {
            //    Int32.Parse(temp[1]) - 1,
            //    Int32.Parse(temp[0]) - 1,
            //    Int32.Parse(temp[3]) - 1,
            //    Int32.Parse(temp[2]) - 1 };

             string temp = moves[0] +" "+ moves[1] + " " + moves[2] + " " + moves[3];


            Board.saveNewMove(moves);
        }

        public void makeMove(int[] moves)
        {

            Console.WriteLine(moves[1] + 1 + " " + (moves[0]+1) + " " + (moves[3]+1) + " " + (moves[2]+1));
        }

   


    }
}
