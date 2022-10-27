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
    public partial class EditStud : Window
    {
        public string id;
        public string sex;
        public string id_spec;
        public string id_fac;
        public string data;
        public string nom;
        public string fam;
        public string im;
        public string otch;
        public string pol;

        public EditStud()
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
            using (SqlConnection sqlConnection = new SqlConnection("data source=ngknn.ru;initial catalog=33П-КР-Кулюкина;persist security info=True;user id=33П;Password=12357"))
            {
                sqlConnection.Open();
                id = IdText.Text;
                id_spec = IdSpecialnostText.Text;
                id_fac = IdFacultetText.Text;
                data = DatePosatupleniaText.Text;
                nom = NomText.Text;
                fam = SurnameText.Text;
                im = NameText.Text;
                otch = PatronymicText.Text;
                pol = Sex.Text;
                //IdSp = IdSpecialnost.Text;
                //IdF = IdFacultet.Text;
                //datePos = DatePosatuplenia.Text;
                int id1 = Convert.ToInt32(id);
                int n = 0;
                int nn = 0;
                int nnn = 0;
                //int id2 = Convert.ToInt32(id);
                using (SqlConnection connection = new SqlConnection("data source=ngknn.ru;initial catalog=33П-КР-Кулюкина;persist security info=True;user id=33П;Password=12357"))
                {
                    connection.Open();
                    SqlCommand command = new SqlCommand($"SELECT ID_FAKULTET FROM Student where ID_Student = '{id1}'", connection);
                    n = Convert.ToInt32(command.ExecuteScalar().ToString());
                    SqlCommand command1 = new SqlCommand($"SELECT CountStudent FROM FACULTET where ID_FAKULTET = '{n}'", connection);
                    nn = Convert.ToInt32(command1.ExecuteScalar().ToString());
                    SqlCommand command2 = new SqlCommand($"SELECT CountStudent FROM FACULTET where ID_FAKULTET = '{id_fac}'", connection);
                    nnn = Convert.ToInt32(command2.ExecuteScalar().ToString());
                }
                DataTable upemis = Select($"UPDATE [dbo].[FACULTET] SET  [CountStudent] = ({(nn - 1)}) WHERE [ID_FAKULTET] = '{n}';");
                DataTable upemis1 = Select($"UPDATE [dbo].[FACULTET] SET  [CountStudent] = ({(nnn + 1)}) WHERE [ID_FAKULTET] = '{id_fac}';");
                DataTable sqlCommand = Select($"UPDATE [dbo].[Student] SET  NOM_ZACHETKI = '{nom}', [FAMILIYA] = '{fam}', [IMYA] = '{im}', [OTCHESTVO] = '{otch}', [POL] = '{pol}', [ID_SPECIALNOST] = '{id_spec}', [ID_FAKULTET] = '{id_fac}', [DATA_POSTUPLENIA] = '{data}' WHERE [ID_Student] = '{id1}';");
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
