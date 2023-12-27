// Author: Shaun Feldman
// File Name: Program.cs
// Project Name: OOP_PASS
// Creation Date: October 17, 2022
// Modified Date: Oct 30, 2022
// Description: A specific object of type book which is in relation to type item

using System;
using System.Collections.Generic;

namespace OOP_PASS
{
    public class Book : Item
    {
        //Variables to store the author and publisher information
        private string author;
        private string publisher;

        public Book(string mediaType, string name, float price, string barcodeNum, string genre, string platform, int releaseYear, string author, string publisher) : base(mediaType, name, price, barcodeNum, genre, platform, releaseYear)
        {
            //sets the author and publisher variables to equal the given info for them
            this.author = author;
            this.publisher = publisher;
        }

        //Pre: None
        //Post: returns the string of the items data displayed
        //Description: returns a string of the items data displayed
        public override string DisplayData()
        {
            //returns a string of the items data displayed, calls the base display data to get the other items displayed data
            return base.DisplayData() + "\nAuthor: " + author + "\nPublisher: " + publisher;
        }

        //Pre: None
        //Post: returns the string of the items data displayed in file format
        //Description: returns a string of the items data displayed in file format
        public override string DisplayFileData()
        {
            //returns a string of the items data displayed, calls the base display file data to get the other items displayed data all in file format
            return base.DisplayFileData() + author + "," + publisher;
        }
    }
}