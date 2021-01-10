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
    public partial class reservation : Form
    {
        public static String url = "SERVER=LOCALHOST; USER=jy; DATABASE=restaurnt_schema;" + "PORT=3306; PASSWORD=1234; SSLMODE=NONE";

        private MySqlConnection mConnection; // DB접속
        private MySqlCommand mCommand; // 쿼리문
        private MySqlDataReader mDataReader; // 실행문

        public reservation()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            mConnection = new MySqlConnection(url); // DB접속
            mCommand = new MySqlCommand(); // 쿼리문 생성
            mCommand.Connection = mConnection; // DB에 연결

            if (String.IsNullOrWhiteSpace(textBox1.Text) || String.IsNullOrWhiteSpace(textBox2.Text) || String.IsNullOrWhiteSpace(textBox3.Text) || String.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("예약정보를 전부 입력해주세요");
            }
            else if (Convert.ToInt32(textBox2.Text) <= Convert.ToInt32(DateTime.Now.AddDays(-1).ToString("yyyyMMdd")) )
            {
                MessageBox.Show("예약은 예약일로부터 하루 전까지 가능합니다.");
            }
            else
            {
                mConnection.Open(); // DB 오픈
                mCommand.CommandText = "select count(예약번호) from 예약;";
                mDataReader = mCommand.ExecuteReader();

                int count = 0;
                while (mDataReader.Read())
                {
                    count = Convert.ToInt32(mDataReader["count(예약번호)"].ToString()) + 1;
                }
                mConnection.Close();

                mConnection.Open(); // DB 오픈
                mCommand.CommandText = "insert 예약 values('" + count + "','" + textBox1.Text + "','" + textBox2.Text + "', '" + textBox3.Text + "'," +
                                                                textBox4.Text + ",'" + Form1.mem_num2 + "'); ";
                mCommand.ExecuteNonQuery();
                Console.WriteLine(mCommand.CommandText);
                mConnection.Close();

                MessageBox.Show("예약번호  " + count + "  로 예약 완료되었습니다.");
                this.Close();
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       
    }
}
