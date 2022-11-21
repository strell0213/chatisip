using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        AC ac;
        System.Windows.Threading.DispatcherTimer timer;
        public AdminWindow()
        {
            ac = new AC();
            InitializeComponent();
            timer = new System.Windows.Threading.DispatcherTimer();
            timer.Tick += new EventHandler(timertick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();

        }

        private void timertick(object sender, EventArgs e)
        {
            updateonlinelist();
        }

        public void updateonlinelist()
        {
            var r = ac.Onlines.Select(x => x.nickName).ToList();
            if (r != null)
                UsersList.ItemsSource = r;
        }
        private void BanButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var w = ac.Onlines.Where(x => x.nickName == UsersList.SelectedItem.ToString()).FirstOrDefault();
                if (w != null)
                {
                    w.IsBanned = 1;
                    ac.SaveChanges();
                    var rt = ac.Onlines.Where(x => x.nickName == UsersList.SelectedItem.ToString()).FirstOrDefault();
                }
                else
                {

                }
            }
            catch { }
        }

        private void PoslatNahuyButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string nick;
                var w = ac.Onlines.Where(x => x.nickName == UsersList.SelectedItem.ToString()).FirstOrDefault();
                if (w != null)
                { 
                    nick = UsersList.SelectedItem.ToString() + ", админ думает вас забанить...";
                    var t = ac.Messages.Where(x => x.nickName == "Bot").FirstOrDefault();
                    if (t != null)
                        ac.Messages.Remove(t);
                    ac.SaveChanges();
                   
                    Message message = new Message("Bot", nick);
                    ac.Messages.Add(message);
                    ac.SaveChanges();
                }
                else
                {

                }
            }
            catch { }

        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            int[] sc = ac.Messages.Select(x => x.ID).ToArray();
            int da;
            for (int i = 0; i < sc.Length; i++) { 
                da = sc[i];
                var r = ac.Messages.Where(x => x.ID == da).FirstOrDefault();
                if (r != null)
                {
                    ac.Messages.Remove(r);
                    ac.SaveChanges();
                }
            }

        }
    }
}
