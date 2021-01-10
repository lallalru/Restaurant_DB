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
    public partial class find_order : Form
    {
        public static String url = "SERVER=LOCALHOST; USER=jy; DATABASE=restaurnt_schema;" + "PORT=3306; PASSWORD=1234; SSLMODE=NONE";

        private MySqlConnection mConnection; // DB접속
        private MySqlCommand mCommand; // 쿼리문
        private MySqlDataReader mDataReader; // 실행문

        public static string order_num;

        DataTable table = new DataTable();

        public find_order()
        {
            InitializeComponent();
            table.Columns.Add("주문번호", typeof(string));
            table.Columns.Add("매장번호", typeof(string));
            table.Columns.Add("주문테이블번호", typeof(string));
            table.Columns.Add("이용인원수", typeof(string));
            table.Columns.Add("총주문금액", typeof(string));
            dataGridView1.DataSource = table;
        }

        private void find_order_Load(object sender, EventArgs e)
        {

            mConnection = new MySqlConnection(url); // DB접속
            mCommand = new MySqlCommand(); // 쿼리문 생성
            mCommand.Connection = mConnection; // DB에 연결

            mConnection.Open(); // DB 오픈
            mCommand.CommandText = "select 매장번호, 매장주소 from 매장 where (매장번호='" + Form1.mem_num2 + "'); ";
            mDataReader = mCommand.ExecuteReader();

            int count = 0;
            while (mDataReader.Read())
            {
                label1.Text += "매장번호 "+mDataReader["매장번호"].ToString() +" , "+ mDataReader["매장주소"].ToString()+" 의 주문내역";
            }
            Console.WriteLine(mCommand.CommandText);
            mConnection.Close();

            mCommand.CommandText = "SELECT * FROM 주문 where (매장번호='" + Form1.mem_num2 + "');"; // 쿼리문 작성
            mConnection.Open(); // DB 오픈
            mDataReader = mCommand.ExecuteReader(); // 쿼리문 실행

            while (mDataReader.Read()) // 전부 다 읽어 옴
            {
                table.Rows.Add(mDataReader["주문번호"].ToString(), mDataReader["매장번호"].ToString(), mDataReader["주문테이블번호"].ToString()
                               , mDataReader["이용인원수"].ToString(), mDataReader["총주문금액"].ToString());

            }

            mConnection.Close(); // 사용 후 객체 닫기

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            order_num = (dataGridView1.Rows[this.dataGridView1.CurrentCellAddress.Y].Cells[0].Value.ToString());  //선택셀 주문번호 
            pay Form31 = new pay();
            Form31.Show();
        }
    }
}
