// Author:  Rebecca Jones
// Created: November 16, 2025
// Updated: November 16, 2025
// Description:
// Motorcycle class that inherits from Vehicle and adds a HasSidecar flag. 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarViewer
{
    internal class Motorcycle : Vehicle
    {
        /// <summary>
        /// Indicates whether the motorcycle has a sidecar attached.
        /// </summary>
        public bool HasSidecar { get; set; }

        public override string Type => "Motorcycle";

        public Motorcycle() : base()
        {
        }

        public Motorcycle(string bikeMake, string bikeModel, int bikeYear, decimal bikePrice, bool isBikeNew, bool hasSidecar)
            : base()
        {
            Make = bikeMake;
            Model = bikeModel;
            Year = bikeYear;
            Price = bikePrice;
            IsNew = isBikeNew;
            HasSidecar = hasSidecar;
        }

        public override string ToString()
        {
            string sidecarText = HasSidecar ? "with sidecar" : "no sidecar";
            string baseText = base.ToString();
            return $"{baseText} - {sidecarText}";
        }
    }
}
