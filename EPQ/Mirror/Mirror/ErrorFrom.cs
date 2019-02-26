using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mirror
{
    public partial class ErrorFrom : Form
    {
        private string ErrorMessage = " ";
        private Color TextColour = Color.White;

        public ErrorFrom(string Message)
        {
            ErrorMessage = Message;
            InitializeComponent();
        }

        private void ErrorFrom_Load(object sender, EventArgs e)
        {
            Location = new Point((Screen.PrimaryScreen.Bounds.Width / 2) - (Size.Width / 2), (Screen.PrimaryScreen.Bounds.Height / 2) - (Size.Height / 2));
            BackColor = Color.Black;

            //Ok Label
            OKButton = new Label()
            {
                Name = "OKButton",
                Text = "OK",
                TextAlign = ContentAlignment.MiddleCenter,
                Cursor = Cursors.Hand,
                ForeColor = TextColour,
            };
            OKButton.Size = SetLabelSize(OKButton);
            OKButton.Location = new Point((Width / 2) - (OKButton.Size.Width/2), Height - OKButton.Size.Height);
            OKButton.MouseDown += new MouseEventHandler(Exit);

            //ErrorLabel
            ErrorLabel = new Label()
            {
                Name = "ErrorLabel",
                Text = ErrorMessage,
                ForeColor = TextColour,
                TextAlign = ContentAlignment.MiddleCenter,
                Size = new Size(this.Width - 10, this.Height - OKButton.Height - 10),
                Location = new Point(5, 5),
            };
            this.Controls.Add(ErrorLabel);
            this.Controls.Add(OKButton);

        }

        private Size SetLabelSize(Label label)
        {
            Size NewSize = new Size(0,0);
            while (NewSize.Width < TextRenderer.MeasureText(label.Text, label.Font).Width)
            {
                NewSize.Width++;
            }
            while(NewSize.Height < TextRenderer.MeasureText(label.Text, label.Font).Height)
            {
                NewSize.Height++;
            }
            return NewSize;
        }

        private void Exit(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                this.Close();
            }
        }


    }
}
