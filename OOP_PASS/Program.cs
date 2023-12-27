// Author: Shaun Feldman
// File Name: Program.cs
// Project Name: OOP_PASS
// Creation Date: October 17, 2022
// Modified Date: Oct 30, 2022
// Description: The driver class for a simple inventory management system, outputs text to a file and to the console

using System;
using System.Collections.Generic;
using System.IO;

namespace OOP_PASS
{
    class Program
    {
        //A list for the output needed to go into the file
        private static List<string> fileOutput = new List<string>();

        //Variables to be able to read in the file and write out to the file
        static StreamReader inFile;
        static StreamWriter outFile;

        //Creates a new manager instance with the inventory.txt file being the one read in
        static Manager manager = new Manager("inventory.txt");

        public static void Main(string[] args)
        {
            //Calls the read file subprogram to read in the file, then the write file to write into a file
            ReadFile();
            WriteFile();

            //Code ends with read line so output stays on screen
            Console.ReadLine();
        }

        ////Pre: None
        //Post: None
        //Description: Reads in the action.txt file and calls the appropriate subprograms depending on what the file line says
        private static void ReadFile()
        {
            //variable to hold the current line in the file, the action, and the data given in it
            string line = "";
            string[] action;
            string[] data;

            //a holder variable in both int and string format and an item type holder to collect the returned item
            int holder = 0;
            string temp;
            Item temporary;

            //stores the length a barcode must be
            int barcodeLength = 12;

            //A list to store the items which made matches when needed
            List<Item> matches = new List<Item>();

            //trys to open the action.txt file and then preform the try inside which reads each line of the file, catches fill file errors
            try
            {
                //opens the actions.txt file
                inFile = File.OpenText("actions.txt");

                //while the file has not reached the end keep looping
                while (!inFile.EndOfStream)
                {
                    //trys to read the line of the file and then call the appropriate subprograms, catches individual file line mistakes
                    try
                    {
                        //stores the current line of the file in the line variable and then splits the data by ":" symbol and stores in the action
                        //array for further use
                        line = inFile.ReadLine();
                        action = line.Split(':');

                        //preform the appropriate actions based on whatever value action[0] has
                        switch (action[0])
                        {
                            case "Add":
                                //splits the right side of the data which is stored in action[1] into ana array of data
                                data = action[1].Split(',');

                                //if the barcode is not at the length is should be, or price is under or equal to 0 then throw the exception
                                if (!data[3].Length.Equals(barcodeLength) || float.Parse(data[2]) <= 0)
                                {
                                    //throws the exception with info that barcode or price is improper
                                    throw new Exception("Data type barcode or price is improper");
                                }

                                //if the first piece of data equals move add a movie, if it equals book or game add a book or game, else throw the exeption the media type is wrong
                                if (data[0].Equals("Movie"))
                                {
                                    //adds a new movie to the manager
                                    manager.AddItem(data[0], data[1], float.Parse(data[2]), data[3], data[4], data[5], Convert.ToInt32(data[6]), data[7], data[8], Convert.ToInt32(data[9]));
                                }
                                else if (data[0].Equals("Book") || data[0].Equals("Game"))
                                {
                                    //adds a new move or book to the manager, the polymorphism will choose
                                    manager.AddItem(data[0], data[1], float.Parse(data[2]), data[3], data[4], data[5], Convert.ToInt32(data[6]), data[7], data[8], holder);
                                }
                                else
                                {
                                    //throws the exeption the data type is not correct
                                    throw new Exception("Data type media type is improper");
                                }
                                break;
                            case "Display2":
                                //stores the string retrieved in a temporary variable
                                temp = manager.GetFirstTwoOfType(action[1]);

                                //adds to the file output the string and writes to the console the string
                                fileOutput.Add(temp);
                                Console.WriteLine(temp);
                                break;
                            case "FindBarcode":
                                //if the item coming back is not null then preform the appropriate action depending on what action[2] says, else outputs it wasnt found
                                if (manager.FindBarcode(action[1]) != null)
                                {
                                    //stores the item returned in the temporary variable
                                    temporary = manager.FindBarcode(action[1]);

                                    //if action[2] says display, display the items data, if it says modify then modify the price to the given price,
                                    //if it says delete then delete the item, else throw the exception that the action is improper
                                    if (action[2].Equals("Display"))
                                    {
                                        //adds the data to the file output and the displays it as well to the console
                                        fileOutput.Add(manager.DisplayData(temporary));
                                        Console.WriteLine(manager.DisplayData(temporary));
                                    }
                                    else if (action[2].Equals("Modify"))
                                    {
                                        //if the price wanted is greater than 0 perform the action, else throw the exception that the price is improper
                                        if (float.Parse(action[3]) > 0)
                                        {
                                            //sets the price of the item to the new price wanted
                                            manager.SetPrice(temporary, float.Parse(action[3]));
                                        }
                                        else
                                        {
                                            //throws the exception that the price is improper
                                            throw new Exception("Data type price is improper");
                                        }
                                    }
                                    else if (action[2].Equals("Delete"))
                                    {
                                        //deletes the item wanted
                                        manager.DeleteItem(temporary);
                                    }
                                    else
                                    {
                                        //throws the exception that the action type is improper
                                        throw new Exception("action type is improper");
                                    }
                                }
                                else
                                {
                                    //adds the output to the fileOutput stating the barcode was not found as well as displays it to the console
                                    fileOutput.Add("Item: " + action[1] + " not found\n");
                                    Console.WriteLine("Item: " + action[1] + " not found\n");
                                }
                                break;
                            case "FindName":
                                //clears the list of matches already made
                                matches.Clear();
                                //adds to the list the list returned from calling the FindName subprogram with the name given from the file
                                matches.AddRange(manager.FindName(action[1]));

                                //if the number of matches equal 0 output the item was not found, else perform the appropriate action
                                if (matches.Count == 0)
                                {
                                    //adds the output to the fileOutput list and outputs to the screen as well that the file was not found
                                    fileOutput.Add("Item: " + action[1] + " not found\n");
                                    Console.WriteLine("Item: " + action[1] + " not found\n");
                                }
                                else
                                {
                                    //if the data says display then display the specified item of all the items with the same name, else if modify the item, else if delete the item
                                    //Else throw an exception that the media type is improper
                                    if (action[2].Equals("Display"))
                                    {
                                        //adds the display data info for the specific item to the file output list and writes the display data to the console
                                        fileOutput.Add(manager.DisplayData(matches[Convert.ToInt32(action[3])]));
                                        Console.WriteLine(manager.DisplayData(matches[Convert.ToInt32(action[3])]));
                                    }
                                    else if (action[2].Equals("Modify"))
                                    {
                                        //if the price given is greater then 0 then modify the price to the new price, else throw an exception that the price is improper
                                        if (float.Parse(action[4]) > 0)
                                        {
                                            //calls the setprice subprogram to modify the price to a new one
                                            manager.SetPrice(matches[Convert.ToInt32(action[3])], float.Parse(action[4]));
                                        }
                                        else
                                        {
                                            //throws the exception that the price is improper
                                            throw new Exception("Data type price is improper");
                                        }
                                    }
                                    else if (action[2].Equals("Delete"))
                                    {
                                        //deletes the item wanted from the list of matches
                                        manager.DeleteItem(matches[Convert.ToInt32(action[3])]);
                                    }
                                    else
                                    {
                                        //throws the exception that the media type is wrong
                                        throw new Exception("Data type media type is improper");
                                    }
                                }
                                break;
                            case "SortByCost":
                                //calls the sort by cost subprogram
                                manager.SortByCost();
                                break;
                            case "SortByName":
                                //calls the sort by name subprogram
                                manager.SortByName();
                                break;
                            default:
                                //if all above doesnt happen then output the no action subprogram to the console and add it to the file output list
                                Console.WriteLine(manager.NoAction(line));
                                fileOutput.Add(manager.NoAction(line));
                                break;
                        }
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        //outputs the file like and the message for this exception
                        Console.WriteLine(line + " " + e.Message + "\n");
                    }
                    catch (Exception e)
                    {
                        //outputs the exception for the individual line
                        Console.WriteLine(line + " " + e.Message + "\n");
                    }
                }
                //Closes the opened file
                inFile.Close();
            }
            catch (FileNotFoundException e)
            {
                //exception if the file was not found, outputs the message
                Console.WriteLine(e.Message + "\n");
            }
            catch (EndOfStreamException e)
            {
                //exception if the file reached the end, outputs the message
                Console.WriteLine(e.Message + "\n");
            }
            catch (Exception e)
            {
                //General exception output to catch any issue with the file
                Console.WriteLine(e.Message + "\n");
            }
        }

        //Pre: None
        //Post: None
        //Description: creates and writes to the file the output of what the actions do
        private static void WriteFile()
        {
            //trys to creat the file, add all the info to it, then close it
            try
            {
                //Creates the file FeldmanS_PASS2.txt
                outFile = File.CreateText("FeldmanS_PASS2.txt");

                //loops the index from 0 to one less than the file count and outputs the info from the list to the file
                for (int i = 0; i < fileOutput.Count; i++)
                {
                    //Writes the info from the current list index to the file
                    outFile.WriteLine(fileOutput[i]);
                }

                //Closes the file
                outFile.Close();
            }
            catch (IndexOutOfRangeException e)
            {
                //Writes the index out of range exception message
                Console.WriteLine("Error: " + e.Message);
            }
            catch (Exception e)
            {
                //Writes the general exception
                Console.WriteLine("Error: " + e.Message);
            }
        }
    }
}
