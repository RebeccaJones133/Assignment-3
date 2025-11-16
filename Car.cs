// Author:  Rebecca
// Created at:    October 1, 2025
// Modified at:  November 16, 2025
// Description:
// Defines the Car class, which inherits from Vehicle.
// Cars share all base Vehicle behaviour and identify themselves as Type "Car".
//Sources: Kyle Chapman CarList2025 was used as my insperation on the basics of this code.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarViewer
{
    internal class Car : Vehicle
    {
        /// <summary>
        /// The type label for this vehicle subclass.
        /// </summary>
        public override string Type => "Car";

        /// <summary>
        /// Default constructor. Uses the Vehicle base constructor to assign ID.
        /// </summary>
        public Car() : base()
        {
        }

        /// <summary>
        /// Parameterized constructor — uses properties so validation in Vehicle runs.
        /// </summary>
        public Car(string carMake, string carModel, int carYear, decimal carPrice, bool isCarNew)
            : base()
        {
            Make = carMake;
            // Will throw ArgumentNullException if blank.
            Model = carModel;
            Year = carYear;
            // Will throw ArgumentOutOfRangeException if < 0.
            Price = carPrice;
            IsNew = isCarNew;
        }

    }
}
