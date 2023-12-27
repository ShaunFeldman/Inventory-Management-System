// Author: Shaun Feldman
// File Name: Program.cs
// Project Name: OOP_PASS
// Creation Date: October 17, 2022
// Modified Date: Oct 30, 2022
// Description: The manager of the inventory system which manages the inventory read in from the file

using System;
using System.Collections.Generic; 
using System.IO;
using System.Linq;

namespace OOP_PASS
{
    public class Manager
    {
        //Variable to store the infile and outfile
        static StreamReader inFile;
        static StreamWriter outFile;

        //List of general Items which will contain movie, book, and game objects
        private List<Item> items = new List<Item>();

        //List to store the matches of items when needed
        List<Item> matches = new List<Item>();

        public Manager(string fileName)
        {
            //Calls the load inventory subprogram with the file name given
            LoadInventory(fileName);
        }

        //Pre: the action text for when the action file cant determine what action was given
        //Post: returns the string saying it didn't recognize this action
        //Description: Gets the action text and returns a string saying this action was not recognized
        public string NoAction(string action)
        {
            return "Action Command: " + action + " not recognized\n";
        }

        //Pre: None
        //Post: None
        //Description: Saves the inventory of items back to the inventory file when called
        public void SaveInventory()
        {
            //Trys to open the inventory file, write the file data info for each item, then close it
            try
            {
                //opens the iventory.txt file
                outFile = File.CreateText("inventory.txt");

                //For every index in the item list write the display file data info
                for (int i = 0; i < items.Count; i++)
                {
                    //Writes to the file the items file data info
                    outFile.WriteLine(items[i].DisplayFileData());
                }

                //Closes the file
                outFile.Close();
            }
            catch (IndexOutOfRangeException e)
            {
                //Outputs the index out of range exception error
                Console.WriteLine("Error: " + e.Message);
            }
            catch (FileNotFoundException e)
            {
                //Outputs the file not found exception
                Console.WriteLine("Error: " + e.Message);
            }
            catch (Exception e)
            {
                //Outputs the general error exception
                Console.WriteLine("Error: " + e.Message);
            }
        }

        //Pre: all the information needed for an item: media type, name, price, barcode num, genre, platform, release year, and then the addition info differentiating between movies games and books
        //Post: None
        //Description: Takes the given information and adds an item of the specific type to the list
        public void AddItem(string mediaType, string name, float price, string barcodeNum, string genre, string platform, int releaseYear, string data1, string data2, int data3)
        {
            //Checks if the media type matches and whichever one it does add an item of that type
            switch (mediaType)
            {
                case "Movie":
                    //Adds a new movie to the item list
                    items.Add(new Movie(mediaType, name, price, barcodeNum, genre, platform, releaseYear, data1, data2, data3));
                    break;
                case "Game":
                    //Adds a new game to the item list
                    items.Add(new Game(mediaType, name, price, barcodeNum, genre, platform, releaseYear, data1, Convert.ToInt32(data2)));
                    break;
                case "Book":
                    //Adds a new book to the item list
                    items.Add(new Book(mediaType, name, price, barcodeNum, genre, platform, releaseYear, data1, data2));
                    break;
            }

            //Saves the inventory after an item has been added
            SaveInventory();
        }

        //Pre: the name of the file
        //Post: None
        //Description: Loads the inventory file and adds the info to the list of items
        private void LoadInventory(string fileName)
        {
            //variables to store the line read in from the file and to contain the data that the line will split into
            string line = "";
            string[] data;

            //Holder variable when needed
            int holder = 0;

            //contains the current line number the file is on
            int lineNum = 0;

            //Stores the length the barcode should be at
            int barcodeLength = 12;

            //Trys to open the file and if succesfull go to the next loop which trys to read each file line
            try
            {
                //Opens the file from the name given
                inFile = File.OpenText(fileName);

                //While the loop has not reached the end of the file keep looping
                while (!inFile.EndOfStream)
                {
                    //Second try to catch individual line mistakes
                    try
                    {
                        //Increases the current line number variable
                        lineNum++;

                        //stores the current line from the file in the line variable then splits it at every "," and stores that in the data arrray
                        line = inFile.ReadLine();
                        data = line.Split(',');

                        //if the barcode length doesnt equal 12 or the price is under 0 throw a new exception
                        if (!data[3].Length.Equals(barcodeLength) || float.Parse(data[2]) <= 0)
                        {
                            //throws the exception
                            throw new Exception();
                        }

                        //if the mediatype equals movie add a new movie, if it equals book or game add a new game or book, else throws a new exception
                        if (data[0].Equals("Movie"))
                        {
                            //Adds a new item with the info for a movie
                            AddItem(data[0], data[1], float.Parse(data[2]), data[3], data[4], data[5], Convert.ToInt32(data[6]), data[7], data[8], Convert.ToInt32(data[9]));
                        }
                        else if (data[0].Equals("Book") || data[0].Equals("Game"))
                        {
                            //Adds a new item with info for a movie or book. the last variable is the holder variable as game and book dont have additional data but the Add Item subprogram requires one more piece of info
                            AddItem(data[0], data[1], float.Parse(data[2]), data[3], data[4], data[5], Convert.ToInt32(data[6]), data[7], data[8], holder);
                        }
                        else
                        {
                            //Throws a new exception as the media type is improper
                            throw new Exception();
                        }
                    }
                    catch (Exception)
                    {
                        //format exceptions on individual line
                        Console.WriteLine("ERROR: Line " + lineNum + " was incorrectly formatted: " + line + "\n");
                    }
                }
                //Closes the file
                inFile.Close();
            }
            catch (FileNotFoundException e)
            {
                //Writes the file not found exception
                Console.WriteLine(e.Message + "\n");
            }
            catch (EndOfStreamException e)
            {
                //Writes the end of stream exception
                Console.WriteLine(e.Message + "\n");
            }
            catch (Exception e)
            {
                //writes a general error exception
                Console.WriteLine(e.Message + "\n");
            }
        }

