using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
            
            timer = new System.Windows.Threading.DispatcherTimer();
            InitializeComponent();
          
            timer.Tick += new EventHandler(timertick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }


        public void updateList() { 
            ac = new AC();
            var r = ac.Messages.Select(x => x.nickName+": \n"+x.mes).ToList();
            MessageList.ItemsSource = r;
            ((INotifyCollectionChanged)MessageList.Items).CollectionChanged += Items_CollectionChanged;
        }

        private void Items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Border border = (Border)VisualTreeHelper.GetChild(MessageList, 0);
            ScrollViewer scrollViewer = (ScrollViewer)VisualTreeHelper.GetChild(border, 0);
            scrollViewer.ScrollToBottom();
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
            if (NickText.Text != null && NickText.Text.Length <= 20)
            {
                Class1.IsLogin = 1;
                Class1.LoginName = NickText.Text;
                Online online = new Online(NickText.Text, 0);
                ac.Onlines.Add(online);
                ac.SaveChanges();
                MainNickNameLabel.Content = Class1.LoginName;
                NickNameGrid.Visibility = Visibility.Hidden;
                MainGrid.Visibility = Visibility.Visible;
                
                updateList();
            }
            else {
                MessageBox.Show("НикНейм слишком длинный!");
            }
        }

        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            var w = ac.Onlines.Where(x=> x.nickName == Class1.LoginName).FirstOrDefault();
            if (MessageText.Text != null && MessageText.Text.Length <= 100 && w.IsBanned == 0)
            {
                string nick = Class1.LoginName;
                Message message = new Message(nick, MessageText.Text);
                ac.Messages.Add(message);
                ac.SaveChanges();
                MessageText.Text = "";
                updateList();
            }
            else if(w.IsBanned == 1) {
                MessageBox.Show("Забанен!");
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            var r = ac.Onlines.Where(x => x.nickName == Class1.LoginName).FirstOrDefault();
            ac.Onlines.Remove(r);
            ac.SaveChanges();
            this.Close();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void Window_Closed(object sender, EventArgs e)
        {
            try
            {
                var r = ac.Onlines.Where(x => x.nickName == Class1.LoginName).FirstOrDefault();
                ac.Onlines.Remove(r);
                ac.SaveChanges();
                this.Close();
            }
            catch { }
        }

        private void AdminPanelButton_Click(object sender, RoutedEventArgs e)
        {
            if (AdminGrid.Visibility == Visibility.Visible)
            {
                AdminGrid.Visibility = Visibility.Hidden;
            }
            else { 
                AdminGrid.Visibility = Visibility.Visible;
            }
        }

        private void GoToAdminPanel_Click(object sender, RoutedEventArgs e)
        {
            
            try
            {
                string pass = PasswordText.Password.Trim(); 
                if (pass.Length <= 50)
                {
                    string[] vs = pass.Split('#');
                    string nwda = "";
                    for (int i = 0; i < vs.Length; i++)
                    {
                        nwda = nwda + vs[i];
                    }
                    var r = ac.Admins.Where(x => x.pass == nwda).FirstOrDefault();
                    if (r != null)
                    {
                        pass = "";
                        AdminWindow adminWindow = new AdminWindow();
                        adminWindow.Show();
                        PasswordText.Password = "";
                    }
                }
                else {
                    MessageBox.Show("Не верно");
                }
            }
            catch {
                MessageBox.Show("Не верно");
            }
        }
    }
}
