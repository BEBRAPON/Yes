using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;

namespace Yes
{
    public partial class MainWindow : Window
    {
        List<jsonRead> obj = JsonConvert.DeserializeObject<List<jsonRead>>(File.ReadAllText("C:\\Users\\kosty\\source\\repos\\Yes\\notes.json"));
        List<Note> notesInf = new List<Note>();
        public List<T> removeDuplFromTypes<T>(List<T> list)
        {
            return new HashSet<T>(list).ToList();
        }
        public MainWindow()
        {
            InitializeComponent();

            jsonCheck();
            loadInfo();
            cbFromFile();
            cbRef();
        }

        private void add_newType_Click(object sender, RoutedEventArgs e)
        {
            TypeKeeping.date = rec_date.Text;
            TypeKeeping.name = rec_name.Text;
            TypeKeeping.sum = rec_cash.Text;

            TypeAddWin win = new TypeAddWin();
            win.Show();
            Close();
        }


        private void add_rec_Click(object sender, RoutedEventArgs e)
        {
            if (rec_name.Text != "" && rec_cash.Text != "" && rec_type.SelectedValue != null && rec_date.SelectedDate != null)
            {
                if (Convert.ToInt32(rec_cash.Text) == 0)
                {
                    MessageBox.Show("Поле с суммой денег не может принимать значение '0'");
                }
                else
                {
                    bool check;
                    string money;
                    if (Convert.ToInt32(rec_cash.Text) < 0)
                        check = false;
                    else
                        check = true;
                    money = rec_cash.Text;
                    if (rec_cash.Text[0] == '-')
                        money = rec_cash.Text.Substring(1);
                    string dat;
                    dat = rec_date.Text;
                    obj.Add(new jsonRead { name = rec_name.Text, type = (string) rec_type.SelectedValue, cash = Convert.ToInt32(money), cashInf = check, date = dat });
                    gridAd();
                }
            }
        }


        private void dateSel(object sender, SelectionChangedEventArgs e)
        {
            gridAd();
        }

        private void del_type_Click(object sender, RoutedEventArgs e)
        {
            if (notes.SelectedItems != null)
            {
                Note selected = notes.SelectedItems as Note; 
                notesInf.Remove(selected);

                for (var i = 0; i < obj.Count; i++)
                {
                    if (obj[i].date == rec_date.Text)
                    {
                        obj.RemoveAt(i);
                    }
                }
                gridAd();
            }
        }


        private void notes_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }







        private void jsonCheck()
        {
            if (obj == null)
            {
                obj = new List<jsonRead>();
            }
        }
        private void loadInfo()
        {
            rec_date.Text = TypeKeeping.date;
            rec_name.Text = TypeKeeping.name;
            rec_cash.Text = TypeKeeping.sum;
        }
        private void cbRef()
        {
            if (TypeKeeping.types != null)
            {
                foreach (var item in TypeKeeping.types)
                {
                    rec_type.Items.Add(item);
                }
            }
            rec_type.Items.Refresh();
        }
        private void gridAd()
        {
            notes.ItemsSource = notesInf;
            notesInf.Clear();
            int sum = 0;
            if (obj != null || rec_date.Text.ToString() != "")
            {
                foreach (var item in obj)
                {
                    if (rec_date.Text.ToString() == item.date.ToString())
                        notesInf.Add(new Note { name = item.name, type = item.type, cash = item.cash, cashInf = item.cashInf });

                    if (item.cashInf == true)
                    {
                        sum += item.cash;
                    }
                    else
                    {
                        sum -= item.cash;
                    }
                }
                all_sum.Text = Convert.ToString(sum);
                notes.Items.Refresh();
            }
        }
        private void cbFromFile()
        {
            rec_type.Items.Clear();
            if (obj != null)
            {
                List<string> types = new List<string>();
                foreach (var item in obj)
                {
                    types.Add(item.type);
                }
                List<string> typess = removeDuplFromTypes(types);
                foreach (var item in typess)
                {
                    rec_type.Items.Add(item);
                }
            }
        }
        private void Close(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string json = JsonConvert.SerializeObject(obj);
            File.WriteAllText("C:\\Users\\kosty\\source\\repos\\Yes\\notes.json", json);
        }
    }
}
