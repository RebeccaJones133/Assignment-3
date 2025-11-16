// THIS IS A COMMENT HEADER
// Kyle
// November 5th
// This program needs a better description written by YOU

using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CarList2025
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        // Declaration.
        List<Vehicle> listOfVehicles = new List<Vehicle>();


        /// <summary>
        /// Constructor for the form.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            PopulateYears();
            SetDefaults();
            listViewCars.ItemsSource = listOfVehicles;
        }

        /// <summary>
        /// Populate the year combobox with the last 50 years.
        /// </summary>
        public void PopulateYears()
        {
            var currentYear = DateTime.Now.Year;

            for (int year = currentYear; year >= currentYear - 50; year--)
            {
                comboYear.Items.Add(year);
            }
        }

        /// <summary>
        /// Set all form controls back to their default state for new entries.
        /// </summary>
        public void SetDefaults()
        {
            comboMake.SelectedIndex = 0;
            textModel.Clear();
            comboYear.SelectedIndex = 0;
            textPrice.Clear();
            checkIsNew.IsChecked = false;

            listViewCars.SelectedIndex = -1;

            // Set default colours for controls that could be in error.
            Unhighlight(textModel);
            Unhighlight(textPrice);

            comboMake.Focus();
        }

        /// <summary>
        /// When Reset is clicked, reset stuff.
        /// </summary>
        private void ResetClick(object sender, RoutedEventArgs e)
        {
            SetDefaults();
        }

        /// <summary>
        /// Me close form.
        /// </summary>
        private void ExitClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Attempt to add the new vehicle into the list.
        /// </summary>
        private void EnterClick(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(textPrice.Text, out double price))
            {
                try
                {
                    var carToAdd = new Car(comboMake.Text, textModel.Text, int.Parse(comboYear.Text), (decimal)price, checkIsNew.IsChecked == true);
                    listOfVehicles.Add(carToAdd);
                    listViewCars.Items.Refresh();
                    SetDefaults();
                }
                catch (ArgumentNullException ex)
                {
                    // Here's some code that responds to errors in the model I guess?
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    // This is going to respond to errors in the price?
                }
            }
            else
            {
                textOutput.Text = "ERROR: Price must be a number";
                ErrorHighlight(textOutput);
            }
        }

        /// <summary>
        /// Highlight a control that's in error.
        /// </summary>
        /// <param name="boxInError">TextBox that's in error.</param>
        private void ErrorHighlight(TextBox boxInError)
        {
            boxInError.BorderBrush = Brushes.Red;
            boxInError.Background = Brushes.MistyRose;
            boxInError.SelectAll();
            boxInError.Focus();
        }

        /// <summary>
        /// Attempt (badly) to set a control's colours closer to default.
        /// </summary>
        /// <param name="boxToClear">Box you want default colours on.</param>
        private void Unhighlight(TextBox boxToClear)
        {
            boxToClear.BorderBrush = textOutput.BorderBrush;
            boxToClear.Background = Brushes.LightGray;
        }
    }
}