// Author:  Rebecca Jones
// Created: November 15 2025
// Updated: November 16 2025
// Description:
// Abstract base class for all vehicles displayed in the CarViewer application.
// Tracks shared state like make, model, year, price, condition, and a unique ID
// for each vehicle instance.
//Sources: Kyle Chapman CarList2025 was used as my insperation on the basics of this code.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarViewer
{
    internal abstract class Vehicle
    {
        // Static counter for all vehicles, used to assign an ID.
        protected static int count = 0;

        // Core fields shared by all vehicles.
        protected string make = string.Empty;
        protected string model = string.Empty;
        protected int year = DateTime.Now.Year;
        protected decimal price = 0.0m;
        protected bool isNew = false;
        protected int id;

        /// <summary>
        /// Default constructor. Increments the shared counter and assigns an ID.
        /// </summary>
        protected Vehicle()
        {
            count++;
            id = count;
        }

        /// <summary>
        /// The vehicle's manufacturer.
        /// </summary>
        public string Make
        {
            get => make;
            set => make = value ?? string.Empty;
        }

        /// <summary>
        /// The vehicle's model. Cannot be null or blank.
        /// </summary>
        public string Model
        {
            get => model;
            set
            {
                // Just in case,don’t allow empty models.
                if (string.IsNullOrWhiteSpace(value))
                {
                    // a error message if the model is blank
                    throw new ArgumentNullException(nameof(value), "Model cannot be blank.");
                }

                model = value;
            }
        }

        /// <summary>
        /// The model year of the vehicle.
        /// </summary>
        public int Year
        {
            get => year;
            set => year = value;
        }

        /// <summary>
        /// The price of the vehicle. Must be zero or greater.
        /// </summary>
        public decimal Price
        {
            get => price;
            set
            {
                // checks if the price in below zero
                if (value < 0)
                {
                    // error message for negative prices
                    throw new ArgumentOutOfRangeException(nameof(value), "Price cannot be negative.");
                }

                price = value;
            }
        }

        /// <summary>
        /// True if the vehicle is new, false if pre-owned.
        /// </summary>
        public bool IsNew
        {
            get => isNew;
            set => isNew = value;
        }

        /// <summary>
        /// Unique identifier for this vehicle.
        /// </summary>
        public int IdentificationNumber => id;

        /// <summary>
        /// Total number of vehicles created.
        /// </summary>
        public static int Count => count;

        /// <summary>
        /// A short label describing the vehicle type ("Car", "Motorcycle", ect).
        /// </summary>
        public abstract string Type { get; }

        /// <summary>
        /// Returns a nicely formatted string representation of the vehicle.
        /// </summary>
        public override string ToString()
        {
            string condition = isNew ? "New" : "Used";
            return $"[{IdentificationNumber}] {Year} {Make} {Model} - {Type} - {condition} - {Price:C}";
        }
    }
}
