using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BricksPlayer
{
    class Program
    {
        static void Main(string[] args)
        {
           
            Communication Sowa = new Communication();
            Movement myMovement = new Movement(false);
           

            string ruch = @"^([0-9]+\s[0-9]+\s[0-9]+\s[0-9]+)$";
            string sizePattern = @"^[0-9]+$";

            int[] move;

            while (true)
            {
                String input = Console.ReadLine();
                if (input.ToLower().Equals("ping"))
                {
                    Sowa.Ping();
                             
                }

                else if (input.ToLower().Equals("zaczynaj"))
                {
                    myMovement = Sowa.First();//zaczynamy

                    move = myMovement.MakeMove(); // nasz ruch

                    Sowa.saveNewMove(move);//zapisuje nasz ruch

                    Sowa.makeMove(move);//wykonuje nasz ruch
                }

                else if(Regex.Matches(input,ruch).Count > 0) //ruch przeciwnika
                {
                    Sowa.saveNewMove(Sowa.ToInt(input));//zapisuje ruch przeciwnika

                    move = myMovement.MakeMove();// nasz ruch

                    Sowa.saveNewMove(move);//zapisuje nasz ruch

                    Sowa.makeMove(move);//wykonuje nasz ruch  
                                                         
                }

                else if (Regex.Matches(input, sizePattern).Count > 0) //wielkość planszy
                {
                    try
                    {
                        Board.newBoard(Int32.Parse(input));
                    }
                    catch (ArgumentNullException)
                    {
                        Console.WriteLine("BUUUUUU");
                    }                   
                }

                else if(input.ToLower().Equals("wygrales") || input.ToLower().Equals("przegrales"))
                {
                    break;
                }

                else //błądzik
                {
                    Console.WriteLine("BUUUUUUUUUUUUUU");
                }          
            }
        }
    }
}
