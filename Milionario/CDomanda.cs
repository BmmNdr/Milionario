namespace Milionario
{
    internal class CDomanda
    {
        public string Domanda { get; set; }
        public string[] Risposte { get; set; } = new string[4]; //risposte[0] == risposta corretta
        public int Difficolta { get; set; }

        public CDomanda(string line)
        {
            FromCSV(line);
        }

        public void FromCSV(string line)
        {
            string[] campi = line.Split(";");

            if(campi.Length >= 6)
            {
                Difficolta = int.Parse(campi[0]);
                Domanda = campi[1];

                for(int i = 2; i < campi.Length; i++)
                    Risposte[i - 2] = campi[i];
            }
        }
    }
}
