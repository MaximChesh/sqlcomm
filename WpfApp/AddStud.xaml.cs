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
    /// Логика взаимодействия для AddStud.xaml
    /// </summary>
    public partial class AddStud : Window
    {
        public string id;
        public string num_zachet;
        public string surn;
        public string name;
        public string otch;
        public string specialnost;
        public string id_f;
        public string data;
        public string pol;
        public AddStud()
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
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            num_zachet = NumZachetText.Text;
            surn = SurnameText.Text;
            name = NameText.Text;
            otch = PatronymicText.Text;
            pol = Sex.Text;
            specialnost = IdSpecialnostText.Text;
            id_f = IdFacultetText.Text;
            data = DatePostupleniaText.Text;
            int n=0;
            DataTable addemis = Select("Insert into Student (NOM_ZACHETKI,FAMILIYA,IMYA,OTCHESTVO,ID_SPECIALNOST,POL, ID_FAKULTET,DATA_POSTUPLENIA) " +
                "Values ('"+ num_zachet +"','"+ surn +"','"+ name +"','"+ otch +"','"+ specialnost + "','" + pol + "','" + id_f + "','"+ data +"');");
            using (SqlConnection connection = new SqlConnection("data source=ngknn.ru;initial catalog=33П-КР-Кулюкина;persist security info=True;user id=33П;Password=12357"))
            {
                connection.Open();
                SqlCommand command = new SqlCommand($"SELECT CountStudent FROM FACULTET where ID_FAKULTET = '{id_f}'", connection);
                n = Convert.ToInt32(command.ExecuteScalar().ToString());
            }
                DataTable upemis = Select($"UPDATE [dbo].[FACULTET] SET  [CountStudent] = ({(n+1)}) WHERE [ID_FAKULTET] = '{id_f}';");
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
