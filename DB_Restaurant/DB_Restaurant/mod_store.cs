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
    public partial class mod_store : Form
    {
        public static String url = "SERVER=LOCALHOST; USER=jy; DATABASE=restaurnt_schema;" + "PORT=3306; PASSWORD=1234; SSLMODE=NONE";
        private MySqlConnection mConnection; // DB접속
        private MySqlCommand mCommand; // 쿼리문
        private MySqlDataReader mDataReader; // 실행문
        private int store_his;
        public mod_store()
        {
            InitializeComponent();
        }

        private void mod_store_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            mConnection = new MySqlConnection(url); // DB접속
            mCommand = new MySqlCommand(); // 쿼리문 생성
            mCommand.Connection = mConnection; // DB에 연결

            

            mConnection.Open(); // DB 오픈
            mCommand.CommandText = "select distinct count(매장이력순서) from 매장이력 where ( 매장번호 ='"+ store.select_store_num + "');"; // 쿼리문 작성
            mDataReader = mCommand.ExecuteReader(); // 쿼리문 실행

            while (mDataReader.Read()) // 전부 다 읽어 옴
            {
                store_his = Convert.ToInt32((mDataReader["count(매장이력순서)"].ToString()))+1;
            }
            Console.WriteLine(store_his + "매장정보이력횟수");
            mConnection.Close();



            if (!String.IsNullOrWhiteSpace(textBox1.Text))
            {
                mCommand.CommandText += "update 매장 set 매장주소= '"+textBox1.Text+"' where (매장번호='"+ store.select_store_num +"'); ";
                mCommand.CommandText += "insert 매장이력 values('" + store.select_store_num + "','"+store_his+"','','','','" + textBox1.Text + "',''); ";
                store_his++;
            }
            if (!String.IsNullOrWhiteSpace(textBox2.Text))
            {
                mCommand.CommandText += "update 매장 set 매장전화번호= '" + textBox2.Text + "' where (매장번호='" + store.select_store_num + "'); ";
                mCommand.CommandText += "insert 매장이력 values('" + store.select_store_num + "','" + store_his + "','','','', '','" + textBox2.Text + "'); ";
                store_his++;
            }
            if (!String.IsNullOrWhiteSpace(textBox3.Text))
            {
                mCommand.CommandText += "update 매장 set 테이블수= '" + textBox3.Text + "' where (매장번호='" + store.select_store_num + "'); ";
                mCommand.CommandText += "insert 매장이력 values('" + store.select_store_num + "','" + store_his + "', '" + textBox3.Text + "','','','',''); ";
                store_his++;
            }
            if (!String.IsNullOrWhiteSpace(textBox4.Text))
            {
                mCommand.CommandText += "update 매장 set 오픈시간= '" + textBox4.Text + "' where (매장번호='" + store.select_store_num + "'); ";
                mCommand.CommandText += "insert 매장이력 values('" + store.select_store_num + "','" + store_his + "','','" + textBox4.Text + "','','',''); ";
                store_his++;
            }
            if (!String.IsNullOrWhiteSpace(textBox5.Text))
            {
                mCommand.CommandText += "update 매장 set 마감시간= '" + textBox5.Text + "' where (매장번호='" + store.select_store_num + "'); ";
                mCommand.CommandText += "insert 매장이력 values('" + store.select_store_num + "','" + store_his + "','','', '" + textBox5.Text + "','',''); ";
                store_his++;
            }
            mConnection.Open(); // DB 오픈
            mCommand.ExecuteNonQuery();
            MessageBox.Show("매장 정보가 수정되었습니다.");
            mConnection.Close();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
