using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
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
    /// Логика взаимодействия для DeleteFac.xaml
    /// </summary>
    public partial class DeleteStud : Window
    {
        public string dele;
        public DeleteStud()
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

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            using (SqlConnection sqlConnection = new SqlConnection("data source=ngknn.ru;initial catalog=33П-КР-Кулюкина;persist security info=True;user id=33П;Password=12357"))
            {
                sqlConnection.Open();
                dele = IdStudentText.Text;
                int n = 0;
                int nn = 0;
                int dele1 = Convert.ToInt32(dele);
                using (SqlConnection connection = new SqlConnection("data source=ngknn.ru;initial catalog=33П-КР-Кулюкина;persist security info=True;user id=33П;Password=12357"))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand($"SELECT ID_FAKULTET FROM Student where ID_Student = '{dele}'", connection);
                    n = Convert.ToInt32(command.ExecuteScalar().ToString());
                    SqlCommand command1 = new SqlCommand($"SELECT CountStudent FROM FACULTET where ID_FAKULTET = '{n}'", connection);
                    nn = Convert.ToInt32(command1.ExecuteScalar().ToString());
                }
                DataTable sqlCommand = Select($"DELETE FROM [dbo].[Student] WHERE [ID_Student] = '{dele1}';");
                DataTable upemis = Select($"UPDATE [dbo].[FACULTET] SET  [CountStudent] = ({(nn - 1)}) WHERE [ID_FAKULTET] = '{n}';");
                Close();
            }
        }
    }
}
