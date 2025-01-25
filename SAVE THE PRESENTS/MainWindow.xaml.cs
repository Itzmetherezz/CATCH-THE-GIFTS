using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SAVE_THE_PRESENTS
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        int MaxItems = 4;
        int CureentItems = 0;
        List<Rectangle> itemsToRemove = new List<Rectangle>();

        Random rand = new Random();
        int score = 0;
        int missed = 0;
        Rect PlayerHitbox;
        DispatcherTimer GameTimer = new DispatcherTimer();
        ImageBrush PlayerImage = new ImageBrush();
        ImageBrush Background = new ImageBrush();

        private int currentTtems;
        private object player1;

        public MainWindow()
        {
            InitializeComponent();
            mycanvas.Focus();
            GameTimer.Tick += GameEngine;
            GameTimer.Interval = TimeSpan.FromMilliseconds(20);
            GameTimer.Start();
            PlayerImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/ASSETS/netLeft.png"));
            player.Fill = PlayerImage;


        }

        private void GameEngine(object? sender, EventArgs e)
        {

            
        
            scoretext.Content = "Caught: " + score;
            missedText.Content = "Missed: " + missed;

            if (currentTtems < MaxItems)
            {
                MakeGift();
                currentTtems++;
            }

            itemsToRemove.Clear();  

            foreach (var x in mycanvas.Children.OfType<Rectangle>())
            {
                if ((string)x.Tag == "drops")
                {
                    Canvas.SetTop(x, Canvas.GetTop(x) + 10);

                    if (Canvas.GetTop(x) > 720) 
                    {
                        itemsToRemove.Add(x); 
                        currentTtems--;
                        missed++; 
                    }

                    Rect presentHitBox = new Rect(Canvas.GetLeft(x), Canvas.GetTop(x), x.Width, x.Height);
                    PlayerHitbox = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.Width, player.Height);

                    if (PlayerHitbox.IntersectsWith(presentHitBox)) 
                    {
                        itemsToRemove.Add(x); 
                        currentTtems--; 
                        score++; 
                    }
                }
            }

            foreach (var i in itemsToRemove) 
            {
                mycanvas.Children.Remove(i);
            }

            if (missed > 5) 
            {
                GameTimer.Stop();
                MessageBox.Show("You Lost!" + Environment.NewLine + "You Scored: " + score + Environment.NewLine + "Click OK to play again");
                Reset();
            }
        

            
            
            


        }

        private void mycanvas_MouseMove(object sender, MouseEventArgs e)
        {

            Point position = e.GetPosition(this);
            double pX = position.X;
            Canvas.SetLeft(player, pX - 10);
            if (Canvas.GetLeft(player) < 260)
            {
                PlayerImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/ASSETS/netLeft.png"));

            }
            else
            {

                PlayerImage.ImageSource = new BitmapImage(new Uri("pack://application:,,,/ASSETS/netRight.png"));

            }



        }
        private void Reset()
        {



            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
            Environment.Exit(0);

        }
        private void MakeGift()
        {

            ImageBrush gifts = new ImageBrush();
            int i = rand.Next(1, 6);
            switch (i)
            {
                case 1:
                    gifts.ImageSource = new BitmapImage(new Uri("pack://application:,,,/ASSETS/present_01.png"));
                    break;
                case 2:
                    gifts.ImageSource = new BitmapImage(new Uri("pack://application:,,,/ASSETS/present_02.png"));
                    break;
                case 3:
                    gifts.ImageSource = new BitmapImage(new Uri("pack://application:,,,/ASSETS/present_03.png"));
                    break;
                case 4:
                    gifts.ImageSource = new BitmapImage(new Uri("pack://application:,,,/ASSETS/present_04.png"));
                    break;
                case 5:
                    gifts.ImageSource = new BitmapImage(new Uri("pack://application:,,,/ASSETS/present_05.png"));
                    break;
                case 6:
                    gifts.ImageSource = new BitmapImage(new Uri("pack://application:,,,/ASSETS/present_06.png"));
                    break;


            }
            Rectangle newRec = new Rectangle
            {
                Tag = "drops",
                Width = 50,
                Height = 50,
                Fill = gifts
            };
            Canvas.SetLeft(newRec, rand.Next(10, 450));
            Canvas.SetTop(newRec, rand.Next(60, 150) * -1);
            mycanvas.Children.Add(newRec);


        }
    }
}








