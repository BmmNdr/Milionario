namespace Milionario
{
    public class CPlayer
    {
        public string Nome { get; set; }
        public int Punteggio { get; set; }
        public int Difficolta { get; set; }

        public readonly static int[] punteggi = { 1000000, 300000, 150000, 70000, 30000, 20000, 15000, 10000, 7000, 5000, 3000, 2000, 1500, 1000, 500 };

        public CPlayer(string nome, int punteggio)
        {
            Nome = nome;
            Punteggio = punteggio;
            Difficolta = 0;
        }

        public CPlayer(string line)
        {
            FromCSV(line);
        }

        public void FromCSV(string line)
        {
            string[] campi = line.Split(";");

            if(campi.Length >= 2)
            {
                Nome = campi[0];
                Punteggio = int.Parse(campi[1]);
            }
        }

        public string ToCSV()
        {
            return Nome + ";" + Punteggio.ToString();
        }

        public override string ToString()
        {
            return Nome + "\t" + Punteggio.ToString();
        }

        public void NextLvl() 
        {
            if (Difficolta < punteggi.Length)
                Punteggio = punteggi[punteggi.Length - 1 - Difficolta];

            Difficolta++;
        }

    }
}
