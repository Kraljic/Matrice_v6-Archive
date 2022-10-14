using System;
using System.Windows.Forms;

namespace Matrice_v2
{
    public class Matrica_3x3
    {
        private float[,] Matrica = new float[3, 3];

        public Matrica_3x3()
        {
            jedinicnaMatrica();
        }

        public Matrica_3x3(double angle)
        {
            jedinicnaMatrica();

            angle = (angle / 180) * Math.PI;

            this.Matrica[0, 0] = (float)Math.Round(Math.Cos(angle), 8);
            this.Matrica[0, 1] = (float)Math.Round(-Math.Sin(angle), 8);
            this.Matrica[1, 0] = (float)Math.Round(Math.Sin(angle), 8);
            this.Matrica[1, 1] = (float)Math.Round(Math.Cos(angle), 8);
        }

        public Matrica_3x3(double alfa, double beta)
        {
            jedinicnaMatrica();

            alfa = (alfa / 180) * Math.PI;
            beta = (beta / 180) * Math.PI;

            this.Matrica[0, 1] = (float)Math.Round(Math.Tan(alfa), 8);
            this.Matrica[1, 0] = (float)Math.Round(Math.Tan(beta), 8);
        }

        private void jedinicnaMatrica()
        {
            for (int redak = 0; redak < 3; redak++)
                for (int j = 0; j < 3; j++)
                    Matrica[redak, j] = (float)Convert.ToDouble(redak == j);
        }

        public override string ToString()
        {
            string matrica = "";
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                    matrica += Matrica[i, j] + "\t";
                matrica += "\n";
            }
            return matrica;
        }

        public float this[int row, int col]
        {
            get
            {
                return Matrica[row, col];
            }
            set
            {
                Matrica[row, col] = value;
            }
        }
    }
}
