using System;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace Milionario
{
    /// <summary>
    /// Logica di interazione per CTimer.xaml
    /// </summary>
    public partial class CTimer : UserControl
    {
        public DispatcherTimer Timer = new DispatcherTimer();
        int t;

        //Dichiaro un delegato da usare come "tipo" per l'evento IsZero
        public delegate void IsZeroEventHandler(object sender, EventArgs e);

        //Dichiaro un nuovo evento che potrà essere gestito dalla MainWindow (con la notazione +=)
        public event IsZeroEventHandler IsZero;

        public CTimer()
        {
            InitializeComponent();
            Timer.Interval = new System.TimeSpan(0, 0, 1);
            Timer.Tick += Timer_Tick;
            lblTimer.Background = Brushes.Green;
        }

        public void Start()
        {
            t = 60;
            Timer.Start();
        }

        public void Stop()
        {
            Timer.Stop();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            lblTimer.Content = t.ToString();

            if (t < 20)
                lblTimer.Background = Brushes.Red;
            else
                lblTimer.Background = Brushes.Green;

            if (t == 0)
            {
                Timer.Stop();
                if (IsZero != null) IsZero(this, e);    //Notifica l'evento
            }

            t--;
        }
    }
}