        //Pre: a string of whatever mediatype is needed
        //Post: returns the string of the displayed data of the first two of the type wanted
        //Description: Finds the first two of a type and collects their displayed data to later be outputed
        public string GetFirstTwoOfType(string type)
        {
            //int to store the count
            int count = 0;

            //int to store where a seperation should be
            int seperator = 1;

            //string to store the console text
            string consoleText = "";

            //a integer to store when the loop must be broken
            int breakLoop = 2;

            //loops through each item in the items list and does the appropriate actions
            for (int i = 0; i < items.Count; i++)
            {
                //if the media type is equal to the type given then add the display data info to the text
                if (items[i].GetMediaType().Equals(type))
                {
                    //If the count equals the seperator then add a line to have seperation
                    if (count == seperator)
                    {
                        //Adds a line for seperation
                        consoleText += "\n";
                    }

                    //Adds the display data info to the text string by calling the display data subprogram
                    consoleText += DisplayData(items[i]);

                    //adds 1 to the count
                    count++;
                }

                //If the count is greater than or equal to 2 break from the loop
                if (count >= breakLoop)
                {
                    //breaks from the loop
                    break;
                }

                //if its on the last loop and count is less than 1 then change the string to no items of the type have been found
                if (i == items.Count - seperator && count < seperator)
                {
                    //changes the string to say no items of this type have been found
                    consoleText = "No items of type " + type + " found." + "\n";
                }
            }

            //Returns the string console text which contains the displayed items
            return consoleText;
        }

        //Pre: the barcode of the item wanted to be found
        //Post: returns the item coresponding to the barcode or null if none was found as a type item
        //Description: Loops through every item until a item that matches the barcode is found, returls the item or returns null
        public Item FindBarcode(string barcode)
        {
            //loops through every index in the list and if the barcode entered equals the items barcode return that item
            for (int i = 0; i < items.Count; i++)
            {
                //if the barcode equals the items barcode return that item
                if (barcode.Equals(items[i].GetBarcode()))
                {
                    //returns the item
                    return items[i];
                }
            }

            //returns empty meaning no item was found
            return null;
        }

        //Pre: the name of the item you're trying to find
        //Post: returns a list of matches as a list
        //Description: loops through every item and every item that matches the name given is added to the list, returns that list
        public List<Item> FindName(string name)
        {
            //clears the matches list so it can hold new stuff
            matches.Clear();

            //for every item in the items list check if the name equals the name given and if so add it to the matches list
            for (int i = 0; i < items.Count; i++)
            {
                //if the name equals the items name add the item to the list
                if (name.Equals(items[i].GetName()))
                {
                    //Adds the item to the matches list of items
                    matches.Add(items[i]);
                }
            }

            //returns the matches list
            return matches;
        }

        //Pre: None
        //Post: None
        //Description: Sorts the items list by cost, first sorts by the media type and then by the price in descending order. saves the inventory after this
        public void SortByCost()
        {
            //Sorts the items list by mediatype then price in descending order
            items = items.OrderBy(item => item.GetMediaType())
              .ThenByDescending(item => item.GetPrice())
              .ToList();

            //Calls the save inventory subprogram
            SaveInventory();
        }

        //Pre: None
        //Post: None
        //Description: Sorts the items list by names, first orders by media types and then by items names, saves the inventory after this
        public void SortByName()
        {
            //Sorts the items list by mediatype then by name
            items = items.OrderBy(item => item.GetMediaType())
               .ThenBy(item => item.GetName())
               .ToList();

            //Calls the save inventory subprogram
            SaveInventory();
        }

        //Pre: The item which price needs to be changed and the price it needs to be changed to
        //Post: None
        //Description: Sets the price of the item to a new price
        public void SetPrice(Item item, float price)
        {
            //Calls the set price subprogram for the specified item and gives it the new price
            item.SetPrice(price);

            //Calls the save inventory subprogram
            SaveInventory();
        }

        //Pre: The item which needs to be deleted
        //Post: None
        //Description:
        public void DeleteItem(Item item)
        {
            //Gets the barcode of the item, then calls find barcode to return the item from the list that needs to be deleted and removes this item from the list
            items.Remove(FindBarcode(item.GetBarcode()));

            //Calls the save inventory subprogram
            SaveInventory();
        }

        //Pre: The item which data needs to be displayed
        //Post: returns the item in displayed data format in a string
        //Description: gets that items displayed data info and returns it
        public string DisplayData(Item item)
        {
            //Returns the items display data info and an extra line for spacing
            return item.DisplayData() + "\n";
        }
    }
}