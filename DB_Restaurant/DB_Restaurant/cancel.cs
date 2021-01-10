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
    public partial class cancel : Form
    {
        public static String url = "SERVER=LOCALHOST; USER=jy; DATABASE=restaurnt_schema;" + "PORT=3306; PASSWORD=1234; SSLMODE=NONE";

        private MySqlConnection mConnection; // DB접속
        private MySqlCommand mCommand; // 쿼리문
        private MySqlDataReader mDataReader; // 실행문

        public cancel()
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
                MessageBox.Show("예약했던정보를 전부 입력해주세요");
            }
            else if (Convert.ToInt32(textBox2.Text) <= Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd"))
                        && Convert.ToInt32(textBox3.Text) >= Convert.ToInt32(DateTime.Now.AddMinutes(-120).ToString("HHmm")))
            {
                MessageBox.Show("예약취소는 예약시간으로부터 2시간 전까지 가능합니다.");
            }
            else
            {
                mConnection.Open(); // DB 오픈
                mCommand.CommandText = "delete from 예약 where(예약번호='" + textBox5.Text + "') and (회원번호='" + Form1.mem_num2 + "'); "
                                            + "update 회원 set 예약취소횟수 = 예약취소횟수+ 1 where (회원번호 = '" + Form1.mem_num2 + "'); ";
                mCommand.ExecuteNonQuery();
                Console.WriteLine(mCommand.CommandText);
                mConnection.Close();

                MessageBox.Show("예약이 취소되었습니다.");
                this.Close();
                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
