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
    public partial class order : Form
    {
        public static String url = "SERVER=LOCALHOST; USER=jy; DATABASE=restaurnt_schema;" + "PORT=3306; PASSWORD=1234; SSLMODE=NONE";

        private MySqlConnection mConnection; // DB접속
        private MySqlCommand mCommand; // 쿼리문
        private MySqlDataReader mDataReader; // 실행문


        public order()
        {
            InitializeComponent();
        }

        private void order_Load(object sender, EventArgs e)
        {
            mConnection = new MySqlConnection(url); // DB접속
            mCommand = new MySqlCommand(); // 쿼리문 생성
            mCommand.Connection = mConnection; // DB에 연결

            mConnection.Open(); // DB 오픈
            mCommand.CommandText = "select 테이블수 from 매장 where 매장번호="+Form1.store_num+";";

            mDataReader = mCommand.ExecuteReader(); // 쿼리문 실행
            
            while (mDataReader.Read()) // 전부 다 읽어 옴
            {
                label4.Text +="1 ~ "+mDataReader["테이블수"].ToString();
            }
            mConnection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrWhiteSpace(textBox5.Text) || String.IsNullOrWhiteSpace(textBox6.Text))
            {
                MessageBox.Show("테이블 번호와 이용인원수를 입력해주세요");
            }
            else
            {
                mConnection.Open(); // DB 오픈
                mCommand.CommandText = "select count(주문번호) from 주문;";

                mDataReader = mCommand.ExecuteReader();

                int count = 0;
                while (mDataReader.Read())
                {
                    count = Convert.ToInt32(mDataReader["count(주문번호)"].ToString())+1;
                }
                mConnection.Close();

                int total = Convert.ToInt32(textBox1.Text) * 25000 + Convert.ToInt32(textBox2.Text) * 14500 + Convert.ToInt32(textBox3.Text) * 11900 + Convert.ToInt32(textBox4.Text) * 8900;
                mConnection.Open(); // DB 오픈
                mCommand.CommandText = "insert 주문 values('"+count+ "','"+Form1.store_num+"',"+textBox5.Text+", "+textBox6.Text+","+total+"); ";
                mCommand.ExecuteNonQuery();
                Console.WriteLine(mCommand.CommandText);
                mConnection.Close();

                MessageBox.Show("주문번호  " + count + "  , " + total + " 원 주문 완료되었습니다.");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
