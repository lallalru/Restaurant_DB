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
    public partial class signUp : Form
    {
        public static String url = "SERVER=LOCALHOST; USER=jy; DATABASE=restaurnt_schema;" + "PORT=3306; PASSWORD=1234; SSLMODE=NONE";

        private MySqlConnection mConnection; // DB접속
        private MySqlCommand mCommand; // 쿼리문
        private MySqlDataReader mDataReader; // 실행문

        public signUp()
        {
            InitializeComponent();
        }


        private void button2_Click(object sender, EventArgs e) //닫기 버튼 클릭
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e) //가입하기 버튼 클릭
        {
            if (String.IsNullOrWhiteSpace(textBox1.Text) || String.IsNullOrWhiteSpace(textBox2.Text) ||
                String.IsNullOrWhiteSpace(textBox3.Text) || String.IsNullOrWhiteSpace(textBox4.Text))
            {
                MessageBox.Show("필수 기입 항목을 작성해주세요");
            }
            else
            {

                mConnection = new MySqlConnection(url); // DB접속
                mCommand = new MySqlCommand(); // 쿼리문 생성
                mCommand.Connection = mConnection; // DB에 연결

                
                mConnection.Open(); // DB 오픈
                mCommand.CommandText = "select count(*) from 회원;";

                mDataReader = mCommand.ExecuteReader(); // 쿼리문 실행
                int count = 0;
                while (mDataReader.Read()) // 전부 다 읽어 옴
                {
                    count = Convert.ToInt32(mDataReader["count(*)"].ToString())+10;
                }

                Console.WriteLine("count:"+count);
                mConnection.Close();

                mConnection.Open();
                mCommand.CommandText = "insert 회원 values('"+count+"', '"+textBox1.Text+"', '"+textBox2.Text+"', '"+ textBox3.Text+"', '"+ textBox4.Text+"', '"+ textBox5.Text +"', '"+ textBox6.Text
                                                         + "',default,default,default,default);" +        //회원 테이블
                                                            "insert 포인트 values(1000, '"+count+"');" +  //포인트 테이블
                                                            "insert 할인 values("+count+");"+             //할인 테이블
                                                            "insert 할인쿠폰 values("+count+", default);"+  //할인쿠폰 테이블
                                                            "insert 회원이력 values('"+count+"', default, curdate());"   //회원이력 테이블
                                                            ;
                mCommand.ExecuteNonQuery();
                Console.WriteLine(mCommand.CommandText);
                mConnection.Close(); 

                MessageBox.Show("회원 가입 완료! 회원번호는 "+count+" 입니다.");
                this.Close();
            }
        }
    }
}
