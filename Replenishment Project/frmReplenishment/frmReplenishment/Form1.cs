using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace frmReplenishment
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int num = 0;
            Int32.TryParse(txtTime.Text, out num);
            num = num + 1;
            txtTime.Text = num.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Input parameters
            int[,,] fijt    = new int[10, 10, 4];
            int[,,] Qijt    = new int[10, 10, 4];
            double[] pri    = new double[] { 25, 25, 120, 40, 50, 25, 70, 60, 80, 15 };
            double[] cu     = new double[] { 8, 8, 40, 13, 15, 8, 25, 20, 30, 5 };
            double[] distj  = new double[] { 40, 50, 55, 70, 60, 450, 480, 760, 800, 800 };
            double[] chv    = new double[] { 0.06f, 0.03f, 0.2f, 0.1f, 0.15f, 0.02f, 0.07f, 0.2f, 0.1f, 0.01f };
            double B        = 100000;
            double ctv      = 0.05f;
            double ctf      = 5;
            double chf      = 5;
            double cs       = 0.01f;
            double unc      = 0.2f;
            int trange      = 43;
            double profit   = 0;
            int totalRuns   = 5;

            // tabu bees parameters
            int n = 100;
            int e_e = 3;
            int b = 3;
            int ne = 5;
            int nb = 5;

            //////////////////////////////////////////////////////////////////////////////////


            fijt[0, 0, 0] = 0;
            fijt[0, 1, 0] = 0;
            fijt[0, 2, 0] = 13;
            fijt[0, 3, 0] = 0;
            fijt[0, 4, 0] = 13;
            fijt[0, 5, 0] = 13;
            fijt[0, 6, 0] = 13;
            fijt[0, 7, 0] = 0;
            fijt[0, 8, 0] = 0;
            fijt[0, 9, 0] = 0;

            fijt[1, 0, 0] = 31;
            fijt[1, 1, 0] = 14;
            fijt[1, 2, 0] = 26;
            fijt[1, 3, 0] = 23;
            fijt[1, 4, 0] = 21;
            fijt[1, 5, 0] = 8;
            fijt[1, 6, 0] = 3;
            fijt[1, 7, 0] = 1;
            fijt[1, 8, 0] = 10;
            fijt[1, 9, 0] = 8;

            fijt[2, 0, 0] = 3;
            fijt[2, 1, 0] = 4;
            fijt[2, 2, 0] = 9;
            fijt[2, 3, 0] = 4;
            fijt[2, 4, 0] = 4;
            fijt[2, 5, 0] = 2;
            fijt[2, 6, 0] = 5;
            fijt[2, 7, 0] = 4;
            fijt[2, 8, 0] = 1;
            fijt[2, 9, 0] = 4;

            fijt[3, 0, 0] = 13;
            fijt[3, 1, 0] = 15;
            fijt[3, 2, 0] = 12;
            fijt[3, 3, 0] = 5;
            fijt[3, 4, 0] = 13;
            fijt[3, 5, 0] = 17;
            fijt[3, 6, 0] = 3;
            fijt[3, 7, 0] = 8;
            fijt[3, 8, 0] = 3;
            fijt[3, 9, 0] = 7;

            fijt[4, 0, 0] = 13;
            fijt[4, 1, 0] = 15;
            fijt[4, 2, 0] = 12;
            fijt[4, 3, 0] = 5;
            fijt[4, 4, 0] = 13;
            fijt[4, 5, 0] = 17;
            fijt[4, 6, 0] = 3;
            fijt[4, 7, 0] = 8;
            fijt[4, 8, 0] = 3;
            fijt[4, 9, 0] = 7;

            fijt[5, 0, 0] = 4;
            fijt[5, 1, 0] = 4;
            fijt[5, 2, 0] = 8;
            fijt[5, 3, 0] = 4;
            fijt[5, 4, 0] = 21;
            fijt[5, 5, 0] = 4;
            fijt[5, 6, 0] = 0;
            fijt[5, 7, 0] = 0;
            fijt[5, 8, 0] = 4;
            fijt[5, 9, 0] = 0;

            fijt[6, 0, 0] = 4;
            fijt[6, 1, 0] = 6;
            fijt[6, 2, 0] = 17;
            fijt[6, 3, 0] = 3;
            fijt[6, 4, 0] = 13;
            fijt[6, 5, 0] = 0;
            fijt[6, 6, 0] = 4;
            fijt[6, 7, 0] = 4;
            fijt[6, 8, 0] = 5;
            fijt[6, 9, 0] = 3;

            fijt[7, 0, 0] = 9;
            fijt[7, 1, 0] = 0;
            fijt[7, 2, 0] = 14;
            fijt[7, 3, 0] = 2;
            fijt[7, 4, 0] = 0;
            fijt[7, 5, 0] = 0;
            fijt[7, 6, 0] = 3;
            fijt[7, 7, 0] = 0;
            fijt[7, 8, 0] = 0;
            fijt[7, 9, 0] = 0;

            fijt[8, 0, 0] = 13;
            fijt[8, 1, 0] = 2;
            fijt[8, 2, 0] = 4;
            fijt[8, 3, 0] = 1;
            fijt[8, 4, 0] = 4;
            fijt[8, 5, 0] = 3;
            fijt[8, 6, 0] = 1;
            fijt[8, 7, 0] = 7;
            fijt[8, 8, 0] = 2;
            fijt[8, 9, 0] = 1;

            fijt[9, 0, 0] = 18;
            fijt[9, 1, 0] = 6;
            fijt[9, 2, 0] = 14;
            fijt[9, 3, 0] = 9;
            fijt[9, 4, 0] = 10;
            fijt[9, 5, 0] = 6;
            fijt[9, 6, 0] = 3;
            fijt[9, 7, 0] = 9;
            fijt[9, 8, 0] = 8;
            fijt[9, 9, 0] = 5;


            //////////////////////////////////////////////////////////////////////////////////


            fijt[0, 0, 1] = 8;
            fijt[0, 1, 1] = 4;
            fijt[0, 2, 1] = 13;
            fijt[0, 3, 1] = 6;
            fijt[0, 4, 1] = 12;
            fijt[0, 5, 1] = 4;
            fijt[0, 6, 1] = 2;
            fijt[0, 7, 1] = 0;
            fijt[0, 8, 1] = 0;
            fijt[0, 9, 1] = 13;

            fijt[1, 0, 1] = 21;
            fijt[1, 1, 1] = 7;
            fijt[1, 2, 1] = 16;
            fijt[1, 3, 1] = 3;
            fijt[1, 4, 1] = 18;
            fijt[1, 5, 1] = 8;
            fijt[1, 6, 1] = 4;
            fijt[1, 7, 1] = 8;
            fijt[1, 8, 1] = 1;
            fijt[1, 9, 1] = 4;

            fijt[2, 0, 1] = 7;
            fijt[2, 1, 1] = 1;
            fijt[2, 2, 1] = 17;
            fijt[2, 3, 1] = 5;
            fijt[2, 4, 1] = 4;
            fijt[2, 5, 1] = 4;
            fijt[2, 6, 1] = 2;
            fijt[2, 7, 1] = 3;
            fijt[2, 8, 1] = 2;
            fijt[2, 9, 1] = 2;

            fijt[3, 0, 1] = 13;
            fijt[3, 1, 1] = 13;
            fijt[3, 2, 1] = 13;
            fijt[3, 3, 1] = 0;
            fijt[3, 4, 1] = 0;
            fijt[3, 5, 1] = 0;
            fijt[3, 6, 1] = 0;
            fijt[3, 7, 1] = 12;
            fijt[3, 8, 1] = 6;
            fijt[3, 9, 1] = 5;

            fijt[4, 0, 1] = 13;
            fijt[4, 1, 1] = 0;
            fijt[4, 2, 1] = 12;
            fijt[4, 3, 1] = 6;
            fijt[4, 4, 1] = 0;
            fijt[4, 5, 1] = 11;
            fijt[4, 6, 1] = 1;
            fijt[4, 7, 1] = 12;
            fijt[4, 8, 1] = 6;
            fijt[4, 9, 1] = 5;

            fijt[5, 0, 1] = 17;
            fijt[5, 1, 1] = 15;
            fijt[5, 2, 1] = 4;
            fijt[5, 3, 1] = 2;
            fijt[5, 4, 1] = 10;
            fijt[5, 5, 1] = 19;
            fijt[5, 6, 1] = 2;
            fijt[5, 7, 1] = 13;
            fijt[5, 8, 1] = 8;
            fijt[5, 9, 1] = 0;

            fijt[6, 0, 1] = 4;
            fijt[6, 1, 1] = 1;
            fijt[6, 2, 1] = 44;
            fijt[6, 3, 1] = 4;
            fijt[6, 4, 1] = 6;
            fijt[6, 5, 1] = 1;
            fijt[6, 6, 1] = 1;
            fijt[6, 7, 1] = 0;
            fijt[6, 8, 1] = 13;
            fijt[6, 9, 1] = 0;

            fijt[7, 0, 1] = 16;
            fijt[7, 1, 1] = 2;
            fijt[7, 2, 1] = 13;
            fijt[7, 3, 1] = 3;
            fijt[7, 4, 1] = 0;
            fijt[7, 5, 1] = 2;
            fijt[7, 6, 1] = 0;
            fijt[7, 7, 1] = 2;
            fijt[7, 8, 1] = 0;
            fijt[7, 9, 1] = 0;

            fijt[8, 0, 1] = 16;
            fijt[8, 1, 1] = 1;
            fijt[8, 2, 1] = 4;
            fijt[8, 3, 1] = 3;
            fijt[8, 4, 1] = 7;
            fijt[8, 5, 1] = 3;
            fijt[8, 6, 1] = 2;
            fijt[8, 7, 1] = 5;
            fijt[8, 8, 1] = 2;
            fijt[8, 9, 1] = 2;

            fijt[9, 0, 1] = 25;
            fijt[9, 1, 1] = 6;
            fijt[9, 2, 1] = 14;
            fijt[9, 3, 1] = 3;
            fijt[9, 4, 1] = 4;
            fijt[9, 5, 1] = 7;
            fijt[9, 6, 1] = 1;
            fijt[9, 7, 1] = 7;
            fijt[9, 8, 1] = 4;
            fijt[9, 9, 1] = 3;

            //////////////////////////////////////////////////////////////////

            fijt[0, 0, 2] = 11;
            fijt[0, 1, 2] = 44;
            fijt[0, 2, 2] = 21;
            fijt[0, 3, 2] = 3;
            fijt[0, 4, 2] = 8;
            fijt[0, 5, 2] = 7;
            fijt[0, 6, 2] = 7;
            fijt[0, 7, 2] = 7;
            fijt[0, 8, 2] = 4;
            fijt[0, 9, 2] = 4;

            fijt[1, 0, 2] = 15;
            fijt[1, 1, 2] = 25;
            fijt[1, 2, 2] = 5;
            fijt[1, 3, 2] = 4;
            fijt[1, 4, 2] = 11;
            fijt[1, 5, 2] = 6;
            fijt[1, 6, 2] = 0;
            fijt[1, 7, 2] = 2;
            fijt[1, 8, 2] = 6;
            fijt[1, 9, 2] = 5;

            fijt[2, 0, 2] = 5;
            fijt[2, 1, 2] = 46;
            fijt[2, 2, 2] = 11;
            fijt[2, 3, 2] = 7;
            fijt[2, 4, 2] = 1;
            fijt[2, 5, 2] = 7;
            fijt[2, 6, 2] = 5;
            fijt[2, 7, 2] = 6;
            fijt[2, 8, 2] = 5;
            fijt[2, 9, 2] = 6;

            fijt[3, 0, 2] = 0;
            fijt[3, 1, 2] = 0;
            fijt[3, 2, 2] = 50;
            fijt[3, 3, 2] = 0;
            fijt[3, 4, 2] = 38;
            fijt[3, 5, 2] = 0;
            fijt[3, 6, 2] = 0;
            fijt[3, 7, 2] = 0;
            fijt[3, 8, 2] = 0;
            fijt[3, 9, 2] = 0;

            fijt[4, 0, 2] = 2;
            fijt[4, 1, 2] = 50;
            fijt[4, 2, 2] = 8;
            fijt[4, 3, 2] = 4;
            fijt[4, 4, 2] = 4;
            fijt[4, 5, 2] = 1;
            fijt[4, 6, 2] = 0;
            fijt[4, 7, 2] = 6;
            fijt[4, 8, 2] = 1;
            fijt[4, 9, 2] = 1;

            fijt[5, 0, 2] = 4;
            fijt[5, 1, 2] = 44;
            fijt[5, 2, 2] = 19;
            fijt[5, 3, 2] = 2;
            fijt[5, 4, 2] = 8;
            fijt[5, 5, 2] = 10;
            fijt[5, 6, 2] = 0;
            fijt[5, 7, 2] = 10;
            fijt[5, 8, 2] = 2;
            fijt[5, 9, 2] = 2;

            fijt[6, 0, 2] = 3;
            fijt[6, 1, 2] = 0;
            fijt[6, 2, 2] = 14;
            fijt[6, 3, 2] = 1;
            fijt[6, 4, 2] = 0;
            fijt[6, 5, 2] = 8;
            fijt[6, 6, 2] = 3;
            fijt[6, 7, 2] = 0;
            fijt[6, 8, 2] = 1;
            fijt[6, 9, 2] = 1;

            fijt[7, 0, 2] = 16;
            fijt[7, 1, 2] = 33;
            fijt[7, 2, 2] = 8;
            fijt[7, 3, 2] = 2;
            fijt[7, 4, 2] = 5;
            fijt[7, 5, 2] = 0;
            fijt[7, 6, 2] = 0;
            fijt[7, 7, 2] = 5;
            fijt[7, 8, 2] = 0;
            fijt[7, 9, 2] = 3;

            fijt[8, 0, 2] = 14;
            fijt[8, 1, 2] = 34;
            fijt[8, 2, 2] = 3;
            fijt[8, 3, 2] = 2;
            fijt[8, 4, 2] = 3;
            fijt[8, 5, 2] = 3;
            fijt[8, 6, 2] = 1;
            fijt[8, 7, 2] = 13;
            fijt[8, 8, 2] = 0;
            fijt[8, 9, 2] = 4;

            fijt[9, 0, 2] = 26;
            fijt[9, 1, 2] = 34;
            fijt[9, 2, 2] = 11;
            fijt[9, 3, 2] = 5;
            fijt[9, 4, 2] = 3;
            fijt[9, 5, 2] = 3;
            fijt[9, 6, 2] = 3;
            fijt[9, 7, 2] = 5;
            fijt[9, 8, 2] = 2;
            fijt[9, 9, 2] = 3;

            ///////////////////////////////////

            fijt[0, 0, 3] = 0;
            fijt[0, 1, 3] = 0;
            fijt[0, 2, 3] = 13;
            fijt[0, 3, 3] = 0;
            fijt[0, 4, 3] = 13;
            fijt[0, 5, 3] = 13;
            fijt[0, 6, 3] = 13;
            fijt[0, 7, 3] = 0;
            fijt[0, 8, 3] = 0;
            fijt[0, 9, 3] = 0;

            fijt[1, 0, 3] = 31;
            fijt[1, 1, 3] = 14;
            fijt[1, 2, 3] = 26;
            fijt[1, 3, 3] = 23;
            fijt[1, 4, 3] = 21;
            fijt[1, 5, 3] = 8;
            fijt[1, 6, 3] = 3;
            fijt[1, 7, 3] = 1;
            fijt[1, 8, 3] = 10;
            fijt[1, 9, 3] = 8;

            fijt[2, 0, 3] = 3;
            fijt[2, 1, 3] = 4;
            fijt[2, 2, 3] = 9;
            fijt[2, 3, 3] = 4;
            fijt[2, 4, 3] = 4;
            fijt[2, 5, 3] = 2;
            fijt[2, 6, 3] = 5;
            fijt[2, 7, 3] = 4;
            fijt[2, 8, 3] = 1;
            fijt[2, 9, 3] = 4;

            fijt[3, 0, 3] = 13;
            fijt[3, 1, 3] = 15;
            fijt[3, 2, 3] = 12;
            fijt[3, 3, 3] = 5;
            fijt[3, 4, 3] = 13;
            fijt[3, 5, 3] = 17;
            fijt[3, 6, 3] = 3;
            fijt[3, 7, 3] = 8;
            fijt[3, 8, 3] = 3;
            fijt[3, 9, 3] = 7;

            fijt[4, 0, 3] = 13;
            fijt[4, 1, 3] = 15;
            fijt[4, 2, 3] = 12;
            fijt[4, 3, 3] = 5;
            fijt[4, 4, 3] = 13;
            fijt[4, 5, 3] = 17;
            fijt[4, 6, 3] = 3;
            fijt[4, 7, 3] = 8;
            fijt[4, 8, 3] = 3;
            fijt[4, 9, 3] = 7;

            fijt[5, 0, 3] = 4;
            fijt[5, 1, 3] = 4;
            fijt[5, 2, 3] = 8;
            fijt[5, 3, 3] = 4;
            fijt[5, 4, 3] = 21;
            fijt[5, 5, 3] = 4;
            fijt[5, 6, 3] = 0;
            fijt[5, 7, 3] = 0;
            fijt[5, 8, 3] = 4;
            fijt[5, 9, 3] = 0;

            fijt[6, 0, 3] = 4;
            fijt[6, 1, 3] = 6;
            fijt[6, 2, 3] = 17;
            fijt[6, 3, 3] = 3;
            fijt[6, 4, 3] = 13;
            fijt[6, 5, 3] = 0;
            fijt[6, 6, 3] = 4;
            fijt[6, 7, 3] = 4;
            fijt[6, 8, 3] = 5;
            fijt[6, 9, 3] = 3;

            fijt[7, 0, 3] = 9;
            fijt[7, 1, 3] = 0;
            fijt[7, 2, 3] = 14;
            fijt[7, 3, 3] = 2;
            fijt[7, 4, 3] = 0;
            fijt[7, 5, 3] = 0;
            fijt[7, 6, 3] = 3;
            fijt[7, 7, 3] = 0;
            fijt[7, 8, 3] = 0;
            fijt[7, 9, 3] = 0;

            fijt[8, 0, 3] = 13;
            fijt[8, 1, 3] = 2;
            fijt[8, 2, 3] = 4;
            fijt[8, 3, 3] = 1;
            fijt[8, 4, 3] = 4;
            fijt[8, 5, 3] = 3;
            fijt[8, 6, 3] = 1;
            fijt[8, 7, 3] = 7;
            fijt[8, 8, 3] = 2;
            fijt[8, 9, 3] = 1;

            fijt[9, 0, 3] = 18;
            fijt[9, 1, 3] = 6;
            fijt[9, 2, 3] = 14;
            fijt[9, 3, 3] = 9;
            fijt[9, 4, 3] = 10;
            fijt[9, 5, 3] = 6;
            fijt[9, 6, 3] = 3;
            fijt[9, 7, 3] = 9;
            fijt[9, 8, 3] = 8;
            fijt[9, 9, 3] = 5;

            //////////////////////////////////////////////////////////////////////////////////


            //////////////////////////////////////////////////////////////////////////////////


            Qijt[0, 0, 0] = 0;
            Qijt[0, 1, 0] = 0;
            Qijt[0, 2, 0] = 13;
            Qijt[0, 3, 0] = 0;
            Qijt[0, 4, 0] = 13;
            Qijt[0, 5, 0] = 13;
            Qijt[0, 6, 0] = 13;
            Qijt[0, 7, 0] = 0;
            Qijt[0, 8, 0] = 0;
            Qijt[0, 9, 0] = 0;

            Qijt[1, 0, 0] = 31;
            Qijt[1, 1, 0] = 14;
            Qijt[1, 2, 0] = 26;
            Qijt[1, 3, 0] = 23;
            Qijt[1, 4, 0] = 21;
            Qijt[1, 5, 0] = 8;
            Qijt[1, 6, 0] = 3;
            Qijt[1, 7, 0] = 1;
            Qijt[1, 8, 0] = 10;
            Qijt[1, 9, 0] = 8;

            Qijt[2, 0, 0] = 3;
            Qijt[2, 1, 0] = 4;
            Qijt[2, 2, 0] = 9;
            Qijt[2, 3, 0] = 4;
            Qijt[2, 4, 0] = 4;
            Qijt[2, 5, 0] = 2;
            Qijt[2, 6, 0] = 5;
            Qijt[2, 7, 0] = 4;
            Qijt[2, 8, 0] = 1;
            Qijt[2, 9, 0] = 4;

            Qijt[3, 0, 0] = 13;
            Qijt[3, 1, 0] = 15;
            Qijt[3, 2, 0] = 12;
            Qijt[3, 3, 0] = 5;
            Qijt[3, 4, 0] = 13;
            Qijt[3, 5, 0] = 17;
            Qijt[3, 6, 0] = 3;
            Qijt[3, 7, 0] = 8;
            Qijt[3, 8, 0] = 3;
            Qijt[3, 9, 0] = 7;

            Qijt[4, 0, 0] = 13;
            Qijt[4, 1, 0] = 15;
            Qijt[4, 2, 0] = 12;
            Qijt[4, 3, 0] = 5;
            Qijt[4, 4, 0] = 13;
            Qijt[4, 5, 0] = 17;
            Qijt[4, 6, 0] = 3;
            Qijt[4, 7, 0] = 8;
            Qijt[4, 8, 0] = 3;
            Qijt[4, 9, 0] = 7;

            Qijt[5, 0, 0] = 4;
            Qijt[5, 1, 0] = 4;
            Qijt[5, 2, 0] = 8;
            Qijt[5, 3, 0] = 4;
            Qijt[5, 4, 0] = 21;
            Qijt[5, 5, 0] = 4;
            Qijt[5, 6, 0] = 0;
            Qijt[5, 7, 0] = 0;
            Qijt[5, 8, 0] = 4;
            Qijt[5, 9, 0] = 0;

            Qijt[6, 0, 0] = 4;
            Qijt[6, 1, 0] = 6;
            Qijt[6, 2, 0] = 17;
            Qijt[6, 3, 0] = 3;
            Qijt[6, 4, 0] = 13;
            Qijt[6, 5, 0] = 0;
            Qijt[6, 6, 0] = 4;
            Qijt[6, 7, 0] = 4;
            Qijt[6, 8, 0] = 5;
            Qijt[6, 9, 0] = 3;

            Qijt[7, 0, 0] = 9;
            Qijt[7, 1, 0] = 0;
            Qijt[7, 2, 0] = 14;
            Qijt[7, 3, 0] = 2;
            Qijt[7, 4, 0] = 0;
            Qijt[7, 5, 0] = 0;
            Qijt[7, 6, 0] = 3;
            Qijt[7, 7, 0] = 0;
            Qijt[7, 8, 0] = 0;
            Qijt[7, 9, 0] = 0;

            Qijt[8, 0, 0] = 13;
            Qijt[8, 1, 0] = 2;
            Qijt[8, 2, 0] = 4;
            Qijt[8, 3, 0] = 1;
            Qijt[8, 4, 0] = 4;
            Qijt[8, 5, 0] = 3;
            Qijt[8, 6, 0] = 1;
            Qijt[8, 7, 0] = 7;
            Qijt[8, 8, 0] = 2;
            Qijt[8, 9, 0] = 1;

            Qijt[9, 0, 0] = 18;
            Qijt[9, 1, 0] = 6;
            Qijt[9, 2, 0] = 14;
            Qijt[9, 3, 0] = 9;
            Qijt[9, 4, 0] = 10;
            Qijt[9, 5, 0] = 6;
            Qijt[9, 6, 0] = 3;
            Qijt[9, 7, 0] = 9;
            Qijt[9, 8, 0] = 8;
            Qijt[9, 9, 0] = 5;


            //////////////////////////////////////////////////////////////////////////////////


            Qijt[0, 0, 1] = 8;
            Qijt[0, 1, 1] = 4;
            Qijt[0, 2, 1] = 13;
            Qijt[0, 3, 1] = 6;
            Qijt[0, 4, 1] = 12;
            Qijt[0, 5, 1] = 4;
            Qijt[0, 6, 1] = 2;
            Qijt[0, 7, 1] = 0;
            Qijt[0, 8, 1] = 0;
            Qijt[0, 9, 1] = 13;

            Qijt[1, 0, 1] = 21;
            Qijt[1, 1, 1] = 7;
            Qijt[1, 2, 1] = 16;
            Qijt[1, 3, 1] = 3;
            Qijt[1, 4, 1] = 18;
            Qijt[1, 5, 1] = 8;
            Qijt[1, 6, 1] = 4;
            Qijt[1, 7, 1] = 8;
            Qijt[1, 8, 1] = 1;
            Qijt[1, 9, 1] = 4;

            Qijt[2, 0, 1] = 7;
            Qijt[2, 1, 1] = 1;
            Qijt[2, 2, 1] = 17;
            Qijt[2, 3, 1] = 5;
            Qijt[2, 4, 1] = 4;
            Qijt[2, 5, 1] = 4;
            Qijt[2, 6, 1] = 2;
            Qijt[2, 7, 1] = 3;
            Qijt[2, 8, 1] = 2;
            Qijt[2, 9, 1] = 2;

            Qijt[3, 0, 1] = 13;
            Qijt[3, 1, 1] = 13;
            Qijt[3, 2, 1] = 13;
            Qijt[3, 3, 1] = 0;
            Qijt[3, 4, 1] = 0;
            Qijt[3, 5, 1] = 0;
            Qijt[3, 6, 1] = 0;
            Qijt[3, 7, 1] = 12;
            Qijt[3, 8, 1] = 6;
            Qijt[3, 9, 1] = 5;

            Qijt[4, 0, 1] = 13;
            Qijt[4, 1, 1] = 0;
            Qijt[4, 2, 1] = 12;
            Qijt[4, 3, 1] = 6;
            Qijt[4, 4, 1] = 0;
            Qijt[4, 5, 1] = 11;
            Qijt[4, 6, 1] = 1;
            Qijt[4, 7, 1] = 12;
            Qijt[4, 8, 1] = 6;
            Qijt[4, 9, 1] = 5;

            Qijt[5, 0, 1] = 17;
            Qijt[5, 1, 1] = 15;
            Qijt[5, 2, 1] = 4;
            Qijt[5, 3, 1] = 2;
            Qijt[5, 4, 1] = 10;
            Qijt[5, 5, 1] = 19;
            Qijt[5, 6, 1] = 2;
            Qijt[5, 7, 1] = 13;
            Qijt[5, 8, 1] = 8;
            Qijt[5, 9, 1] = 0;

            Qijt[6, 0, 1] = 4;
            Qijt[6, 1, 1] = 1;
            Qijt[6, 2, 1] = 44;
            Qijt[6, 3, 1] = 4;
            Qijt[6, 4, 1] = 6;
            Qijt[6, 5, 1] = 1;
            Qijt[6, 6, 1] = 1;
            Qijt[6, 7, 1] = 0;
            Qijt[6, 8, 1] = 13;
            Qijt[6, 9, 1] = 0;

            Qijt[7, 0, 1] = 16;
            Qijt[7, 1, 1] = 2;
            Qijt[7, 2, 1] = 13;
            Qijt[7, 3, 1] = 3;
            Qijt[7, 4, 1] = 0;
            Qijt[7, 5, 1] = 2;
            Qijt[7, 6, 1] = 0;
            Qijt[7, 7, 1] = 2;
            Qijt[7, 8, 1] = 0;
            Qijt[7, 9, 1] = 0;

            Qijt[8, 0, 1] = 16;
            Qijt[8, 1, 1] = 1;
            Qijt[8, 2, 1] = 4;
            Qijt[8, 3, 1] = 3;
            Qijt[8, 4, 1] = 7;
            Qijt[8, 5, 1] = 3;
            Qijt[8, 6, 1] = 2;
            Qijt[8, 7, 1] = 5;
            Qijt[8, 8, 1] = 2;
            Qijt[8, 9, 1] = 2;

            Qijt[9, 0, 1] = 25;
            Qijt[9, 1, 1] = 6;
            Qijt[9, 2, 1] = 14;
            Qijt[9, 3, 1] = 3;
            Qijt[9, 4, 1] = 4;
            Qijt[9, 5, 1] = 7;
            Qijt[9, 6, 1] = 1;
            Qijt[9, 7, 1] = 7;
            Qijt[9, 8, 1] = 4;
            Qijt[9, 9, 1] = 3;

            //////////////////////////////////////////////////////////////////

            Qijt[0, 0, 2] = 11;
            Qijt[0, 1, 2] = 44;
            Qijt[0, 2, 2] = 21;
            Qijt[0, 3, 2] = 3;
            Qijt[0, 4, 2] = 8;
            Qijt[0, 5, 2] = 7;
            Qijt[0, 6, 2] = 7;
            Qijt[0, 7, 2] = 7;
            Qijt[0, 8, 2] = 4;
            Qijt[0, 9, 2] = 4;

            Qijt[1, 0, 2] = 15;
            Qijt[1, 1, 2] = 25;
            Qijt[1, 2, 2] = 5;
            Qijt[1, 3, 2] = 4;
            Qijt[1, 4, 2] = 11;
            Qijt[1, 5, 2] = 6;
            Qijt[1, 6, 2] = 0;
            Qijt[1, 7, 2] = 2;
            Qijt[1, 8, 2] = 6;
            Qijt[1, 9, 2] = 5;

            Qijt[2, 0, 2] = 5;
            Qijt[2, 1, 2] = 46;
            Qijt[2, 2, 2] = 11;
            Qijt[2, 3, 2] = 7;
            Qijt[2, 4, 2] = 1;
            Qijt[2, 5, 2] = 7;
            Qijt[2, 6, 2] = 5;
            Qijt[2, 7, 2] = 6;
            Qijt[2, 8, 2] = 5;
            Qijt[2, 9, 2] = 6;

            Qijt[3, 0, 2] = 0;
            Qijt[3, 1, 2] = 0;
            Qijt[3, 2, 2] = 50;
            Qijt[3, 3, 2] = 0;
            Qijt[3, 4, 2] = 38;
            Qijt[3, 5, 2] = 0;
            Qijt[3, 6, 2] = 0;
            Qijt[3, 7, 2] = 0;
            Qijt[3, 8, 2] = 0;
            Qijt[3, 9, 2] = 0;

            Qijt[4, 0, 2] = 2;
            Qijt[4, 1, 2] = 50;
            Qijt[4, 2, 2] = 8;
            Qijt[4, 3, 2] = 4;
            Qijt[4, 4, 2] = 4;
            Qijt[4, 5, 2] = 1;
            Qijt[4, 6, 2] = 0;
            Qijt[4, 7, 2] = 6;
            Qijt[4, 8, 2] = 1;
            Qijt[4, 9, 2] = 1;

            Qijt[5, 0, 2] = 4;
            Qijt[5, 1, 2] = 44;
            Qijt[5, 2, 2] = 19;
            Qijt[5, 3, 2] = 2;
            Qijt[5, 4, 2] = 8;
            Qijt[5, 5, 2] = 10;
            Qijt[5, 6, 2] = 0;
            Qijt[5, 7, 2] = 10;
            Qijt[5, 8, 2] = 2;
            Qijt[5, 9, 2] = 2;

            Qijt[6, 0, 2] = 3;
            Qijt[6, 1, 2] = 0;
            Qijt[6, 2, 2] = 14;
            Qijt[6, 3, 2] = 1;
            Qijt[6, 4, 2] = 0;
            Qijt[6, 5, 2] = 8;
            Qijt[6, 6, 2] = 3;
            Qijt[6, 7, 2] = 0;
            Qijt[6, 8, 2] = 1;
            Qijt[6, 9, 2] = 1;

            Qijt[7, 0, 2] = 16;
            Qijt[7, 1, 2] = 33;
            Qijt[7, 2, 2] = 8;
            Qijt[7, 3, 2] = 2;
            Qijt[7, 4, 2] = 5;
            Qijt[7, 5, 2] = 0;
            Qijt[7, 6, 2] = 0;
            Qijt[7, 7, 2] = 5;
            Qijt[7, 8, 2] = 0;
            Qijt[7, 9, 2] = 3;

            Qijt[8, 0, 2] = 14;
            Qijt[8, 1, 2] = 34;
            Qijt[8, 2, 2] = 3;
            Qijt[8, 3, 2] = 2;
            Qijt[8, 4, 2] = 3;
            Qijt[8, 5, 2] = 3;
            Qijt[8, 6, 2] = 1;
            Qijt[8, 7, 2] = 13;
            Qijt[8, 8, 2] = 0;
            Qijt[8, 9, 2] = 4;

            Qijt[9, 0, 2] = 26;
            Qijt[9, 1, 2] = 34;
            Qijt[9, 2, 2] = 11;
            Qijt[9, 3, 2] = 5;
            Qijt[9, 4, 2] = 3;
            Qijt[9, 5, 2] = 3;
            Qijt[9, 6, 2] = 3;
            Qijt[9, 7, 2] = 5;
            Qijt[9, 8, 2] = 2;
            Qijt[9, 9, 2] = 3;

            ///////////////////////////////////

            Qijt[0, 0, 3] = 0;
            Qijt[0, 1, 3] = 0;
            Qijt[0, 2, 3] = 13;
            Qijt[0, 3, 3] = 0;
            Qijt[0, 4, 3] = 13;
            Qijt[0, 5, 3] = 13;
            Qijt[0, 6, 3] = 13;
            Qijt[0, 7, 3] = 0;
            Qijt[0, 8, 3] = 0;
            Qijt[0, 9, 3] = 0;

            Qijt[1, 0, 3] = 31;
            Qijt[1, 1, 3] = 14;
            Qijt[1, 2, 3] = 26;
            Qijt[1, 3, 3] = 23;
            Qijt[1, 4, 3] = 21;
            Qijt[1, 5, 3] = 8;
            Qijt[1, 6, 3] = 3;
            Qijt[1, 7, 3] = 1;
            Qijt[1, 8, 3] = 10;
            Qijt[1, 9, 3] = 8;

            Qijt[2, 0, 3] = 3;
            Qijt[2, 1, 3] = 4;
            Qijt[2, 2, 3] = 9;
            Qijt[2, 3, 3] = 4;
            Qijt[2, 4, 3] = 4;
            Qijt[2, 5, 3] = 2;
            Qijt[2, 6, 3] = 5;
            Qijt[2, 7, 3] = 4;
            Qijt[2, 8, 3] = 1;
            Qijt[2, 9, 3] = 4;

            Qijt[3, 0, 3] = 13;
            Qijt[3, 1, 3] = 15;
            Qijt[3, 2, 3] = 12;
            Qijt[3, 3, 3] = 5;
            Qijt[3, 4, 3] = 13;
            Qijt[3, 5, 3] = 17;
            Qijt[3, 6, 3] = 3;
            Qijt[3, 7, 3] = 8;
            Qijt[3, 8, 3] = 3;
            Qijt[3, 9, 3] = 7;

            Qijt[4, 0, 3] = 13;
            Qijt[4, 1, 3] = 15;
            Qijt[4, 2, 3] = 12;
            Qijt[4, 3, 3] = 5;
            Qijt[4, 4, 3] = 13;
            Qijt[4, 5, 3] = 17;
            Qijt[4, 6, 3] = 3;
            Qijt[4, 7, 3] = 8;
            Qijt[4, 8, 3] = 3;
            Qijt[4, 9, 3] = 7;

            Qijt[5, 0, 3] = 4;
            Qijt[5, 1, 3] = 4;
            Qijt[5, 2, 3] = 8;
            Qijt[5, 3, 3] = 4;
            Qijt[5, 4, 3] = 21;
            Qijt[5, 5, 3] = 4;
            Qijt[5, 6, 3] = 0;
            Qijt[5, 7, 3] = 0;
            Qijt[5, 8, 3] = 4;
            Qijt[5, 9, 3] = 0;

            Qijt[6, 0, 3] = 4;
            Qijt[6, 1, 3] = 6;
            Qijt[6, 2, 3] = 17;
            Qijt[6, 3, 3] = 3;
            Qijt[6, 4, 3] = 13;
            Qijt[6, 5, 3] = 0;
            Qijt[6, 6, 3] = 4;
            Qijt[6, 7, 3] = 4;
            Qijt[6, 8, 3] = 5;
            Qijt[6, 9, 3] = 3;

            Qijt[7, 0, 3] = 9;
            Qijt[7, 1, 3] = 0;
            Qijt[7, 2, 3] = 14;
            Qijt[7, 3, 3] = 2;
            Qijt[7, 4, 3] = 0;
            Qijt[7, 5, 3] = 0;
            Qijt[7, 6, 3] = 3;
            Qijt[7, 7, 3] = 0;
            Qijt[7, 8, 3] = 0;
            Qijt[7, 9, 3] = 0;

            Qijt[8, 0, 3] = 13;
            Qijt[8, 1, 3] = 2;
            Qijt[8, 2, 3] = 4;
            Qijt[8, 3, 3] = 1;
            Qijt[8, 4, 3] = 4;
            Qijt[8, 5, 3] = 3;
            Qijt[8, 6, 3] = 1;
            Qijt[8, 7, 3] = 7;
            Qijt[8, 8, 3] = 2;
            Qijt[8, 9, 3] = 1;

            Qijt[9, 0, 3] = 18;
            Qijt[9, 1, 3] = 6;
            Qijt[9, 2, 3] = 14;
            Qijt[9, 3, 3] = 9;
            Qijt[9, 4, 3] = 10;
            Qijt[9, 5, 3] = 6;
            Qijt[9, 6, 3] = 3;
            Qijt[9, 7, 3] = 9;
            Qijt[9, 8, 3] = 8;
            Qijt[9, 9, 3] = 5;

            //////////////////////////////////////////////////////////////////////////////////






















            //profit = MainMaximizer.CalcProfit(fijt, Qijt, pri, unc,cu, cs,ctf,ctv,distj,trange,chf, chv,B);

            double totalProfitRuns = 0;

            for(int i = 0; i < totalRuns; i++)
            {
                totalProfitRuns = MainMaximizer.Optimize(fijt, Qijt, pri, unc, cu, cs, ctf, ctv, distj, trange, chf, chv, B, n, e_e, b, ne, nb);
                if(profit < totalProfitRuns)
                {
                    profit = totalProfitRuns;
                }
            }

            txtTime.Text = profit.ToString();
        }
    }
}
