using System;
using System.Windows;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;

namespace WpfControlLibrary1
{

    public class Tab
    {
        public string Name { get; set; }
        public WebBrowser browser { get; set; }
        public Tab(Uri uri)
        {
            browser = new WebBrowser() { Source = uri };
            Name = uri.AbsoluteUri;
        }

    }
    public partial class UserControl1 : UserControl
    {
        string homeUrl;
        ObservableCollection<Tab> webTabs = new ObservableCollection<Tab>();
        public UserControl1()
        {
            homeUrl = "https://www.google.com/";
            InitializeComponent();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            var uri = new Uri(Query.Text, UriKind.RelativeOrAbsolute);
            if (uri.IsAbsoluteUri)
            {
                if (Tabs.SelectedItem != null)
                {
                    ((Tab)Tabs.SelectedItem).browser.Navigate(uri);
                    ((Tab)Tabs.SelectedItem).Name=uri.AbsoluteUri;
                }
                else
                {
                    webTabs.Add(new Tab(uri));
                    Tabs.ItemsSource = webTabs;
                    Tabs.SelectedIndex = webTabs.Count - 1;
                    gg.Children.Clear();
                    gg.Children.Add((webTabs.Last()).browser);
                }
            }
            else
            {

            }
        }

        private void AddTab_Click(object sender, RoutedEventArgs e)
        {
            webTabs.Add(new Tab(new Uri(homeUrl)));
            Tabs.ItemsSource = webTabs;
            Tabs.SelectedIndex = webTabs.Count - 1;
            gg.Children.Clear();
            gg.Children.Add((webTabs.Last()).browser);
        }

        private void GoHomeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Tabs.SelectedItem != null)
            {
                ((Tab)Tabs.SelectedItem).browser.Navigate(homeUrl);
            }
        }

        private void RefreshBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Tabs.SelectedItem != null)
            {
                ((Tab)Tabs.SelectedItem).browser.Refresh();
            }
        }

        private void GoForwardBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Tabs.SelectedItem != null)
            {
                if (((Tab)Tabs.SelectedItem).browser.CanGoForward) ((Tab)Tabs.SelectedItem).browser.GoForward();
                
            }
        }

        private void GoBackBtn_Click(object sender, RoutedEventArgs e)
        {
            if (Tabs.SelectedItem != null)
            {
                if (((Tab)Tabs.SelectedItem).browser.CanGoBack) ((Tab)Tabs.SelectedItem).browser.GoBack();

            }
        }

        private void Tabs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            gg.Children.Clear();
            gg.Children.Add(((Tab)Tabs.SelectedItem).browser);
        }
    }
}
