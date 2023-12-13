using System.Collections.Generic;
using System.IO;

namespace Milionario
{
    internal class File
    {
        public static List<CPlayer> GetClassifica()
        {
            List<CPlayer> classifica = new();

            StreamReader sIN = new(@".\classifica.csv");

            while (!sIN.EndOfStream) 
                classifica.Add(new CPlayer(sIN.ReadLine()));

            sIN.Close();
            return classifica;
        }

        public static List<CDomanda>[] GetDomande()
        {
            //15 == numero di difficoltà
            List<CDomanda>[] domande = new List<CDomanda>[15];

            for(int i = 0; i < domande.Length; i++)
                domande[i] = new List<CDomanda> ();


            StreamReader sIN = new(@".\domande.csv");

            while (!sIN.EndOfStream)
            {
                CDomanda d = new(sIN.ReadLine());
                domande[d.Difficolta].Add(d);
            }

            sIN.Close();
            return domande;
        }

        public static void SaveClassifica(List<CPlayer> classifica)
        {
            StreamWriter sOUT = new(@".\classifica.csv");

            for(int i = 0; i < 10 && i < classifica.Count; i++)
                sOUT.WriteLine(classifica[i].ToCSV());

            sOUT.Close();
        }
    }
}
