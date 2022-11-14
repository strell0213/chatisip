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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        AC ac;
        System.Windows.Threading.DispatcherTimer timer;
        public MainWindow()
        {
            ac = new AC();
            timer = new System.Windows.Threading.DispatcherTimer();
            InitializeComponent();
            timer.Tick += new EventHandler(timertick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }

       

        public void updateList() { 
            var r = ac.Messages.Select(x => x.nickName+": \n"+x.mes).ToList();
            MessageList.ItemsSource = r;
        }
        public void updateonline() {
            var r = ac.Onlines.Count();
            OnlineLabel.Content = "Онлайн: " + r;
        }
        private void timertick(object sender, EventArgs e)
        {
            updateList();
            updateonline();
        }
        private void GoButton_Click(object sender, RoutedEventArgs e)
        {
            if (NickText.Text != null)
            {
                Class1.IsLogin = 1;
                Class1.LoginName = NickText.Text;
                Online online = new Online(NickText.Text);
                ac.Onlines.Add(online);
                ac.SaveChanges();
                MainNickNameLabel.Content = Class1.LoginName;
                NickNameGrid.Visibility = Visibility.Hidden;
                MainGrid.Visibility = Visibility.Visible;
                
                updateList();
            }
            else { 
            }
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            if (MessageText != null)
            {
                string nick = Class1.LoginName;
                Message message = new Message(nick, MessageText.Text);
                ac.Messages.Add(message);
                ac.SaveChanges();
                updateList();
            }
            else { 
            
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            var r = ac.Onlines.Where(x => x.nickName == Class1.LoginName).FirstOrDefault();
            ac.Onlines.Remove(r);
            ac.SaveChanges();
            this.Close();
        }
    }
}
