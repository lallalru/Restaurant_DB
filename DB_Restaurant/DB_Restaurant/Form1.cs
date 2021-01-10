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
    public partial class Form1 : Form
    {
        public static String url = "SERVER=LOCALHOST; USER=jy; DATABASE=restaurnt_schema;" + "PORT=3306; PASSWORD=1234; SSLMODE=NONE";
        private MySqlConnection mConnection; // DB접속
        private MySqlCommand mCommand; // 쿼리문
        private MySqlDataReader mDataReader; // 실행문


        public static int mem_num=0;
        public static string store_num;
        public static string mem_num2;
        public static int no_show_count;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e) //매장조회
        {
            store Form2 = new store();
            Form2.Show();
        }

        private void button2_Click(object sender, EventArgs e) //회원가입
        {
            signUp Form2 = new signUp();
            Form2.Show();
        }

        private void button4_Click(object sender, EventArgs e) //메뉴조회
        {
            menu Form4 = new menu();
            Form4.Show();
        }

        private void button3_Click(object sender, EventArgs e)  //로그인
        {
            mConnection = new MySqlConnection(url); // DB접속
            mCommand = new MySqlCommand(); // 쿼리문 생성
            mCommand.Connection = mConnection; // DB에 연결

            if (String.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("회원번호를 입력해주세요");
            }
            else
            {
                mConnection.Open(); // DB 오픈
                mCommand.CommandText = "select 회원이름 from 회원 where 회원번호=" + textBox1.Text + ";";
                mDataReader = mCommand.ExecuteReader(); // 쿼리문 실행
                if (!mDataReader.Read())
                {
                    MessageBox.Show("존재하지않는 회원번호입니다.");
                    mConnection.Close();
                }
                else
                {
                    mConnection.Close();
                    mem_num = Convert.ToInt32(textBox1.Text);
                    login Form3 = new login();
                    Form3.Show();
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)  //주문하기
        {
            mConnection = new MySqlConnection(url); // DB접속
            mCommand = new MySqlCommand(); // 쿼리문 생성
            mCommand.Connection = mConnection; // DB에 연결
            if (String.IsNullOrWhiteSpace(textBox2.Text))
            {
                MessageBox.Show("주문하실 매장번호를 입력해주세요");
            }
            else
            {
                mConnection.Open(); // DB 오픈
                mCommand.CommandText = "select 매장번호 from 매장 where 매장번호=" + textBox2.Text + ";";
                mDataReader = mCommand.ExecuteReader(); // 쿼리문 실행
                if (!mDataReader.Read())
                {
                    MessageBox.Show("존재하지않는 매장번호입니다.");
                    mConnection.Close();
                }
                else
                {
                    mConnection.Close();
                    store_num = textBox2.Text;
                    order Form5 = new order();
                    Form5.Show();
                }
            }
        }

        private void button8_Click(object sender, EventArgs e)  //예약하기
        {
            mConnection = new MySqlConnection(url); // DB접속
            mCommand = new MySqlCommand(); // 쿼리문 생성
            mCommand.Connection = mConnection; // DB에 연결

            if (String.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("회원번호를 입력해주세요");
            }
            else
            {
                mConnection.Open(); // DB 오픈
                mCommand.CommandText = "select 회원이름 from 회원 where 회원번호=" + textBox4.Text + ";";
                mDataReader = mCommand.ExecuteReader(); // 쿼리문 실행
                if (!mDataReader.Read())
                {
                    MessageBox.Show("존재하지않는 회원번호입니다.");
                    mConnection.Close();
                }
                else
                {
                    mConnection.Close();

                    mConnection.Open(); // DB 오픈
                    mCommand.CommandText = "select No_Show from 회원 where 회원번호=" + textBox4.Text + ";";
                    mDataReader = mCommand.ExecuteReader(); // 쿼리문 실행
                    
                    while (mDataReader.Read()) // 전부 다 읽어 옴
                    {
                        no_show_count = Convert.ToInt32(mDataReader["No_Show"].ToString());
                    }

                    if (no_show_count >=3)
                    {
                        MessageBox.Show("No-Show 횟수가 3회이상인 회원은 예약을 할 수 없습니다.");
                    }
                    else
                    {
                        mem_num2 = textBox4.Text;
                        reservation Form8 = new reservation();
                        Form8.Show();
                    }
                    mConnection.Close();
                }
            }
        }

        private void button10_Click(object sender, EventArgs e)  //예약취소
        {
            mConnection = new MySqlConnection(url); // DB접속
            mCommand = new MySqlCommand(); // 쿼리문 생성
            mCommand.Connection = mConnection; // DB에 연결

            mConnection.Open(); // DB 오픈
            mCommand.CommandText = "select 예약취소횟수 from 회원 where (회원번호='" + textBox6.Text + "'); ";
            mDataReader = mCommand.ExecuteReader();
            int cancel_count = 0;
            while (mDataReader.Read())
            {
                cancel_count = Convert.ToInt32(mDataReader["예약취소횟수"].ToString());
            }
            mConnection.Close();
            Console.WriteLine(cancel_count + "취소횟수");

            if (String.IsNullOrWhiteSpace(textBox6.Text))
            {
                MessageBox.Show("회원번호를 입력해주세요");
            }
            else if (cancel_count >= 5)
            {
                MessageBox.Show("지난 1년간 예약취소횟수가 5회 이상인 회원은 예약을 취소할 수 없습니다.");
            }
            else
            {
                mConnection.Open(); // DB 오픈
                mCommand.CommandText = "select 회원이름 from 회원 where 회원번호=" + textBox6.Text + ";";
                mDataReader = mCommand.ExecuteReader(); // 쿼리문 실행
                if (!mDataReader.Read())
                {
                    MessageBox.Show("존재하지않는 회원번호입니다.");
                    mConnection.Close();
                }
                else
                {
                    mConnection.Close();
                    mem_num2 = textBox6.Text;
                    cancel Form10 = new cancel();
                    Form10.Show();
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)  //주문조회
        {
            mConnection = new MySqlConnection(url); // DB접속
            mCommand = new MySqlCommand(); // 쿼리문 생성
            mCommand.Connection = mConnection; // DB에 연결

            if (String.IsNullOrWhiteSpace(textBox3.Text))
            {
                MessageBox.Show("매장번호를 입력해주세요");
            }
            else
            {
                mConnection.Open(); // DB 오픈
                mCommand.CommandText = "select 주문번호 from 주문 where 매장번호=" + textBox3.Text + ";";
                mDataReader = mCommand.ExecuteReader(); // 쿼리문 실행
                if (!mDataReader.Read())
                {
                    MessageBox.Show("해당 매장에 대한 주문이 존재하지 않습니다.");
                    mConnection.Close();
                }
                else
                {
                    mConnection.Close();
                    mem_num2 = textBox3.Text;
                    find_order Form7 = new find_order();
                    Form7.Show();
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)  //No-Show
        {
            mConnection = new MySqlConnection(url); // DB접속
            mCommand = new MySqlCommand(); // 쿼리문 생성
            mCommand.Connection = mConnection; // DB에 연결

            if (String.IsNullOrWhiteSpace(textBox5.Text))
            {
                MessageBox.Show("매장번호를 입력해주세요");
            }
            else
            {
                mConnection.Open(); // DB 오픈
                mCommand.CommandText = "select 예약번호 from 예약 where 방문매장=" + textBox5.Text + ";";
                mDataReader = mCommand.ExecuteReader(); // 쿼리문 실행
                if (!mDataReader.Read())
                {
                    MessageBox.Show("해당 매장에 대한 예약이 존재하지 않습니다.");
                    mConnection.Close();
                }
                else
                {
                    mConnection.Close();
                    mem_num2 = textBox5.Text;
                    no_show Form9 = new no_show();
                    Form9.Show();
                }
            }
        }
    }
}
