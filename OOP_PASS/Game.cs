// Author: Shaun Feldman
// File Name: Program.cs
// Project Name: OOP_PASS
// Creation Date: October 17, 2022
// Modified Date: Oct 30, 2022
// Description: A specific object of type game which is in relation to type item

using System;
using System.Collections.Generic;

namespace OOP_PASS
{
    public class Game : Item
    {
        //Stores the age rating and metric score in the appropriate variables
        private string ageRating;
        private int metricScore;

        public Game(string mediaType, string name, float price, string barcodeNum, string genre, string platform, int releaseYear, string ageRating, int metricScore) : base(mediaType, name, price, barcodeNum, genre, platform, releaseYear)
        {
            //sets the age rating and metric score variables to equal the data given for the age rating and metric score
            this.ageRating = ageRating;
            this.metricScore = metricScore;
        }

        //Pre: None
        //Post: returns the string of the items data displayed
        //Description: returns a string of the items data displayed
        public override string DisplayData()
        {
            //returns a string of the items data displayed, calls the base display data to get the other items displayed data
            return base.DisplayData() + "\nRating: " + ageRating + "\nScore: " + metricScore;
        }

        //Pre: None
        //Post: returns the string of the items data displayed in file format
        //Description: returns a string of the items data displayed in file format
        public override string DisplayFileData()
        {
            //returns a string of the items data displayed, calls the base display file data to get the other items displayed data all in file format
            return base.DisplayFileData() + ageRating + "," + metricScore;
        }
    }
}
