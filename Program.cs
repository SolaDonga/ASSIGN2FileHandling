using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileAssign2
{
    /*========================================/
    Programmed by Sola Donga                  
    Assignment 2                              
    File Handling                             
    Semester2                                 
    /*=======================================*/
    class Program
    {
        
        static int[] count = new int[5];
        static int[] nonIrish = new int[5];
        static int[] irish = new int[5];
        static int[] score = new int[2];
       
        static void Main(string[] args)
        {
            string menuChoice = PrintMenu();

            while (menuChoice != "D")  // in the case D is the exit option
            {
                // process choice

                switch (menuChoice)
                {
                    case "A":
                        Console.Clear();
                        Console.WriteLine();
                        Console.WriteLine("1.Player Report ");
                        Console.WriteLine("================");
                        // call appropriate method(s)
                        Console.WriteLine();
                        PlayerReportPrint();
                        Console.WriteLine();
                        Console.WriteLine("Press enter to call up menu choice");
                        Console.ReadLine();
                       
                       break;

                    case "B":
                        Console.Clear();
                        Console.WriteLine();
                        Console.WriteLine("2.Score Analysis Report ");
                        Console.WriteLine("=======================");
                        // call appropriate method(s)
                        Console.WriteLine();
                        ScoreAnalysisReport();
                        Console.WriteLine();
                        Console.WriteLine("Press enter to call up menu choice");
                        Console.ReadLine();
                        break;
                    case "C":
                        Console.Clear();
                        Console.WriteLine();
                        Console.WriteLine("3.Search for a Player");
                        Console.WriteLine("=====================");
                        // call appropriate method(s)
                        Console.WriteLine();
                        SearchPlayer();
                        Console.WriteLine();
                        Console.WriteLine("Press enter to call up menu choice");
                        Console.ReadLine();
                        break;
                    default:  // must be something other than A,B,C
                        Console.Clear();
                        Console.WriteLine();
                        Console.WriteLine("Invalid menu choice");
                        Console.WriteLine();
                        Console.WriteLine("Press enter to call up menu choice");
                        Console.ReadLine();
                        break;


                }//end switch

                //call menu again

                menuChoice = PrintMenu();

            } // end while



        }// end main

        static string PrintMenu()
        {
            Console.WriteLine("game menu");
            Console.WriteLine("==========");
            Console.WriteLine("A	Player Report");
            Console.WriteLine("B	Score Analysis Report");
            Console.WriteLine("C	Search for a Player");
            Console.WriteLine("D	Exit");
            Console.WriteLine("\nEnter Menu choice : ");
            Console.WriteLine();
            string result = Console.ReadLine().ToUpper();

            return result;
        }
        static void PlayerReportPrint()
        {
            string lineIn, starRating;
            string[] fields = new string[3];
            string[] names = new string[3];
            int[] score = new int[3];
            int count = 0;
            string topPlayer;
            string tableFormat = "{0,-15}{1,-15}{2,-15}";
            StreamReader inputStream = new StreamReader(@"H:\C#\FileAssign2\scores.txt");
            // print heading

            Console.WriteLine(tableFormat, "Player Name", "Scores", "Star Rating");
            Console.WriteLine("=======================================================");
            lineIn = inputStream.ReadLine();

            while (lineIn != null)
            {

                // must split and process each line

                fields = lineIn.Split(',');
                names[count] = fields[1];
                score[count] = Convert.ToInt32(fields[3]);
               
                //fields[1] = nameSplit(fields[1]);
                
                starRating = ratingAdd(Convert.ToInt32(fields[3]));
               
                //This line below will write out each line containing data in the correct formats
                Console.WriteLine(tableFormat, fields[1], Convert.ToInt32(fields[3]), starRating);


                lineIn = inputStream.ReadLine();
                count++;
            }
            topPlayer = scoreSort(score, names);
            Console.WriteLine("=================================================================");
            Console.WriteLine("\nPop Standard Deviation: {0:f2} \nTop Player: {1} ", standDev(score), topPlayer);
            inputStream.Close();
            return;




        }
        static string nameSplit(string nameIn)
        {
            //This splits the employee name so that the first name can be abbreviated
            string[] name = new string[1];

            name = nameIn.Split(' ');
            name[0] = name[0].Substring(0, 1).ToUpper() + ".";
            name[1] = name[1].Substring(0, 1).ToUpper() + ".";
            return String.Join(" ", name);

        }
        static string ratingAdd(int ratingIn)
        {
            //This adds the correct star rating for scores 
            string rating="";

            if (ratingIn < 400)
            {
                rating = "*";
            }
            else if (ratingIn >= 400 && ratingIn <= 599)
            {
                rating = "**";
            }
            else if (ratingIn >= 600 && ratingIn <= 699)
            {
                rating = "***";
            }
            else if (ratingIn >= 700 && ratingIn <= 999)
            {
                rating = "****";
            }
            else
                rating = "*****";

                return rating;
           
        }
        static string scoreSort(int[] score, string[] names)
        {

            //This Sorts the Names correctly based on the values of the Scores tab
            Array.Sort(score, names);
            Array.Reverse(names);
            string[] highestScorerName = new string[1];
            highestScorerName = names[0].Split(' ');
            // highestScorerName = names[names.Length-1].Split(' ');
            highestScorerName[0] = highestScorerName[0].Substring(0, 1).ToUpper() + ".";
            highestScorerName[1] = highestScorerName[1].Substring(0, 1).ToUpper() + ".";
            return String.Join(" ", highestScorerName);

          
        }
        static double standDev(int[] scoreIn)
        {
            //This gathers the standard deviation for Scores.
            double average = scoreIn.Average();
            double sumOfVariance = 0;
            foreach (double value in scoreIn)
            {

                average += (scoreIn.Length - 3) / 2;
                double difference = value - average;
                double variance = difference * difference;

                sumOfVariance += variance;
            }
            double varianceAverage = sumOfVariance / (scoreIn.Length);
            Console.WriteLine("Average Score is: {0}", average);
            return (Math.Sqrt(varianceAverage));
        }

        /// <summary>
        /// This will display the summary report analysis from the file as per player score range and nationality
        /// </summary>

        static void ScoreAnalysisReport()
        {
            string lineIn;
            string[] fields = new string[3];
            string[] tableTitle = { "Under 400", "400-599", "600-699", "700-999", "1000+" };

            string tableFormat = "{0,-15}{1,-10}{2,-15}{3,-15}";//format table will align my heading

            // open connection

            StreamReader inputStream = new StreamReader(@"H:\C#\FileAssign2\scores.txt");
           
            // This is were I print my heading

            Console.WriteLine(tableFormat, "Score Range", "Count", "Non-Irish", "Irish");
            Console.WriteLine("====================================================");
            lineIn = inputStream.ReadLine();
            do
            {
                fields = lineIn.Split(',');
                countCheck(Convert.ToInt32(fields[3]), fields[2]);

                lineIn = inputStream.ReadLine();

            } while (lineIn != null);
            inputStream.Close();
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine(tableFormat, tableTitle[i], count[i], nonIrish[i], irish[i]);
            }


            //This will print combined total of fields above

            Console.WriteLine("==============================================================");
            Console.WriteLine();
            Console.WriteLine(tableFormat, "Total", count.Sum(), nonIrish.Sum(), irish.Sum());
            Console.WriteLine();
            return;

        }
        static void countCheck(int checkIn, string nationalityIn)
        {
            // This is where I will count something,
            int check = 0;
            for (int i = 100; i <= checkIn; i += 100)
            {
                check++;
            }
            switch (check)//checks nationality with sales made
            {
                case 1:
                case 2:
                case 3:
                    count[0] += 1;
                    if (nationalityIn.ToLower() == "irish")
                    {
                        irish[0] += 1;
                    }
                    else
                    {
                        nonIrish[0] += 1;
                    }
                    break;
                case 4:
                case 5:
                    count[1] += 1;
                    if (nationalityIn.ToLower() == "irish")
                    {
                        irish[1] += 1;
                    }
                    else
                    {
                        nonIrish[1] += 1;
                    }
                    break;
                case 6:
                    count[2] += 1;
                    if (nationalityIn.ToLower() == "irish")
                    {
                        irish[2] += 1;
                    }
                    else
                    {
                        nonIrish[2] += 1;
                    }
                    break;
                case 7:
                case 8:
                case 9:
                    count[3] += 1;
                    if (nationalityIn.ToLower() == "irish")
                    {
                        irish[3] += 1;
                    }
                    else
                    {
                        nonIrish[3] += 1;
                    }
                    break;
                default:
                    count[4] += 1;
                    if (nationalityIn.ToLower() == "irish")
                    {
                        irish[4] += 1;
                    }
                    else
                    {
                        nonIrish[4] += 1;
                    }
                    break;
            }

        }
        static string fileSearch(string searchTerm)
        {
            string lineIn;
            string[] fields = new string[3];
            // open connection
            StreamReader inputStream = new StreamReader(@"H:\C#\FileAssign2\scores.txt");
           
            // print my heading

            lineIn = inputStream.ReadLine();

            searchTerm = searchTerm.ToLower();
            do
            {
                fields = lineIn.Split(',');
                if (fields[0].ToLower().Contains(searchTerm))//checks search term and returns fields to be split
                {
                    return lineIn;
                }
                lineIn = inputStream.ReadLine();
            } while (lineIn != null);
            inputStream.Close();
            return null;//If nothing is found here the method will just return a nullable value.
        }

        /// This is where the players numbers and names are going to be displayed if found.

        static void SearchPlayer()
        {
            string searchTerm, employee;
            string[] fields = new string[3];
            Console.Write("Enter Player Number\t:");
           
            searchTerm = (Console.ReadLine().ToUpper());


            if (searchTerm != "-999")//sentinel value is a special value that is used to terminate a program most specially a loop. 
            {
                employee = fileSearch(searchTerm);//searches file
                //Display Name if the search is valid 
                if (employee != null)
                {
                    fields = employee.Split(',');
                    Console.WriteLine("Employee name         \t:" + fields[1]);
                    Console.WriteLine("Scores           \t:" + fields[3]);
                    Console.WriteLine("======================================");
                }
                else
                {
                    Console.WriteLine("No match found!");
                    Console.Write("Enter Player Number(-999 to exit)   :\t\t".ToUpper());//If no match found at first
                    searchTerm = Console.ReadLine();
                }

            }
                else
                {
                return;
                }
        }

    }
  }
    


