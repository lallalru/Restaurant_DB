using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace DB_Restaurant
{
    public partial class his_store : Form
    {
        public static String url = "SERVER=LOCALHOST; USER=jy; DATABASE=restaurnt_schema;" +

            "PORT=3306; PASSWORD=1234; SSLMODE=NONE";
        private MySqlConnection mConnection; // DB접속
        private MySqlCommand mCommand; // 쿼리문
        private MySqlDataReader mDataReader; // 실행문

        DataTable table = new DataTable();

        public his_store()
        {
            InitializeComponent();
            table.Columns.Add("매장번호", typeof(string));
            table.Columns.Add("매장이력순서", typeof(string));
            table.Columns.Add("테이블수이력", typeof(string));
            table.Columns.Add("오픈시간이력", typeof(string));
            table.Columns.Add("마감시간이력", typeof(string));
            table.Columns.Add("매장주소이력", typeof(string));
            table.Columns.Add("매장전화번호이력", typeof(string));

            dataGridView1.DataSource = table;
        }

        private void his_store_Load(object sender, EventArgs e)
        {
            mConnection = new MySqlConnection(url); // DB접속
            mCommand = new MySqlCommand(); // 쿼리문 생성
            mCommand.Connection = mConnection; // DB에 연결

            mCommand.CommandText = "SELECT * FROM 매장이력 where ( 매장번호 ='"+store.select_store_num+"')"; // 쿼리문 작성
            mConnection.Open(); // DB 오픈
            mDataReader = mCommand.ExecuteReader(); // 쿼리문 실행

            while (mDataReader.Read()) // 전부 다 읽어 옴
            {
                table.Rows.Add(mDataReader["매장번호"].ToString(), mDataReader["매장이력순서"].ToString(), mDataReader["테이블수이력"].ToString(),
                                mDataReader["오픈시간이력"].ToString(), mDataReader["마감시간이력"].ToString(), mDataReader["매장주소이력"].ToString(), mDataReader["매장전화번호이력"].ToString());

            }

            mConnection.Close(); // 사용 후 객체 닫기
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
