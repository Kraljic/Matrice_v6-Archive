using System.Collections.Generic;

namespace Matrice_v2
{
    public class Transformacije
    {
        public static List<Tocka[]> PomnoziLinije(Matrica_3x3 matrica, List<Tocka[]> linije)
        {
            List<Tocka[]> _linije = new List<Tocka[]>();
            for (int i = 0; i < linije.Count; i++)
            {
                Tocka[] t = new Tocka[2];
                t[0] = Operacije.Pomnozi(matrica, linije[i][0]);
                t[1] = Operacije.Pomnozi(matrica, linije[i][1]);
                _linije.Add(t);
            }

            return _linije;
        }
    }
}
