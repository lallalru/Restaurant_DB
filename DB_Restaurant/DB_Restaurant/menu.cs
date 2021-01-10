using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using MySql.Data.MySqlClient;

namespace DB_Restaurant
{
    public partial class menu : Form
    {
        public static String url = "SERVER=LOCALHOST; USER=jy; DATABASE=restaurnt_schema;" +

            "PORT=3306; PASSWORD=1234; SSLMODE=NONE";
        private MySqlConnection mConnection; // DB접속
        private MySqlCommand mCommand; // 쿼리문
        private MySqlDataReader mDataReader; // 실행문


        DataTable table = new DataTable();
        public menu()
        {
            InitializeComponent();
            table.Columns.Add("메뉴번호", typeof(string));
            table.Columns.Add("메뉴명", typeof(string));
            table.Columns.Add("가격", typeof(string));

            // 값들이 입력된 테이블을 DataGridView에 입력합니다.
            dataGridView1.DataSource = table;
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            mConnection = new MySqlConnection(url); // DB접속
            mCommand = new MySqlCommand(); // 쿼리문 생성
            mCommand.Connection = mConnection; // DB에 연결

            mCommand.CommandText = "SELECT * FROM 메뉴"; // 쿼리문 작성
            mConnection.Open(); // DB 오픈
            mDataReader = mCommand.ExecuteReader(); // 쿼리문 실행

            while (mDataReader.Read()) // 전부 다 읽어 옴
            {
                table.Rows.Add(mDataReader["메뉴번호"].ToString(), mDataReader["메뉴명"].ToString(), mDataReader["메뉴금액"].ToString());
                
            }

            mConnection.Close(); // 사용 후 객체 닫기
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        
    }
}
