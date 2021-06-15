// Programmer: Meredith Odell
// Date: 11/25/2019
// Project: Odell_4
// Descripion: Individual Assignment #4

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Odell_4
{
    public partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        // Handles the close button's click event
        // Closes the form and returns the user to the registration form
        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
