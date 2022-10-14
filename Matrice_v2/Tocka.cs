namespace Matrice_v2
{
    public class Tocka
    {
        private float[] tocka = { 0.0F, 0.0F, 1.0F };
        public float X { get { return tocka[0]; } }
        public float Y { get { return tocka[1]; } }

        public Tocka() { }
        public Tocka(float X, float Y)
        {
            this.tocka[0] = X;
            this.tocka[1] = Y;
        }
        public Tocka(System.Drawing.Point p)
        {
            this.tocka[0] = p.X;
            this.tocka[1] = p.Y;
        }
        public float this[int row]
        {
            get
            {
                return tocka[row];
            }
            set
            {
                tocka[row] = value;
            }
        }
    }
}
