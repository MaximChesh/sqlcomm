using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SqlClient;
using System.Data;
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
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class EditFac : Window
    {
        public string id;
        public string name;
        public int countS;

        public EditFac()
        {
            InitializeComponent();
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
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection sqlConnection = new SqlConnection("server=ngknn.ru;Trusted_Connection=No;DataBase=Emissions_Barashenkov;User ID=33П;Password=12357"))
            {
                sqlConnection.Open();
                id = IdFacultetText.Text;
                name = NameFacultetText.Text;
                countS = Convert.ToInt32(CountSrudent.Text);
                int id1 = Convert.ToInt32(id);
                DataTable sqlCommand = Select($"UPDATE [dbo].[FACULTET] SET [NAME_FAKULTET] = '{name}', [CountStudent] = ({countS}) WHERE [ID_FAKULTET] = '{id1}';");
                sqlConnection.Close();
            }
            Close();
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
