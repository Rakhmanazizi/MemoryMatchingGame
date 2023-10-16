using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemoryMatchingGame
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            AssignIconsToSquares();
        }


        // firstClicked point to the first Label control
        // that the player clicks, but it will be null
        // if the player hasn't clicked label yet
        Label firstClicked = null;
        Label secondClicked = null;

        // Use this random object to choose random icons for the squares
        Random random = new Random();

        // Each of these letters is an interesting icon
        // in the Webdings font,
        // and each icon appears twice in this list
        List<string> icons = new List<string>()
        {
            "!", "!", "N", "N", ",", ",", "k", "k",
            "b", "b", "v", "v", "w", "w", "z", "z"
        };

        private void AssignIconsToSquares()
        {
            // the tableLayoutPanel has 16 panels,
            // and the icon list has 16 icons,
            // so and icon is pulled at random from the list
            // and added to each label
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];
                    iconLabel.ForeColor = iconLabel.BackColor;
                    icons.RemoveAt(randomNumber);
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Label clickedLabel = sender as Label;

            if (clickedLabel != null)
            {
                // if the clicked label is black, the player clicked
                // an icon that's already been revealed 
                // ignore the click
                if (clickedLabel.ForeColor == Color.Black)
                    return;

                // if the firstClicked is null, this is the first icon
                // in the pair that the player clicked,
                // so set firstClicked to the label that the player
                // clicked, change its color to black, and return it
                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;
                    return;
                }

                // if the player gets this far, the timer isn't
                // running and firstClicked isn't null,
                // so this must be the second icon the player clicked
                // set its color to black
                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;

                // check to see if the player won
                CheckForWinner();

                if (firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;
                    return;
                }

                timer1.Start();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // stop the timer
            timer1.Stop();

            // hide both icons
            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            // reset firstClicked and secondClicked
            // so the next time a label is
            // clicked, the program knos it's the first click
            firstClicked = null; 
            secondClicked = null;
        }

        private void CheckForWinner()
        {
            // go through all of the labels in the TableLayoutPanel,
            // checking each one to see if its icon is matched
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                }
            }

            // if the loop didn't return, it didn't find
            // any unmatched icons
            // that means the user won
            // Show a message and close the form
            MessageBox.Show("Selamat... kamu berhasil mencocokan semua ikon!", "Kamu berhasil");
            Close();
        }
    }
}
