using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
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

namespace WpfApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            using (SqlConnection connection = new SqlConnection("data source=ngknn.ru;initial catalog=33П-КР-Кулюкина;persist security info=True;user id=33П;PWD=12357"))
            {
                connection.Open();
                SqlCommand command = new SqlCommand("select max(ID_Student) from Student", connection);
                int n = Convert.ToInt32(command.ExecuteScalar().ToString());
                int[] id = new int[n];
                int[] id_s = new int[n];
                int[] id_f = new int[n];
                string[] data = new string[n];
                string[] text = new string[n];
                string[] text1 = new string[n];
                string[] text2 = new string[n];
                string[] text3 = new string[n];
                string[] text4 = new string[n];

                //string[] Date = new string[n];

                for (int i = 1; i <= n; i++)
                {
                    SqlCommand command1 = new SqlCommand("select ID_Student FROM Student WHERE ID_Student=" + i + "", connection);
                    if (command1.ExecuteScalar() is null)
                    {

                    }
                    else
                    {
                        id[i - 1] = Convert.ToInt32(command1.ExecuteScalar().ToString());
                        SqlCommand command2 = new SqlCommand("select ID_Student FROM Student WHERE ID_Student=" + i + "", connection);
                        SqlCommand command3 = new SqlCommand("select NOM_ZACHETKI FROM Student WHERE ID_Student=" + i + "", connection);
                        text[i - 1] = Convert.ToString(command3.ExecuteScalar().ToString());
                        SqlCommand command4 = new SqlCommand("select FAMILIYA FROM Student WHERE ID_Student=" + i + "", connection);
                        text1[i - 1] = Convert.ToString(command4.ExecuteScalar().ToString());
                        SqlCommand command5 = new SqlCommand("select IMYA FROM Student WHERE ID_Student=" + i + "", connection);
                        text2[i - 1] = Convert.ToString(command5.ExecuteScalar().ToString());
                        SqlCommand command6 = new SqlCommand("select OTCHESTVO FROM Student WHERE ID_Student=" + i + "", connection);
                        text3[i - 1] = Convert.ToString(command6.ExecuteScalar().ToString());
                        SqlCommand command7 = new SqlCommand("select ID_SPECIALNOST FROM Student WHERE ID_Student=" + i + "", connection);
                        id_s[i - 1] = Convert.ToInt32(command7.ExecuteScalar().ToString());
                        SqlCommand command10 = new SqlCommand("select POL FROM Student WHERE ID_Student=" + i + "", connection);
                        text4[i - 1] = Convert.ToString(command10.ExecuteScalar().ToString());
                        SqlCommand command8 = new SqlCommand("select ID_FAKULTET FROM Student WHERE ID_Student=" + i + "", connection);
                        id_f[i - 1] = Convert.ToInt32(command8.ExecuteScalar().ToString());
                        SqlCommand command9 = new SqlCommand("select DATA_POSTUPLENIA FROM Student WHERE ID_Student=" + i + "", connection);
                        data[i - 1] = Convert.ToString(command9.ExecuteScalar().ToString());
                    }
                }
                List<Students> StudentList = new List<Students>
                {

                };
                for (int i = 136; i <= n; i++)
                {
                    if (id[i - 1] == 0)
                    {

                    }
                    else
                    {
                        StudentList.Add(new Students { ID_Student = i, NOM_ZACHETKI = text[i - 1], FAMILIYA = text1[i - 1], IMYA = text2[i - 1], OTCHESTVO = text3[i - 1], POL= text4[i-1], ID_SPECIALNOST = id_s[i - 1], ID_FAKULTET = id_f[i - 1], DATA_POSTUPLENIA = data[i - 1] });
                    }
                }

                StudentG.ItemsSource = StudentList;
            }
            using (SqlConnection connection = new SqlConnection("data source=ngknn.ru;initial catalog=33П-КР-Кулюкина;persist security info=True;user id=33П;PWD=12357"))
            {
                connection.Open();
                SqlCommand commandd = new SqlCommand("select max(ID_FAKULTET) from FACULTET", connection);
                int n = Convert.ToInt32(commandd.ExecuteScalar().ToString());
                int[] id = new int[n];
                string[] text = new string[n];
                int[] count = new int[n];

                for (int i = 1; i <= n; i++)
                {
                    SqlCommand commandd1 = new SqlCommand("select ID_FAKULTET FROM FACULTET WHERE ID_FAKULTET=" + i + "", connection);
                    if (commandd1.ExecuteScalar() is null)
                    {

                    }
                    else
                    {

                        id[i - 1] = Convert.ToInt32(commandd1.ExecuteScalar().ToString());
                        SqlCommand commandd2 = new SqlCommand("select NAME_FAKULTET FROM FACULTET WHERE ID_FAKULTET=" + i + "", connection);
                        text[i - 1] = Convert.ToString(commandd2.ExecuteScalar().ToString());
                        SqlCommand commandd3 = new SqlCommand("select CountStudent FROM FACULTET WHERE ID_FAKULTET=" + i + "", connection);
                        count[i - 1] = Convert.ToInt32(commandd3.ExecuteScalar().ToString());
                    }
                }
                List<Facultets> FakultetList = new List<Facultets>
                {

                };
                for (int i = 1; i <=n; i++)
                {
                    if (id[i-1] == 0)
                    {

                    }
                    else
                    {
                        FakultetList.Add(new Facultets { ID_FAKULTET = i, NAME_FAKULTET = text[i - 1], CountStudent = count[i-1] });
                    }
                }
                FacultetG.ItemsSource = FakultetList;
            }

        }

        public DataTable Select(string selectSQL) // функция подключения к базе данных и обработка запросов
        {
            DataTable dataTable = new DataTable("dataBase");                // создаём таблицу в приложении
                                                                            // подключаемся к базе данных
            SqlConnection sqlConnection = new SqlConnection("data source=ngknn.ru;initial catalog=33П-КР-Кулюкина;persist security info=True;user id=33П;Password=12357");
            sqlConnection.Open();                                           // открываем базу данных
            SqlCommand sqlCommand = sqlConnection.CreateCommand();          // создаём команду
            sqlCommand.CommandText = selectSQL;                             // присваиваем команде текст
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand); // создаём обработчик
            sqlDataAdapter.Fill(dataTable);                                 // возращаем таблицу с результатом
            sqlConnection.Close();
            return dataTable;
        }
        //Добавление факультета
        private void AddFacultet_Click(object sender, RoutedEventArgs e)
        {
            AddFacultet addSource = new AddFacultet();
            addSource.Show();
        }
        //Редактирование факультета
        private void EditFacultet_Click(object sender, RoutedEventArgs e)
        {
            EditFac editIst = new EditFac();
            editIst.Show();
        }
        //Удаление факультета
        private void DeleteFacultet_Click(object sender, RoutedEventArgs e)
        {
            DeleteFac deleteSource = new DeleteFac();
            deleteSource.Show();
        }
        //Добавление студента
        private void AddStudent_Click(object sender, RoutedEventArgs e)
        {
            AddStud фddOutliers = new AddStud();
            фddOutliers.Show();
        }
        //Редактированее записи о студенте
        private void EditStudent_Click(object sender, RoutedEventArgs e)
        {
            EditStud editOutliers = new EditStud();
            editOutliers.Show();
        }
        //Удаление студента
        private void DeleteStudent_Click(object sender, RoutedEventArgs e)
        {
            DeleteStud deleteOutliers = new DeleteStud();
            deleteOutliers.Show();
        }
        //Максимальное количество студентов
        private void MaxStud_Click(object sender, RoutedEventArgs e)
        {
            MaxStud maxEmission = new MaxStud();
            maxEmission.Show();
        }

        //Среднее количество студентов
        private void AverageStud_Click(object sender, RoutedEventArgs e)
        {
            AverageStud mediumEmission = new AverageStud();
            mediumEmission.Show();
        }
        //Минимальное количество студентов
        private void MinStud_Click(object sender, RoutedEventArgs e)
        {
            MinStud minEmission = new MinStud();
            minEmission.Show();
        }

        internal class Students
        {
            public int ID_Student { get; set; }
            public string NOM_ZACHETKI { get; set; }
            public string FAMILIYA { get; set; }
            public string IMYA { get; set; }
            public string OTCHESTVO { get; set; }
            public int ID_SPECIALNOST { get; set; }
            public string POL { get; set; }
            public int ID_FAKULTET { get; set; }
            public string DATA_POSTUPLENIA { get; set; }

        }
        internal class Facultets
        {
            public int ID_FAKULTET { get; set; }
            public string NAME_FAKULTET { get; set; }
            public int CountStudent { get; set; }

        }

        private void MenuItem_Click_Update(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            Close();
            mainWindow.Show();
        }
    }
}
