// Author: Rebecca Jones
// Created: October 1, 2025
// Updated: November 16, 2025
// Description:
// Code-behind for the CarViewer WPF application.
// Allows adding Cars and Motorcycles, shows a list of all vehicles,
// displays statistics, and reports status messages with timestamps.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CarViewer
{
    public partial class MainWindow : Window
    {
        // A list of all vehicles in the inventory.
        private readonly List<Vehicle> listOfVehicles = new List<Vehicle>();

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Runs when the form loads. Populates controls and adds demo data.
        /// </summary>
        private void FormLoad(object sender, RoutedEventArgs e)
        {
            PopulateVehicleTypes();
            PopulateMakes();
            PopulateYears();

            // Demo data – helps the stats tab feel less like a red herring.
            listOfVehicles.Add(new Car("Hyundai", "Tucson", 2020, 17500m, true));
            listOfVehicles.Add(new Car("Dodge", "Caliber", 2012, 11499m, true));
            listOfVehicles.Add(new Car("Volkswagen", "Beetle", 1979, 5999m, false));
            listOfVehicles.Add(new Motorcycle("Honda", "CB500F", 2022, 8500m, true, false));

            listViewCars.ItemsSource = listOfVehicles;
            SetDefaults();
            UpdateStatistics();
            UpdateStatus("Application started.");
        }

        private void PopulateVehicleTypes()
        {
            comboVehicleType.Items.Clear();
            comboVehicleType.Items.Add("Car");
            comboVehicleType.Items.Add("Motorcycle");
        }

        private void PopulateMakes()
        {
            comboMake.Items.Clear();
            string[] makes = {
                "Honda", "Toyota", "Ford", "Chevrolet", "Hyundai", "Kia",
                "Nissan", "Volkswagen", "Mazda", "Dodge", "BMW", "Mercedes-Benz"
            };

            foreach (var make in makes)
            {
                comboMake.Items.Add(make);
            }
        }

        private void PopulateYears()
        {
            comboYear.Items.Clear();
            int currentYear = DateTime.Now.Year;

            for (int year = currentYear; year >= currentYear - 50; year--)
            {
                comboYear.Items.Add(year);
            }
        }

        /// <summary>
        /// Reset all input controls back to a default state for new entries.
        /// </summary>
        private void SetDefaults()
        {
            comboVehicleType.SelectedIndex = 0;
            comboMake.SelectedIndex = 0;
            comboYear.SelectedIndex = 0;
            textModel.Clear();
            textPrice.Clear();
            checkIsNew.IsChecked = false;
            checkHasSidecar.IsChecked = false;

            listViewCars.SelectedIndex = -1;

            Unhighlight(textModel);
            Unhighlight(textPrice);

            comboVehicleType.Focus();
        }

        private void ResetClick(object sender, RoutedEventArgs e)
        {
            SetDefaults();
            UpdateStatus("Form reset for new vehicle entry.");
        }

        private void ExitClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Validate, create, and add a new vehicle to the inventory.
        /// Handles argument exceptions from the model layer.
        /// </summary>
        private void EnterVehicleClick(object sender, RoutedEventArgs e)
        {
            labelOutput.Content = string.Empty;
            Unhighlight(textModel);
            Unhighlight(textPrice);

            // Basic validation for numeric price.
            if (!decimal.TryParse(textPrice.Text.Trim(), out decimal rawPrice))
            {
                labelOutput.Content = "ERROR: Price must be a valid number.";
                ErrorHighlight(textPrice);
                UpdateStatus("Error adding vehicle: price must be numeric.");
                return;
            }

            string? vehicleType = comboVehicleType.SelectedItem?.ToString();
            if (string.IsNullOrWhiteSpace(vehicleType))
            {
                labelOutput.Content = "Please select a vehicle type.";
                comboVehicleType.Focus();
                UpdateStatus("Error adding vehicle: vehicle type not selected.");
                return;
            }

            if (comboMake.SelectedItem == null)
            {
                labelOutput.Content = "Please select a make.";
                comboMake.Focus();
                UpdateStatus("Error adding vehicle: make not selected.");
                return;
            }

            if (comboYear.SelectedItem == null)
            {
                labelOutput.Content = "Please select a year.";
                comboYear.Focus();
                UpdateStatus("Error adding vehicle: year not selected.");
                return;
            }

            string make = comboMake.SelectedItem.ToString() ?? string.Empty;
            string model = textModel.Text.Trim();
            int year = (int)comboYear.SelectedItem;
            bool isNew = checkIsNew.IsChecked == true;
            bool hasSidecar = checkHasSidecar.IsChecked == true;

            try
            {
                Vehicle newVehicle;

                if (vehicleType == "Car")
                {
                    newVehicle = new Car(make, model, year, rawPrice, isNew);
                }
                else
                {
                    newVehicle = new Motorcycle(make, model, year, rawPrice, isNew, hasSidecar);
                }

                listOfVehicles.Add(newVehicle);
                listViewCars.Items.Refresh();

                labelOutput.Content = $"Added: {newVehicle}";
                UpdateStatistics();
                UpdateStatus($"Added new {newVehicle.Type} #{newVehicle.IdentificationNumber}.");
                SetDefaults();
            }
            catch (ArgumentNullException)
            {
                labelOutput.Content = "Model cannot be blank. Please enter a model.";
                ErrorHighlight(textModel);
                UpdateStatus("Error: model was left blank while adding a vehicle.");
            }
            catch (ArgumentOutOfRangeException)
            {
                labelOutput.Content = "Price cannot be negative. Please enter a valid price.";
                ErrorHighlight(textPrice);
                UpdateStatus("Error: negative price entered while adding a vehicle.");
            }
            catch (Exception)
            {
                labelOutput.Content = "An unexpected error occurred while adding the vehicle.";
                UpdateStatus("Unexpected error while adding a vehicle.");
            }
        }

        /// <summary>
        /// Highlight a TextBox that's in error.
        /// </summary>
        private void ErrorHighlight(TextBox boxInError)
        {
            boxInError.BorderBrush = Brushes.Red;
            boxInError.Background = Brushes.MistyRose;
            boxInError.SelectAll();
            boxInError.Focus();
        }

        /// <summary>
        /// Reset a TextBox's visual style closer to default.
        /// </summary>
        private void Unhighlight(TextBox boxToClear)
        {
            boxToClear.BorderBrush = Brushes.Gray;
            boxToClear.Background = Brushes.White;
        }

        /// <summary>
        /// Update the status bar with a message and the current time.
        /// </summary>
        private void UpdateStatus(string message)
        {
            if (statusTextBlock != null)
            {
                statusTextBlock.Text = $"{DateTime.Now:T} - {message}";
            }
        }

        /// <summary>
        /// Calculate and display statistics for the Statistics tab.
        /// </summary>
        private void UpdateStatistics()
        {
            int count = listOfVehicles.Count;
            decimal total = listOfVehicles.Sum(v => v.Price);
            decimal average = count > 0 ? total / count : 0m;

            textTotalVehicles.Text = count.ToString();
            textTotalPrice.Text = total.ToString("C");
            textAveragePrice.Text = average.ToString("C");
        }

        /// <summary>
        /// When a row is double-clicked in the list, show info & switch to Add tab.
        /// (Optional editing behaviour.)
        /// </summary>
        private void ListViewItem_DoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (listViewCars.SelectedItem is not Vehicle v)
            {
                return;
            }

            comboVehicleType.SelectedItem = v.Type;
            comboMake.SelectedItem = v.Make;
            comboYear.SelectedItem = v.Year;
            textModel.Text = v.Model;
            textPrice.Text = v.Price.ToString();
            checkIsNew.IsChecked = v.IsNew;

            if (v is Motorcycle moto)
            {
                checkHasSidecar.IsChecked = moto.HasSidecar;
            }
            else
            {
                checkHasSidecar.IsChecked = false;
            }

            tabContainer.SelectedItem = tabAdd;
            UpdateStatus($"Loaded {v.Type} #{v.IdentificationNumber} into the Add tab.");
        }

        /// <summary>
        /// Respond when the user switches tabs (update status / stats).
        /// </summary>
        private void TabContainer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (tabAdd.IsSelected)
            {
                UpdateStatus("Adding vehicles in the Add tab.");
            }
            else if (tabList.IsSelected)
            {
                UpdateStatus("Viewing the inventory list.");
            }
            else if (tabStats.IsSelected)
            {
                UpdateStatistics();
                UpdateStatus("Viewing inventory statistics.");
            }
        }
    }
}
