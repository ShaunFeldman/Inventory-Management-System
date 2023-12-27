// Author: Shaun Feldman
// File Name: Program.cs
// Project Name: OOP_PASS
// Creation Date: October 17, 2022
// Modified Date: Oct 30, 2022
// Description: A specific object of type movie which is in relation to type item

using System;
using System.Collections.Generic;

namespace OOP_PASS
{
    public class Movie : Item
    {
        //Stores the dircetor, age rating, and duration in their appropriate variables
        private string director;
        private string ageRating;
        private int duration;

        public Movie(string mediaType, string name, float price, string barcodeNum, string genre, string platform, int releaseYear, string director, string ageRating, int duration) : base (mediaType, name, price, barcodeNum, genre, platform, releaseYear)
        {
            //sets the director, age rating, and duration to the given director, age rating, and duration variable
            this.director = director;
            this.ageRating = ageRating;
            this.duration = duration;
        }

        //Pre: None
        //Post: returns the string of the items data displayed
        //Description: returns a string of the items data displayed
        public override string DisplayData()
        {
            //returns a string of the items data displayed, calls the base display data to get the other items displayed data
            return base.DisplayData() + "\nDirector: " + director + "\nMPAA Rating: " + ageRating + "\nDuration: " + duration;
        }

        //Pre: None
        //Post: returns the string of the items data displayed in file format
        //Description: returns a string of the items data displayed in file format
        public override string DisplayFileData()
        {
            //returns a string of the items data displayed, calls the base display file data to get the other items displayed data all in file format
            return base.DisplayFileData() + director + "," + ageRating + "," + duration;
        }
    }
}
