using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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

namespace WpfApp
{
    /// <summary>
    /// Логика взаимодействия для MinStud.xaml
    /// </summary>
    public partial class MinStud : Window
    {
        public int num;
        public int id_f;
        public MinStud()
        {
            InitializeComponent();
            using (SqlConnection connection = new SqlConnection("data source=ngknn.ru;initial catalog=33П-КР-Кулюкина;persist security info=True;user id=33П;Password=12357"))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("SELECT max(ID_FAKULTET) FROM FACULTET ", connection);
                int n = Convert.ToInt32(command.ExecuteScalar().ToString());
                int[] mass = new int[n];
                for (int i = 0; i < n; i++)
                {
                    SqlCommand commandd = new SqlCommand("SELECT Count(ID_STUDENT) FROM Student  WHERE ID_FAKULTET=" + (i + 1) + "", connection);
                    if (commandd.ExecuteScalar() is null)
                    {

                    }
                    else
                    {                        
                        mass[i] = Convert.ToInt32(commandd.ExecuteScalar().ToString());
                    }

                }
                int endid = 0;
                SqlCommand commanddd = new SqlCommand("SELECT min(CountStudent) FROM FACULTET", connection);
                endid = Convert.ToInt32(commanddd.ExecuteScalar().ToString());
                for (int i = 0; i <n; i++)
                {
                   
                    if (mass[i]!=0)
                    {

                            if (endid == mass[i])
                            {
                                num = mass[i];
                                id_f = i + 1;
                            }
                    }
                }
                List<Students> minStudentList = new List<Students>
                {

                };
                minStudentList.Add(new Students { Numder = num, ID_FAKULTET = id_f });

                MinStudentG.ItemsSource = minStudentList;
            }
        }
        internal class Students
        {
            public int ID_FAKULTET { get; set; }
            public int Numder { get; set; }

        }
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

    }
}
