using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace WindowsFormsApp11111
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        double[,] Q = {
            { -0.4, 0.3, 0.1 },
            { 0.4, -0.8, 0.4 },
            { 0.1, 0.4, -0.5 }
        };

        Random random = new Random();
        double[] p = new double[3], count = new double[3];
        int t, n, new_state = 0, k = 0, good = 0, bad = 0, ugly = 0;
        double dt, rand, t_curr = 0;

        private void button1_Click_1(object sender, EventArgs e)
        {
            t = (int)numericUpDown1.Value;
            n = (int)numericUpDown2.Value;

            good = 0;
            bad = 0;
            ugly = 0;
            dt = 0;
            t_curr = 0;
            k = 0;
            new_state = 0;

            for (int i = 0; i < 3; i++)
            {
                count[i] = 0;
            }

            button1.Text = "В процессе...";

            timer1.Start();
        }

        int new_rand()
        {
            rand = random.NextDouble();
            int state = 0;

            for (int i = 0; i < 3; i++)
            {
                rand -= p[i];

                if (rand <= 0)
                {
                    state = i;
                    break;
                }
            }

            return state;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (k >= n)
            {
                label3.Text = good.ToString();
                label5.Text = bad.ToString();
                label7.Text = ugly.ToString();

                count[0] = count[0] / n;
                label4.Text = count[0].ToString();

                count[1] = count[1] / n;
                label6.Text = count[1].ToString();

                count[2] = count[2] / n;
                label8.Text = count[2].ToString();

                button1.Text = "Начать заново";

                timer1.Stop();
            }

            if (t_curr < t)
            {
                dt = Math.Log(random.NextDouble()) / Q[new_state, new_state];
                t_curr += dt;

                for (int i = 0; i < 3; i++)
                {
                    if (i != new_state)
                    {
                        p[i] = -Q[new_state, i] / Q[new_state, new_state];
                    }
                    else
                    {
                        p[i] = 0;
                    }
                }

                new_state = new_rand();

                if (new_state == 0)
                {
                    good++;
                }
                else
                {
                    if (new_state == 1)
                    {
                        bad++;
                    }
                    else
                    {
                        ugly++;
                    }
                }
            }
            else
            {
                count[new_state]++;
                k++;

                t_curr = 0;
            }
        }
    }
}
