// Programmer: Meredith Odell
// Date: 11/25/2019
// Project: Odell_4
// Descripion: Individual Assignment #4

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Odell_4
{
    public partial class RegistrationForm : Form
    {
        // Declare and initialize class-level constants
        private const decimal LIVE_ACTION_RATE = 79.95m;
        private const decimal ANIMATION_RATE = 99.95m;

        public RegistrationForm()
        {
            InitializeComponent();
        }

        // Handles the registration form's load event
        // Sets the default parameters for controls
        private void RegistrationForm_Load(object sender, EventArgs e)
        {
            // Call custom method to set form to default
            ResetForm();

            // Call custom method to fill classes listbox depending on which class type is selected
            PopulateList();

            // Call custom method to update totals
            UpdateTotals();

            // Create an array to initialize the values available for the status combo box
            string[] statusArray = {"Actor","Producer",
            "Director","Animator","Cinematographer","Drama Teacher",
            "Light Technician","Sound Technician"};

            // Adds array strings to status combo box
            for (int index = 0; index < statusArray.Length; index++)
            {
                statusComboBox.Items.Add(statusArray[index]);
            }
        }

        // Custom method
        // Populates the classes list box depending on which class type the userhas selected
        private void PopulateList()
        {
            try
            {
                // Reads from the LiveActionClasses.txt file
                if (liveActionRadioButton.Checked)
                {
                    StreamReader inputFile;

                    inputFile = File.OpenText("LiveActionClasses.txt");

                    while (!inputFile.EndOfStream)
                    {
                        classesListBox.Items.Add(inputFile.ReadLine());
                    }

                    // Closes the inputFile
                    inputFile.Close();
                }

                // Reads from the AnimationClasses.txt file
                else
                {
                    StreamReader inputFile;

                    inputFile = File.OpenText("AnimationClasses.txt");

                    while (!inputFile.EndOfStream)
                    {
                        classesListBox.Items.Add(inputFile.ReadLine());
                    }

                    // Closes the inputFile
                    inputFile.Close();
                }
            }
            // Shows a message if there was an error reading data from the txt files
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }   

        // Custom method
        // Performs calculations based on certain choices the user makes
        // Displays the totals in the registration group box
        private void UpdateTotals()
        {
            decimal total;
            decimal numberOfClasses = classesListBox.SelectedItems.Count;
            decimal pricePerClass;

            // Handles which rate is applied to the price per class price
            if (liveActionRadioButton.Checked)
            {
                pricePerClass = LIVE_ACTION_RATE;
            }
            else
            {
                pricePerClass = ANIMATION_RATE;
            }

            total = numberOfClasses * pricePerClass;

            // Displays the prices in the respective labels
            totalPriceLabel.Text = total.ToString("c");
            numberOfClassesLabel.Text = numberOfClasses.ToString();
            pricePerClassLabel.Text = pricePerClass.ToString("c");
        }

        // Custom method
        // Returns the form to the default settings
        private void ResetForm()
        {
            // Sets the time to the present computer's system clock
            dateMaskedTextBox.Text = DateTime.Now.ToString("MM/dd/yyyy");

            firstNameTextBox.Text = "";
            lastNameTextBox.Text = "";
            emailAddressTextBox.Text = "";
            dateOfBirthMaskedTextBox.Text = "";

            // Deselects the status combobox selected item
            statusComboBox.SelectedIndex = -1;
            // Clears the status combobox's text
            statusComboBox.Text = "";

            liveActionRadioButton.Checked = true;

            // Deselects the classes listbox's selected items
            classesListBox.SelectedIndex = -1;
            classesListBox.Items.Clear();

            numberOfClassesLabel.Text = "";
            pricePerClassLabel.Text = "";
            totalPriceLabel.Text = "";

            cashRadioButton.Checked = true;
            emailReceiptCheckBox.Checked = false;
        }

        // Handles the clear tool strip menu item's click event
        // Resets the form to it's default state
        private void ClearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Call custom method to clear form
            ResetForm();

            // Call custom method to reset form to populate the classes list box
            PopulateList();
        }

        // Handles the about tool strip menu item's click event
        // Displays the copyright information
        private void AboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Create an instance of the AboutForm form class
            AboutForm myAboutForm = new AboutForm();

            // Display AboutForm instance as a model form
            myAboutForm.ShowDialog();
        }

        // Handles the animation radio button's checked changed event
        // Updates the classes list box
        private void AnimationRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            // Clears listbox from previous selection
            classesListBox.SelectedIndex = -1;
            classesListBox.Items.Clear();

            // Call custom method to populate the classes list box
            PopulateList();

            // Call custom method to update the total labels
            UpdateTotals();
        }

        // Handles the live action radio button's checked changed event
        // Updates the classes list box
        private void LiveActionRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            // Clears listbox from previous selection
            classesListBox.SelectedIndex = -1;
            classesListBox.Items.Clear();

            // Call custom method to populate the classes list box
            PopulateList();

            // Call custom method to update the total labels
            UpdateTotals();
        }

        // Handles the exit too strip menu item's click event
        // Closes the program or sends the user back to the first name text box
        private void ExitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Do you really want to exit?",
                "Exit COnfirmation",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            // If the user clicks the yes button on the message box it will close the program
            if (dialog == DialogResult.Yes)
            {
                this.Close();
            }
            // If the user clicks the no button on the message box it will send them back tot eh first name text box
            else if (dialog == DialogResult.No)
            {
                firstNameTextBox.Focus();
            }
        }

        // Handles the save tool strip menu item's click event
        // Creates a summary of teh users selections
        // Writes the summary to the external file RegistrationData.txt
        // Displays the summary in a message box
        private void SaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Declare and initialize local variables
            string classType;
            string paymentType;
            string emailReceiptStatus;
            int numberOfClasses;

            numberOfClasses = int.Parse(numberOfClassesLabel.Text);

            // Creates strings from the selected classes in the classes list box
            StringBuilder classes = new StringBuilder();
            foreach (object selectedItem in classesListBox.SelectedItems)
            {
                classes.AppendLine(selectedItem.ToString());
            }

            // Handles which option to display for the class type radio buttons
            if (liveActionRadioButton.Checked)
            {
                classType = "Live Action";
            }
            else
            {
                classType = "Animation";
            }

            // Handles which option to display for the payment type radio buttons
            if (cashRadioButton.Checked)
            {
                paymentType = "Cash";
            }
            else
            {
                paymentType = "Check";
            }

            // Displays whether or not the email receipt request check box was checked or not
            if (emailReceiptCheckBox.Checked)
            {
                emailReceiptStatus = "Yes";
            }
            else
            {
                emailReceiptStatus = "No";
            }

            // Displays an error message if the first name and last name are not filled, or if the phone number was incomplete
            if (firstNameTextBox.Text == "" || lastNameTextBox.Text == "" || emailAddressTextBox.Text == "" || dateOfBirthMaskedTextBox.MaskCompleted == false)
            {
                MessageBox.Show("You must enter a first name, last name, email address, and valid date of birth.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            // Displays the summary if all data is correct
            else
            {
                // Only executes the message summary and writes to the txt file if there are 4 or less classes selected
                if (numberOfClasses >= 1)
                {
                    if (numberOfClasses <= 4)
                    {
                        // Try-catch shows a message if there was an issue with writing the data to the txt file
                        try
                        {
                            // Writes the summary to the RegistrationData.txt file
                            StreamWriter outputFile;

                            outputFile = File.AppendText("RegistrationData.txt");

                            outputFile.WriteLine("Date: " + DateTime.Now.ToString("MM/dd/yyyy"));
                            outputFile.WriteLine("Name: " + firstNameTextBox.Text + " " + lastNameTextBox.Text);
                            outputFile.WriteLine("Email Address: " + emailAddressTextBox.Text);
                            outputFile.WriteLine("Date Of Birth: " + dateOfBirthMaskedTextBox.Text);
                            outputFile.WriteLine("Status: " + statusComboBox.Text);

                            // Handles which class type to display based on radio button selection
                            if (liveActionRadioButton.Checked)
                            {
                                outputFile.WriteLine("Class Type: Live Action");
                            }
                            else
                            {
                                outputFile.WriteLine("Class Type: Animation");
                            }

                            outputFile.WriteLine("Number Of Classes: " + numberOfClassesLabel.Text);
                            outputFile.WriteLine("Price Per Class: " + pricePerClassLabel.Text);
                            outputFile.WriteLine("Total Price: " + totalPriceLabel.Text);

                            // Displays all selected classes in the classes list box
                            for (int count = 0; count < classesListBox.Items.Count; count++)
                            {
                                if (classesListBox.GetSelected(count))
                                {
                                    outputFile.WriteLine(classesListBox.Items[count]);
                                }
                            }

                            // Handles which payment type to display based on radio button selection
                            if (cashRadioButton.Checked)
                            {
                                outputFile.WriteLine("Payment Type: Cash");
                            }
                            else
                            {
                                outputFile.WriteLine("Payment Type: Check");
                            }

                            // Displays whether or not there is a email receipt requested based on whether the check box is checked
                            if (emailReceiptCheckBox.Checked)
                            {
                                outputFile.WriteLine("Email Receipt Requested: Yes");
                            }
                            else
                            {
                                outputFile.WriteLine("Email Receipt Requested: No");
                            }

                            // Writes an empty line to seperate entities
                            outputFile.WriteLine();

                            // Closes the outputFile
                            outputFile.Close();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                        // Shows the summary in a message box
                        MessageBox.Show("Hopkins Film School Registration Summary" + Environment.NewLine
                            + "Date: " + dateMaskedTextBox.Text + Environment.NewLine
                            + "Name: " + firstNameTextBox.Text + " " + lastNameTextBox.Text + Environment.NewLine
                            + "Email Address: " + emailAddressTextBox.Text + Environment.NewLine
                            + "Date of Birth: " + dateOfBirthMaskedTextBox.Text + Environment.NewLine
                            + "Status: " + statusComboBox.Text + Environment.NewLine
                            + "Class Type: " + classType + Environment.NewLine
                            + "Number Of Classes: " + numberOfClassesLabel.Text + Environment.NewLine
                            + "Price Per Class: " + pricePerClassLabel.Text + Environment.NewLine
                            + "Total Price: " + totalPriceLabel.Text + Environment.NewLine
                            + "Classes: " + Environment.NewLine
                            + classes 
                            + "Payment Method: " + paymentType + Environment.NewLine
                            + "Email Recepit Requested: " + emailReceiptStatus,
                            "Registration Summary",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);

                        // Call custom method to clear the form and send it back to it's orginal state
                        ResetForm();

                        //Call custom method to populate the classes list box
                        PopulateList();
                    }
                    else
                    {
                        MessageBox.Show("You can choose up to 4 classes.",
                           "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);

                        // Sets the focus back to the first name text box
                        firstNameTextBox.Focus();
                    }
                }
                else
                {
                    MessageBox.Show("You can choose up to 4 classes.",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                    // Sets the focus back to the first name text box
                    firstNameTextBox.Focus();
                }
            }
        }

        // Handles the classes list box's selected index change
        private void ClassesListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Call custom method to update the total labels
            UpdateTotals();
        }
    }
}
