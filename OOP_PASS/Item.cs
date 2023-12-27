// Author: Shaun Feldman
// File Name: Program.cs
// Project Name: OOP_PASS
// Creation Date: October 17, 2022
// Modified Date: Oct 30, 2022
// Description: An object of type item for each thing you can add to the management system

using System;
using System.Collections.Generic;

namespace OOP_PASS
{
    public class Item
    {
        //Stores the items data information: the media type, name, price, barcode number, genre, platform, and release year
        protected string mediaType;
        protected string name;
        protected float price;
        protected string barcodeNum;
        protected string genre;
        protected string platform;
        protected int releaseYear;

        public Item(string mediaType, string name, float price, string barcodeNum, string genre, string platform, int releaseYear)
        {
            //sets the variables media type, name, price, barcode num, genre, platfom, and release year to equal the given variables info
            this.mediaType = mediaType;
            this.name = name;
            this.price = price;
            this.barcodeNum = barcodeNum;
            this.genre = genre;
            this.platform = platform;
            this.releaseYear = releaseYear;
        }

        //Pre: None
        //Post: returns the media type, name, price, barcode num, genre, platform, and release year in file format as a string
        //Description: returns the media type, name, price, barcode num, genre, platform, and release year in file format
        public virtual string DisplayFileData()
        {
            //returns the media type, name, price, barcode num, genre, platform, and release year in file format
            return mediaType + "," + name + "," + price + "," + barcodeNum + "," + genre + "," + platform + "," + releaseYear + ",";
        }

        //Pre: None
        //Post: returns the media type, name, price, barcode num, genre, platform, and release year in file format as a string
        //Description: returns the media type, name, price, barcode num, genre, platform, and release year in file format
        public virtual string DisplayData()
        {
            //returns the media type, name, price, barcode num, genre, platform, and release year in file format
            return "Type: " + mediaType + "\nName: " + name + "\nCost: " + price + "\nBarcode: " + barcodeNum
                + "\nGenre: " + genre + "\nPlatform: " + platform + "\nRelease Year: " + releaseYear;
        }

        //Pre: None
        //Post: returns the media type as string
        //Description: returns the media type
        public string GetMediaType()
        {
            //returns the media type
            return mediaType;
        }

        //Pre: None
        //Post: returns the name as string
        //Description: Returns the name
        public string GetName()
        {
            //returns the name
            return name;
        }

        //Pre: None
        //Post: returns the barcode as string
        //Description: returns the barcode
        public string GetBarcode()
        {
            //returns the barcode
            return barcodeNum;
        }

        //Pre: None
        //Post: returns the genre as string
        //Description: returns the genre
        public string GetGenre()
        {
            //returns the genre
            return genre;
        }

        //Pre: None
        //Post: returns the platform as string
        //Description: returns the platfrom
        public string GetPlatform()
        {
            //returns the platform
            return platform;
        }

        //Pre: None
        //Post: returns the release year as int
        //Description: returns the release year
        public int GetReleaseYear()
        {
            //returns the release year
            return releaseYear;
        }

        //Pre: None
        //Post: returns the price as float
        //Description: returns the price
        public float GetPrice()
        {
            //returns the price
            return price;
        }

        //Pre: the price variable
        //Post: sets the price variable to the new price
        //Description: sets the price to the new price
        public void SetPrice(float price)
        {
            //sets the price variable to the new price
            this.price = price;
        }
    }
}
