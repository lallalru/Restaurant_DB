using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace DB_Restaurant
{
    public partial class store : Form
    {
        public static String url = "SERVER=LOCALHOST; USER=jy; DATABASE=restaurnt_schema;" +

               "PORT=3306; PASSWORD=1234; SSLMODE=NONE";

        // DB접속 URL 설정 - SERVER : DB주소, USER : ID명, DATABASE : DB명, PORT : TCP 포트번호
        // PASSWORD : 비밀번호, SSLMODE : NONE (SSL 사용안함)

        private MySqlConnection mConnection; // DB접속
        private MySqlCommand mCommand; // 쿼리문
        private MySqlDataReader mDataReader; // 실행문
        public static string select_store_num;

        DataTable table = new DataTable();

        public store()
        {
            InitializeComponent();
            table.Columns.Add("매장번호", typeof(string));
            table.Columns.Add("매장주소", typeof(string));
            table.Columns.Add("매장전화번호", typeof(string));
            table.Columns.Add("테이블수", typeof(string));
            table.Columns.Add("오픈시간", typeof(string));
            table.Columns.Add("마감시간", typeof(string));

            // 값들이 입력된 테이블을 DataGridView에 입력합니다.
            dataGridView1.DataSource = table;
        }
        private void store_Load(object sender, EventArgs e)
        {
            mConnection = new MySqlConnection(url); // DB접속
            mCommand = new MySqlCommand(); // 쿼리문 생성
            mCommand.Connection = mConnection; // DB에 연결

            mCommand.CommandText = "SELECT * FROM 매장"; // 쿼리문 작성
            mConnection.Open(); // DB 오픈
            mDataReader = mCommand.ExecuteReader(); // 쿼리문 실행

            while (mDataReader.Read()) // 전부 다 읽어 옴
            {
                table.Rows.Add(mDataReader["매장번호"].ToString(), mDataReader["매장주소"].ToString(), mDataReader["매장전화번호"].ToString()
                                            ,mDataReader["테이블수"].ToString(), mDataReader["오픈시간"].ToString(), mDataReader["마감시간"].ToString());
            }

            mConnection.Close(); // 사용 후 객체 닫기

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            select_store_num = (dataGridView1.Rows[this.dataGridView1.CurrentCellAddress.Y].Cells[0].Value.ToString());
            mod_store Form21 = new mod_store();
            Form21.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            select_store_num = (dataGridView1.Rows[this.dataGridView1.CurrentCellAddress.Y].Cells[0].Value.ToString());
            his_store Form22 = new his_store();
            Form22.Show();
        }
    }
}
