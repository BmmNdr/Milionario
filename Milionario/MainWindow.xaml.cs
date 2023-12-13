using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Milionario
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MediaPlayer audioPlayer = new();
        private List<CPlayer> classifica;
        private readonly List<CDomanda>[] domande;
        private readonly Button[] btnRisposte;
        private CDomanda curDomanda;
        private CPlayer player;

        private readonly int nBgImg = 5;
        private readonly int nAudioEsatta = 3;
        private bool isGameStart = true;
        private int lastBgImg = -1;
        private int lastAudioEsatta = -1;

        public MainWindow()
        {
            InitializeComponent();
            Timer.IsZero += Timer_IsZero;
            mediaPlayer.Source = new Uri(@".\img\sigla.mov", UriKind.Relative);
            Panel.SetZIndex(mediaPlayer, 1);

            audioPlayer.MediaEnded += AudioPlayer_MediaEnded;
            audioPlayer.ScrubbingEnabled = true;

            //carico le domande
            domande = File.GetDomande();

            btnRisposte = new Button[] {btnRA, btnRB, btnRC, btnRD};
        }

        private void AudioPlayer_MediaEnded(object sender, EventArgs e)
        {
            //sottofondo musicale
            if(player == null)
                audioPlayer.Open(new Uri(@".\audio\sigla.mp3", UriKind.Relative));
            else
                audioPlayer.Open(new Uri(@".\audio\musica" + player.Difficolta.ToString() + ".mp3", UriKind.Relative));

            audioPlayer.Play();
        }

        private void Timer_IsZero(object sender, EventArgs e)
        {
            Risposta(sender, new RoutedEventArgs());
        }

        private void MediaPlayer_MediaEnded(object sender, RoutedEventArgs e)
        {
            if(isGameStart)
            {
                btnSalta.Visibility = Visibility.Hidden;
                txtDomanda.Visibility = Visibility.Hidden;
                btnRA.Visibility = Visibility.Hidden;
                btnRB.Visibility = Visibility.Hidden;
                btnRC.Visibility = Visibility.Hidden;
                btnRD.Visibility = Visibility.Hidden;
                Timer.Visibility = Visibility.Hidden;
                txtPunteggio.Visibility = Visibility.Hidden;
                mediaPlayer.Visibility = Visibility.Hidden;

                btnGioca.Visibility = Visibility.Visible;
                txtNome.Visibility = Visibility.Visible;
                lblNome.Visibility = Visibility.Visible;
                imagePlayer.Visibility = Visibility.Visible;

                lstDiff.Items.Clear();
                txtNome.Clear();
                player = null;
                Panel.SetZIndex(txtDomanda, 0);

                //cambio lo sfondo
                mediaPlayer.Source = new Uri(@".\img\logo.jpg", UriKind.Relative); //per stoppare il video
                imagePlayer.Source = new BitmapImage(new Uri(@".\img\logo.jpg", UriKind.Relative));

                //porto l'immagine sullo sfondo
                Panel.SetZIndex(mediaPlayer, -1);

                //sottofondo musicale
                AudioPlayer_MediaEnded(sender, new RoutedEventArgs());

                //carico la classifica
                classifica = File.GetClassifica();
                foreach (CPlayer p in classifica)
                    lstDiff.Items.Add(p);

                isGameStart = false;
            }
        }

        //Creo il giocatore e inizio la partita
        private void BtnGioca_Click(object sender, RoutedEventArgs e)
        {
            if (txtNome.Text.Length <= 0)
                return;

            player = new CPlayer(txtNome.Text, 0);

            //nascondo i componenti che non utilizzo
            audioPlayer.Pause();
            btnGioca.Visibility = Visibility.Hidden;
            txtNome.Visibility = Visibility.Hidden;
            lblNome.Visibility = Visibility.Hidden;

            //mostro i componenti per le domande
            txtDomanda.Visibility = Visibility.Visible;
            btnRA.Visibility = Visibility.Visible;
            btnRB.Visibility = Visibility.Visible;
            btnRC.Visibility = Visibility.Visible;
            btnRD.Visibility = Visibility.Visible;
            Timer.Visibility = Visibility.Visible;
            txtPunteggio.Visibility = Visibility.Visible;

            //carico i montepremi
            lstDiff.Items.Clear();
            lstDiff.FontSize = 10;

            foreach (int p in CPlayer.punteggi)
            {
                Label lbl = new();
                var brush = new ImageBrush
                {
                    ImageSource = new BitmapImage(new Uri("img/risposta.png", UriKind.Relative))
                };
                lbl.Content = p.ToString();
                lbl.Foreground = Brushes.Gold;
                lbl.Background = brush;
                lbl.Height = 25;
                lbl.Width = 100;
                lbl.HorizontalContentAlignment = HorizontalAlignment.Center;
                lstDiff.Items.Add(lbl);
            }

            AudioPlayer_MediaEnded(new object(), new EventArgs());
            CaricaDomanda();
        }

        private void CaricaDomanda()
        {
            if (player.Difficolta < CPlayer.punteggi.Length)
            {
                //cambio lo sfondo
                int random;
                do
                {
                    random = new Random().Next(nBgImg);
                } while (random == lastBgImg);
                lastBgImg = random;

                imagePlayer.Source = new BitmapImage(new Uri(@".\img\domanda" + lastBgImg + ".jpg", UriKind.Relative));

                //imposto lo sfondo base alle risposte
                var brush = new ImageBrush
                {
                    ImageSource = new BitmapImage(new Uri("img/risposta.png", UriKind.Relative))
                };

                foreach (Button btn in btnRisposte)
                    btn.Background = brush;

                //sposto indicatore difficoltà/montepremi
                var brushMontepremi = new ImageBrush
                {
                    ImageSource = new BitmapImage(new Uri("img/rispostaEsatta.png", UriKind.Relative))
                };
                (lstDiff.Items[CPlayer.punteggi.Length - 1 - player.Difficolta] as Label).Background = brushMontepremi;

                if (player.Difficolta > 0)
                {
                    var brushMontepremiPrec = new ImageBrush();
                    brushMontepremiPrec.ImageSource = new BitmapImage(new Uri("img/risposta.png", UriKind.Relative));
                    (lstDiff.Items[CPlayer.punteggi.Length - player.Difficolta] as Label).Background = brushMontepremiPrec;
                }

                //carico la domanda e le risposte
                curDomanda = domande[player.Difficolta][new Random().Next(domande[player.Difficolta].Count)];
                txtDomanda.Content = curDomanda.Domanda;
                Shuffle();

                for (int i = 0; i < curDomanda.Risposte.Length; i++)
                    btnRisposte[i].Content = curDomanda.Risposte[i];

                //starto il timer
                Timer.Start();
            }
            else
                EndGame(true);
        }

        //basato su Fisher-Yates shuffle
        private void Shuffle()
        {
            for (int i = 0; i < btnRisposte.Length; i++)
            {
                int j = new Random().Next(i);
                (btnRisposte[j], btnRisposte[i]) = (btnRisposte[i], btnRisposte[j]);
            }
        }

        private void Risposta(object sender, RoutedEventArgs e)
        {
            var brushEsatta = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri("img/rispostaEsatta.png", UriKind.Relative))
            };

            Timer.Stop();

            if(sender is Button && (sender as Button) == btnRisposte[0])
            {
                (sender as Button).Background = brushEsatta;

                int random;
                do
                {
                    random = new Random().Next(nAudioEsatta);
                } while (random == lastAudioEsatta);
                lastAudioEsatta = random;

                audioPlayer.Open(new Uri(@".\audio\esatta" + lastAudioEsatta + ".mp3", UriKind.Relative));
                audioPlayer.Play();

                MessageBox.Show("Risposta Esatta!");

                //aumento la difficoltà
                txtPunteggio.Text = player.Punteggio.ToString();
                player.NextLvl();

                CaricaDomanda();
            }
            else
            {
                btnRisposte[0].Background = brushEsatta;

                if (sender is Button)
                {
                    var brushErrata = new ImageBrush
                    {
                        ImageSource = new BitmapImage(new Uri("img/rispostaErrata.png", UriKind.Relative))
                    };

                    (sender as Button).Background = brushErrata;

                    MessageBox.Show("Risposta Errata!");
                }
                else
                    MessageBox.Show("Tempo Scaduto!");

                EndGame(false);
            }
        }

        private void EndGame(bool isVictory)
        {
            classifica.Add(player);
            classifica.Sort((x, y) => y.Punteggio.CompareTo(x.Punteggio));
            File.SaveClassifica(classifica);

            isGameStart = true;
            Panel.SetZIndex(mediaPlayer, 1);
            btnSalta.Visibility = Visibility.Visible;


            txtDomanda.Content = player.Punteggio.ToString();
            Panel.SetZIndex(txtDomanda, 1);

            audioPlayer.Stop();

            imagePlayer.Visibility = Visibility.Hidden;
            mediaPlayer.Visibility = Visibility.Visible;

            if (isVictory)
                mediaPlayer.Source = new Uri(@".\img\vittoria.mov", UriKind.Relative);
            else
                mediaPlayer.Source = new Uri(@".\img\errore.mov", UriKind.Relative);
        }
    }
}
