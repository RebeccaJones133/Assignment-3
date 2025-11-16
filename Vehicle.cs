// Author:  Kyle Chapman
// Created: October 16, 2025
// Updated: October 16, 2025
// Description: Defines a generic vehicle to be inherited by diferent vehicle types for display.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarList2025
{
    internal abstract class Vehicle
    {
        protected static int count = 0;

        // Declared and initialized Variables
        protected string make = String.Empty;
        protected string model = String.Empty;
        protected int year = DateTime.Now.Year;
        protected decimal price = 0.0M;
        protected bool isNew = false;
        protected int id = count;

        /// <summary>
        /// A default constructor
        /// </summary>
        public Vehicle()
        {
            count++;
            id = count;
        }

        /// <summary>
        /// An accessor that returns the make of the car
        /// </summary>
        /// <returns></returns>
        public string Make { get { return make; } set { make = value; } }

        /// <summary>
        /// An accessor that returns the model of the car
        /// </summary>
        /// <returns></returns>
        public string Model { get { return model; } set { model = value; } }

        /// <summary>
        /// An accessor that returns the year of the car
        /// </summary>
        /// <returns></returns>
        public int Year { get { return year; } set { year = value; } }

        /// <summary>
        /// An accessor that returns the colour of the car
        /// </summary>
        /// <returns></returns>
        public decimal Price { get { return price; } set { price = value; } }

        /// <summary>
        /// An accessor that returns the if the car is new or old
        /// </summary>
        /// <returns></returns>
        public bool IsNew { get { return isNew; } set { isNew = value; } }

        /// <summary>
        /// An accessor that returns car's unique identifier.
        /// </summary>
        /// <returns></returns>
        public int IdentificationNumber { get { return id; } }

        /// <summary>
        /// Returns the number of vehicles created.
        /// </summary>
        /// <returns></returns>
        public static int Count { get { return count; } }

        /// <summary>
        /// Returns a string version of the vehicle.
        /// </summary>
        /// <returns>A string version of the vehicle.</returns>
        public override string ToString()
        {
            return Year + " " + Make + " " + Model;
        }

    }
}
