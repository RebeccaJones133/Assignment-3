// Author:  Brendan Obilo & Kyle Chapman
// Created at:    October 18, 2024
// Modified at:  October 16, 2025
// Description:
// A class that deals with the input from user. The input includes, Car’s make (or manufacturer),
// model, year, colour, price, and whether it is new or not (as a Boolean).
// It Uses this parameters and adds to the car list that displays on the textBlock.
// Created by inheriting a vehicle class.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarList2025
{
    internal class Car : Vehicle
    {
        /// <summary>
        /// A default constructor
        /// </summary>
        public Car() : base() { }

        /// <summary>
        /// A parameterized constructor that accepts various paramterized
        /// And sets them to the private variables
        /// </summary>
        /// <param name="carMake"> The make of the car entered by user</param>
        /// <param name="carModel"> The model of the car entered by user</param>
        /// <param name="carYear"> The Year the car was made as entered by user</param>
        /// <param name="carPrice"> The price of the car as entered by the user</param>
        /// <param name="isCarNew"> A parameter than determines if the car is new or not</param>
        public Car(string carMake, string carModel, int carYear, decimal carPrice, bool isCarNew) : base()
        {
            make = carMake;
            model = carModel;
            year = carYear;
            price = carPrice;
            isNew = isCarNew;
        }

        /// <summary>
        /// Returns a string version of the Car.
        /// </summary>
        /// <returns>A string version of the Car.</returns>
        public override string ToString()
        {
            return Year + " " + Make + " " + Model + " (Car)";
        }

    }
}
