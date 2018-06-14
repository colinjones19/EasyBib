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

namespace EasyBibClone
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string line;
        string author;
        string authorFirst;
        string authorLast;
        int authorLastPos;
        string datePublished;
        string dateAccessed;
        string articleTitle;
        string title;
        string location;
        string publisher;
        string URL = "http://www.cbc.ca/radio/quirks/canada-not-ready-for-driverless-cars-senate-report-says-1.4517064";
        string ISBN = "9780718895372";
        public bool authorFirstFound = false;
        public bool authorLastFound = false;
        public bool datePublishedFound = false;
        public bool dateAccessedFound = false;
        public bool articletitleFound = false;
        public bool titleFound = false;
        public bool locationFound = false;
        public bool publisherFound = false;
        string temp;
        string formatRule = "APA";
        string sourceType;
        System.Net.WebClient webClient = new System.Net.WebClient();

        public MainWindow()
        {
            InitializeComponent();
            ComboWombo.Width = 269;
            ComboWombo.Height = 25;


            ComboWombo.IsEditable = true;
            ComboWombo.IsReadOnly = true;
            ComboWombo.Text = "Select A Format";

            ComboWombo.Items.Add("MLA8");
            
            ComboWombo.Items.Add("APA");
            MainGrid.Children.Add(ComboWombo);
            Canvas.SetTop(ComboWombo, 213);
            Canvas.SetLeft(ComboWombo, 123);

            sourceType = "BOOK";
            if(sourceType == "BOOK")
            {
                System.IO.StreamReader streamReader = new System.IO.StreamReader(webClient.OpenRead("https://isbndb.com/search/books/" + ISBN));
                dateAccessedFound = true;
                articletitleFound = true;
                read(streamReader);
            }

            else if (sourceType == "URL")
            {
                System.IO.StreamReader streamReader = new System.IO.StreamReader(webClient.OpenRead(URL));
                read(streamReader);
            }
            output();
        }

        private void output()
        {
            if (publisher == "")
            {
                
            }

            if(formatRule == "APA")
            {
                if(sourceType == "BOOK")
                {
                    MessageBox.Show(authorLast + ", " + authorFirst + ". (" + datePublished + "). " + title + ". " + location + ": " + publisher + "." );
                }

                if(sourceType == "URL")
                {
                    MessageBox.Show(authorLast + ", " + authorFirst + ". (" + datePublished + "). " + articleTitle + ". " + title + ". Retreived from: " + URL);
                }
            }

            if(formatRule == "MLA")
            {
                if (sourceType == "BOOK")
                {
                    MessageBox.Show(authorLast + ", " + authorFirst + ". " + title + ". " + location + ", " + publisher + ", " + datePublished + ".");
                }

                if (sourceType == "URL")
                {
                    MessageBox.Show(authorLast + ", " + authorFirst + ". " + title + ". " + publisher + ", " + datePublished + ", " + URL + ". " + dateAccessed + ".");
                }
            }
        }

        private void read(System.IO.StreamReader streamReader)
        {

            while (!streamReader.EndOfStream)
            {
                line = streamReader.ReadLine();

                if (titleFound == false)
                {
                    if (sourceType == "BOOK" && line.ToUpper().Contains("FULL TITLE"))
                    {
                        title = getData(line);
                        title = title.Remove(0, 12);
                        MessageBox.Show(title);
                    }

                    if (sourceType == "URL" && line.ToUpper().Contains("TITLE"))
                    {
                        title = getData(line);
                        MessageBox.Show(title);
                    }
                    titleFound = true;
                }

                if (authorFirstFound == false || authorLastFound == false)
                {
                    if (line.ToUpper().Contains("AUTHOR"))
                    {
                        author = getData(line);

                        if (author.ToUpper().Contains("AUTHOR"))
                        {
                            continue;
                        }

                        authorFirst = author.Remove(author.IndexOf(" "));
                        if (authorFirst != " ")
                        {
                            MessageBox.Show(authorFirst);
                            authorFirstFound = true;
                        }

                        authorLast = author.Remove(0, author.IndexOf(" "));
                        if (authorLast != " ")
                        {
                            MessageBox.Show(authorLast);
                            authorLastFound = true;
                        }
                    }
                }

                if (datePublishedFound == false)
                {
                    if (line.ToUpper().Contains("PUBLISH DATE"))
                    {
                        datePublished = getData(line);

                        if (sourceType == "BOOK")
                        {
                            datePublished = datePublished.Remove(0, 14);
                        }
                        MessageBox.Show(datePublished);
                        datePublishedFound = true;
                    }
                }

                //if(articletitleFound == false)
                //{

                //}

                //if (locationFound == false)
                //{

                //}

                if(publisherFound == false)
                {
                    if (line.ToUpper().Contains("PUBLISHER"))
                    {
                        publisher = getData(line);

                        if(sourceType == "BOOK")
                        {

                        }
                        publisher = publisher.Remove(0, 10);
                        MessageBox.Show(publisher);
                        publisherFound = true;
                    }
                }

            }
            streamReader.Close();
            MessageBox.Show("AF: " + authorFirst + " AL: " + authorLast + " DP: " + datePublished + " DA: " + dateAccessed + " AT: " +  articleTitle + " T: " + title + " L: " + location + " P: " +publisher);
            output();
        }

        public string getData(string line)
        {
            temp = line;
            //Removes code text and leaves only the data we are looking for.
            while (temp.Contains("<") || temp.Contains(">"))
            {
                temp = temp.Remove(temp.IndexOf("<"), temp.IndexOf(">") - temp.IndexOf("<") + 1);
            }

            //Removes extra spaces at the beginning.
            while (temp.IndexOf(" ") == 0)
            {
                temp = temp.Remove(temp.IndexOf(" "), 1);
            }

            //Removes extra spaces at the end.
            while (temp.LastIndexOf(" ") == temp.Length - 1 && temp.Length != 0)
            {
                temp = temp.Remove(temp.Length - 1);
            }
            return temp;
            
        private void CreateCit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
           
        }
        }
    }
}
