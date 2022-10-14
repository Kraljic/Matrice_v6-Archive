using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Matrice_v2
{
    public class Operacije
    {
public static Tocka Pomnozi(Matrica_3x3 matrica, Tocka tocka)
{
    var t = new Tocka();

    t[0] = matrica[0, 0] * tocka[0] + matrica[0, 1] * tocka[1]
                + matrica[0, 2] * tocka[2];
    t[1] = matrica[1, 0] * tocka[0] + matrica[1, 1] * tocka[1]
                + matrica[1, 2] * tocka[2];
    t[2] = matrica[2, 0] * tocka[0] + matrica[2, 1] * tocka[1]
                + matrica[2, 2] * tocka[2];

    return t;
}

        public static List<Tocka> PomnoziTocke(Matrica_3x3 matrica, List<Tocka> tocke)
        {
            for (int i = 0; i < tocke.Count; i++)
                tocke[i] = Pomnozi(matrica, tocke[i]);

            return tocke;
        }
    }
}
