using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace Matrice_v2
{
    public partial class Form1 : Form
    {
        List<Tocka[]> linije = new List<Tocka[]>();
        List<Tocka[]> l2 = new List<Tocka[]>();
        List<Tocka[]> l3 = new List<Tocka[]>();
        List<Tocka[]> l4 = new List<Tocka[]>();
        List<Tocka[]> lRotacija = new List<Tocka[]>();
        List<Tocka[]> lSkalacija = new List<Tocka[]>();

        bool draw = false;
        Point S, E;
        bool RUN = true;
        Pen pen2 = new Pen(Color.Black, 2);
        Pen pen2red = new Pen(Color.Red, 2);
        Pen pen2yellow = new Pen(Color.Yellow, 2);
        Pen pen2green = new Pen(Color.Green, 2);
        Pen pen2blue = new Pen(Color.Blue, 2);
        Pen pen2orange = new Pen(Color.Orange, 2);
        Pen pen7 = new Pen(Color.DarkGray, 5);

        int W;
        int H;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Show();

            txt2_00.Focus();
            izracunajMatricuRotacije();
            izracunajMatricuSkalacije();

            Bitmap bmp;

            while (RUN)
            {
                this.W = pbImage1.Width;
                this.H = pbImage1.Height;

                if (W > 0 || H > 0)
                {
                    bmp = new Bitmap(W, H);
                    using (var g = Graphics.FromImage(bmp))
                    {
                        Draw(g);
                        pbImage1.Image = bmp;
                    }
                }

                GC.Collect();
                Application.DoEvents();
                Thread.Sleep(50);
            }
        }
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            RUN = false;
        }

        private void pbImage1_MouseDown(object sender, MouseEventArgs e)
        {
            draw = true;
            S = e.Location;
            S.X -= W / 2;
            S.Y = (H + S.Y * -1) - H / 2;
        }
        private void pbImage1_MouseUp(object sender, MouseEventArgs e)
        {
            if (draw)
            {
                draw = false;

                E = e.Location;

                // Pomakni za prvi kvadrant
                E.X -= W / 2;
                E.Y = (H + E.Y * -1) - H / 2;

                Tocka[] line = { new Tocka(S), new Tocka(E) };
                linije.Add(line);
            }
        }

        private void pbImage1_MouseMove(object sender, MouseEventArgs e)
        {
            E = e.Location;
            E.X -= W / 2;
            E.Y = (H + E.Y * -1) - H / 2;
        }

        public void Draw(Graphics g)
        {
            // Nacrtaj osi
            g.DrawLine(pen7, W / 2, 0, W / 2, H);
            g.DrawLine(pen7, 0, H / 2, W, H / 2);

            // Nacrtaj koordinate
            for (int i = -20; i < 21; i++)
                g.DrawString((i * 50) + "", DefaultFont, Brushes.Black,
                    W / 2 + 50 * i, -1 + H / 2);
            for (int i = -20; i < 21; i++)
                g.DrawString((i * -50) + "", DefaultFont, Brushes.Black,
                    W / 2, -1 + H / 2 + 50 * i);

            // Nacrtaj orginalne tocke
            foreach (Tocka[] line in linije)
                g.DrawLine(pen2, line[0].X + W / 2, line[0].Y * -1 + H / 2,
                    line[1].X + W / 2, line[1].Y * -1 + H / 2);

            // Nacrtaj crvene tocke ako je uključen prikaz
            if (cb2.Checked)
                foreach (Tocka[] line in l2)
                    g.DrawLine(pen2red, line[0].X + W / 2, line[0].Y * -1 + H / 2,
                        line[1].X + W / 2, line[1].Y * -1 + H / 2);

            // Nacrtaj zute tocke ako je uključen prikaz
            if (cb3.Checked)
                foreach (Tocka[] line in l3)
                    g.DrawLine(pen2yellow, line[0].X + W / 2, line[0].Y * -1 + H / 2,
                        line[1].X + W / 2, line[1].Y * -1 + H / 2);

            // Nacrtaj zelene tocke ako je uključen prikaz
            if (cb4.Checked)
                foreach (Tocka[] line in l4)
                    g.DrawLine(pen2green, line[0].X + W / 2, line[0].Y * -1 + H / 2,
                        line[1].X + W / 2, line[1].Y * -1 + H / 2);

            // Nacrtaj plave tocke ako je uključen prikaz (matrica rotacije)
            if (cb5.Checked)
                foreach (Tocka[] line in lRotacija)
                    g.DrawLine(pen2blue, line[0].X + W / 2, line[0].Y * -1 + H / 2,
                        line[1].X + W / 2, line[1].Y * -1 + H / 2);

            // Nacrtaj narandjaste tocke ako je uključen prikaz (matrica skalacije)
            if (cb6.Checked)
                foreach (Tocka[] line in lSkalacija)
                    g.DrawLine(pen2orange, line[0].X + W / 2, line[0].Y * -1 + H / 2,
                        line[1].X + W / 2, line[1].Y * -1 + H / 2);

            // Ako korisnik unosi novu liniju
            if (draw)
                g.DrawLine(pen2, S.X + W / 2, S.Y * -1 + H / 2, E.X + W / 2,
                    E.Y * -1 + H / 2);
        }

        private void btnPomnozi_Click(object sender, EventArgs e)
        {
            try
            {
                Matrica_3x3[] matrice = getMatrice();
                l2 = Transformacije.PomnoziLinije(matrice[1], linije);
                l3 = Transformacije.PomnoziLinije(matrice[2], linije);
                l4 = Transformacije.PomnoziLinije(matrice[3], linije);
                lRotacija = Transformacije.PomnoziLinije(matrice[4], linije);
                lSkalacija = Transformacije.PomnoziLinije(matrice[5], linije);
            }
            catch (Exception)
            {
                MessageBox.Show("Sva polja u matricama moraju biti ispravno popunjena.", "Greška", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnClear_Click(object sender, EventArgs e)
        {
            linije.Clear();
            l2.Clear();
            l3.Clear();
            l4.Clear();
            lRotacija.Clear();
            lSkalacija.Clear();
        }
        public Matrica_3x3[] getMatrice()
        {
            Matrica_3x3[] matrice = new Matrica_3x3[6];
            matrice[0] = objectToMatrica(tlp1);
            matrice[1] = objectToMatrica(tlp2);
            matrice[2] = objectToMatrica(tlp3);
            matrice[3] = objectToMatrica(tlp4);
            matrice[4] = objectToMatrica(tlpRotacija);
            matrice[5] = objectToMatrica(tlpSmicanje);
            return matrice;
        }


        public Matrica_3x3 objectToMatrica(object matricaObject)
        {
            TableLayoutPanel matricaPanel = matricaObject as TableLayoutPanel;
            Matrica_3x3 matrica = new Matrica_3x3();
            foreach (TextBox element in matricaPanel.Controls)
            {
                int row, col;
                float val = (float)Convert.ToDouble(element.Text.Replace('.', ','));
                row = Convert.ToInt32(element.Name[5]) - 48;
                col = Convert.ToInt32(element.Name[6]) - 48;

                matrica[row, col] = val;
            }

            return matrica;
        }

        private void numAngle_ValueChanged(object sender, EventArgs e)
        {
            izracunajMatricuRotacije();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void numAngleTranslacija_ValueChanged(object sender, EventArgs e)
        {
            izracunajMatricuSkalacije();
        }

        public void izracunajMatricuRotacije()
        {
            float angle = (float)numAngleRotation.Value;
            Matrica_3x3 rotacija = new Matrica_3x3(angle);

            txt5_00.Text = rotacija[0, 0].ToString();
            txt5_01.Text = rotacija[0, 1].ToString();
            txt5_10.Text = rotacija[1, 0].ToString();
            txt5_11.Text = rotacija[1, 1].ToString();
        }

        public void izracunajMatricuSkalacije()
        {
            float angleAlfa = (float)numAngleAlfa.Value;
            float anglBeta = (float)numAngleBeta.Value;
            Matrica_3x3 skalacija = new Matrica_3x3(angleAlfa, anglBeta);

            txt6_01.Text = skalacija[0, 1].ToString();
            txt6_10.Text = skalacija[1, 0].ToString();
        }
    }
}
