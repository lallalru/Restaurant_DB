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
    public partial class login : Form
    {
        public static String url = "SERVER=LOCALHOST; USER=jy; DATABASE=restaurnt_schema;" + "PORT=3306; PASSWORD=1234; SSLMODE=NONE";
        private MySqlConnection mConnection; // DB접속
        private MySqlCommand mCommand; // 쿼리문
        private MySqlDataReader mDataReader; // 실행문

        DataTable table = new DataTable();

        public login()
        {
            InitializeComponent();
            table.Columns.Add("누적포인트", typeof(string));
            table.Columns.Add("회원등급", typeof(string));
            table.Columns.Add("할인율", typeof(string));
            table.Columns.Add("연간결제횟수", typeof(string));
            table.Columns.Add("예약취소횟수", typeof(string));
            table.Columns.Add("No_Show", typeof(string));
            dataGridView1.DataSource = table;
        }

        private void login_Load(object sender, EventArgs e)
        {
            mConnection = new MySqlConnection(url); // DB접속
            mCommand = new MySqlCommand(); // 쿼리문 생성
            mCommand.Connection = mConnection; // DB에 연결
            

            mConnection.Open(); // DB 오픈
            mCommand.CommandText = "select 회원이름 from 회원 where 회원번호="+Form1.mem_num+";";
            mDataReader = mCommand.ExecuteReader(); 
            while (mDataReader.Read()) // 전부 다 읽어 옴
            {
                label2.Text += " " + Form1.mem_num + "  " + mDataReader["회원이름"].ToString() + " 님 반갑습니다!";
            }        
            mConnection.Close();


            mConnection.Open(); // DB 오픈
            mCommand.CommandText = "select 누적포인트 from 포인트 where 회원번호=" + Form1.mem_num + ";";
            mDataReader = mCommand.ExecuteReader(); // 쿼리문 실행
            while (mDataReader.Read()) // 전부 다 읽어 옴
            {
                label3.Text += " " + mDataReader["누적포인트"].ToString() + "  이고 현재 등급은  ";
            }
            mConnection.Close();


            mConnection.Open(); // DB 오픈
            mCommand.CommandText = "select 회원등급 from 회원 where 회원번호=" + Form1.mem_num + ";";
            mDataReader = mCommand.ExecuteReader(); // 쿼리문 실행
            while (mDataReader.Read()) // 전부 다 읽어 옴
            {
                label3.Text += mDataReader["회원등급"].ToString() + "  입니다. ";
            }
            mConnection.Close();


            mConnection.Open();
            mCommand.CommandText = "select 누적포인트,회원등급,할인율,연간결제횟수,예약취소횟수,No_Show from 회원,포인트,할인쿠폰 " +
                                   "where 회원.회원번호="+Form1.mem_num+ " and 포인트.회원번호=" + Form1.mem_num + " and 할인쿠폰.회원번호=" + Form1.mem_num + ";";
            mDataReader = mCommand.ExecuteReader();
            while (mDataReader.Read()) // 전부 다 읽어 옴
            {
                table.Rows.Add(mDataReader["누적포인트"].ToString(), mDataReader["회원등급"].ToString(), mDataReader["할인율"].ToString(),
                            mDataReader["연간결제횟수"].ToString(), mDataReader["예약취소횟수"].ToString(), mDataReader["No_Show"].ToString());
            }
            mConnection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
