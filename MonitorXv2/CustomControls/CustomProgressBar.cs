using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorXv2.CustomControls
{
    public class CustomProgressBar : PictureBox
    {
        private double _value;
        public double Percentage
        {
            get => _value; set
            {
                if (_value < 0) 
                    throw new ArgumentOutOfRangeException("value");
                if (_value > 100) 
                    throw new ArgumentOutOfRangeException("value");
                else
                {
                    _value = value;

                    Bar.Width = (int)(Percentage / 100 * Width);

                    if (value < 33)
                    {
                        Bar.BackColor = Color.Green;
                    }
                    else if (value < 66)
                    {
                        Bar.BackColor = Color.Orange;
                    }
                    else
                    {
                        Bar.BackColor = Color.Red;
                    }
                }
            }
        }

        private PictureBox Bar { get; set; }
        private Label Label { get; set; }

        public new string Text { get => Label.Text; set => Label.Text = value; }

        public CustomProgressBar()
        {
            BackColor = Color.White;

            Bar = new PictureBox()
            {
                Parent = this,
                Location = new Point(0, 0),
                Size = new Size(0, Height),
                BackColor = Color.Black,
            };

            Label = new Label()
            {
                Parent = this,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter,
            };
        }
    }
}
