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
    public partial class pay : Form
    {
        public static String url = "SERVER=LOCALHOST; USER=jy; DATABASE=restaurnt_schema;" + "PORT=3306; PASSWORD=1234; SSLMODE=NONE";

        private MySqlConnection mConnection; // DB접속
        private MySqlCommand mCommand; // 쿼리문
        private MySqlDataReader mDataReader; // 실행문
        public static string pay_mem, pay_mem_grade;
        public double total = 0;
        public double mem_coupon = 0;
        public double dis_rate = 0;
        public pay()
        {
            InitializeComponent();
        }


        private void pay_Load(object sender, EventArgs e)
        {
            mConnection = new MySqlConnection(url); // DB접속
            mCommand = new MySqlCommand(); // 쿼리문 생성
            mCommand.Connection = mConnection; // DB에 연결

            mConnection.Open(); // DB 오픈
            mCommand.CommandText = "select 총주문금액 from 주문 where (주문번호='" + find_order.order_num + "'); ";
            mDataReader = mCommand.ExecuteReader();


            while (mDataReader.Read())
            {
                label1.Text += mDataReader["총주문금액"].ToString() + "  원 ";
                label4.Text += mDataReader["총주문금액"].ToString() + "  원 ";
                total = Convert.ToInt32(mDataReader["총주문금액"].ToString());
            }
            Console.WriteLine(mCommand.CommandText);
            mConnection.Close();

        }
        private void textBox1_TextChanged(object sender, EventArgs e)  //회원번호
        {
            mConnection = new MySqlConnection(url); // DB접속
            mCommand = new MySqlCommand(); // 쿼리문 생성
            mCommand.Connection = mConnection; // DB에 연결

            mConnection.Open(); // DB 오픈
            mCommand.CommandText = "select 회원등급 from 회원 where (회원번호='" + textBox1.Text + "'); ";
            mDataReader = mCommand.ExecuteReader();

            int count = 0;
            while (mDataReader.Read())
            {
                label5.Text = "- 회원번호 ( " + mDataReader["회원등급"].ToString() + " )  ";
                pay_mem_grade = mDataReader["회원등급"].ToString();
            }
            Console.WriteLine(mCommand.CommandText);
            mConnection.Close();
        }
        private void textBox2_TextChanged(object sender, EventArgs e)  //할인쿠폰사용
        {
            if (textBox2.Text.Equals("네"))
            {
                if (pay_mem_grade.Equals("골드"))
                {
                    label8.Text = "- 할인쿠폰 사용 ( 골드 ) 10%";
                    mem_coupon = 0.1;
                }
                else if (pay_mem_grade.Equals("실버"))
                {
                    label8.Text = "- 할인쿠폰 사용 ( 실버 ) 7%";
                    mem_coupon = 0.07;
                }
                else if (pay_mem_grade.Equals("일반"))
                {
                    label8.Text = "- 할인쿠폰 사용 ( 일반 ) 3%";
                    mem_coupon = 0.03;
                }
                else
                {
                    label8.Text = "- 할인쿠폰 사용 (  ) 0% ";
                    mem_coupon = 0.00;
                }
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text.Equals("네") && !textBox4.Text.Equals("네"))
            {
                label9.Text = "- 통신사 제휴할인 사용 ( 5% )";

            }
            else { label9.Text = "- 통신사 제휴할인 사용"; }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox4.Text.Equals("네") && !textBox3.Text.Equals("네"))
            {
                label10.Text = "- 신용카드 제휴할인 사용 ( 5% )";
            }
            else {
                label10.Text = "- 신용카드 제휴할인 사용 "; }
        }

        private void button4_Click(object sender, EventArgs e)  //계산하기
        {
            if (textBox4.Text.Equals("네") && textBox3.Text.Equals("네"))
            {
                MessageBox.Show("신용카드 제휴할인과 통신사 제휴할인 중 하나만 선택해주세요. ");
            }
            else
            {
                if (textBox2.Text.Equals("네"))
                {
                    dis_rate = mem_coupon;
                    label4.Text = "- 최종결제금액: " + total * (1 - dis_rate) + " 원";
                }
                if (textBox4.Text.Equals("네") || textBox3.Text.Equals("네"))
                {
                    dis_rate += 0.05;
                    label4.Text = "- 최종결제금액: " + total * (1 - dis_rate) + " 원";
                }
            }
        }

        private void button3_Click(object sender, EventArgs e) //비회원결제
        {
            if (! ( (textBox5.Text.Equals("0")) || (textBox5.Text.Equals("1")) || (textBox5.Text.Equals("2")) || (textBox5.Text.Equals("3")) 
                || (textBox5.Text.Equals("4")) || (textBox5.Text.Equals("5")) || (textBox5.Text.Equals("6") ) ) ) 
            {
                MessageBox.Show("할부 개월은 0개월부터 최대 6개월까지 가능합니다.");
            }
            else
            {
                MessageBox.Show("비회원결제로 " + total * (1 - dis_rate) + " 원, 할부 " + textBox5.Text + "개월로 결제되었습니다.");
                mConnection.Open(); // DB 오픈
                mCommand.CommandText = "delete from 주문 where(주문번호='" +find_order.order_num+ "') and (매장번호='" +Form1.mem_num2 + "') and (총주문금액 ='"+total+"'); "
                                + "insert 결제 values ('"+find_order.order_num+ "', '"+Form1.mem_num2+"', '"+textBox1.Text+"', "+total+", "+textBox5.Text+","+ total * (1 - dis_rate) + ");";
                mCommand.ExecuteNonQuery();
                Console.WriteLine(mCommand.CommandText);
                mConnection.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)  //회원결제
        {
            if (!((textBox5.Text.Equals("0")) || (textBox5.Text.Equals("1")) || (textBox5.Text.Equals("2")) || (textBox5.Text.Equals("3"))
                || (textBox5.Text.Equals("4")) || (textBox5.Text.Equals("5")) || (textBox5.Text.Equals("6"))))
            {
                MessageBox.Show("할부 개월은 0개월부터 최대 6개월까지 가능합니다.");
            }
            else
            {
                mConnection.Open(); // DB 오픈
                mCommand.CommandText = "delete from 주문 where(주문번호='" + find_order.order_num + "') and (매장번호='" + Form1.mem_num2 + "') and (총주문금액 ='" + total + "'); "
                                + "insert 결제 values ('" + find_order.order_num + "', '" + Form1.mem_num2 + "', '" + textBox1.Text + "', " + total + ", " + textBox5.Text + "," + total * (1 - dis_rate) + ");"
                                +"UPDATE 회원 SET `연간결제횟수` = 연간결제횟수+1 WHERE (`회원번호` = '"+textBox1.Text+"');"
                                + "UPDATE 포인트 SET `누적포인트` = '1000' + "+ total * (1 - dis_rate)*0.01 + " WHERE(`회원번호` = '"+textBox1.Text+"');"
                                + "UPDATE 회원이력, 포인트 SET 회원이력.회원등급이력 = '일반', 회원이력.반영날짜 = CURDATE() where(회원이력.회원번호 = 포인트.회원번호) AND(포인트.누적포인트 >= 1400); "
                                +"UPDATE 회원이력, 포인트 SET 회원이력.회원등급이력 = '실버', 회원이력.반영날짜 = CURDATE() where(회원이력.회원번호 = 포인트.회원번호) AND(포인트.누적포인트 >= 2000); "
                                +"UPDATE 회원이력, 포인트 SET 회원이력.회원등급이력 = '골드', 회원이력.반영날짜 = CURDATE() where(회원이력.회원번호 = 포인트.회원번호) AND(포인트.누적포인트 >= 3000); ";
                MessageBox.Show("회원결제로 " + total * (1 - dis_rate) + " 원, 할부 " + textBox5.Text + "개월로 결제되었습니다.");
                mCommand.ExecuteNonQuery();
                Console.WriteLine(mCommand.CommandText);
                mConnection.Close();
            }
        }

        

        private void button2_Click(object sender, EventArgs e)  //닫기
        {
            this.Close();
        }

        
    }
}
