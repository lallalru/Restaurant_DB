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
    public partial class no_show : Form
    {
        public static String url = "SERVER=LOCALHOST; USER=jy; DATABASE=restaurnt_schema;" + "PORT=3306; PASSWORD=1234; SSLMODE=NONE";

        private MySqlConnection mConnection; // DB접속
        private MySqlCommand mCommand; // 쿼리문
        private MySqlDataReader mDataReader; // 실행문

        DataTable table = new DataTable();

        public no_show()
        {
            InitializeComponent();
            table.Columns.Add("예약번호", typeof(string));
            table.Columns.Add("방문일자", typeof(string));
            table.Columns.Add("방문시간", typeof(string));
            table.Columns.Add("방문인원", typeof(string));
            table.Columns.Add("회원번호", typeof(string));
            dataGridView1.DataSource = table;
        }

        private void no_show_Load(object sender, EventArgs e)
        {
            label1.Text += "매장번호 "+Form1.mem_num2+" 의 예약내역";
            mConnection = new MySqlConnection(url); // DB접속
            mCommand = new MySqlCommand(); // 쿼리문 생성
            mCommand.Connection = mConnection; // DB에 연결
            
            mConnection.Open(); // DB 오픈
            mCommand.CommandText = "SELECT * FROM 예약 where (방문매장='" + Form1.mem_num2 + "');"; // 쿼리문 작성
            mDataReader = mCommand.ExecuteReader(); // 쿼리문 실행

            while (mDataReader.Read()) // 전부 다 읽어 옴
            {
                table.Rows.Add(mDataReader["예약번호"].ToString(), mDataReader["방문일자"].ToString(), mDataReader["방문시간"].ToString()
                               , mDataReader["방문인원"].ToString(), mDataReader["회원번호"].ToString());

            }

            mConnection.Close(); // 사용 후 객체 닫기
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String noshow_num = (dataGridView1.Rows[this.dataGridView1.CurrentCellAddress.Y].Cells[0].Value.ToString());

            if(Convert.ToInt32(dataGridView1.Rows[this.dataGridView1.CurrentCellAddress.Y].Cells[2].Value.ToString()) <= Convert.ToInt32(DateTime.Now.AddMinutes(30).ToString("HHmm"))
            && Convert.ToInt32(dataGridView1.Rows[this.dataGridView1.CurrentCellAddress.Y].Cells[1].Value.ToString()) <= Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")))
            {
                mConnection.Open(); // DB 오픈
                mCommand.CommandText = "delete from 예약 where(예약번호='" + dataGridView1.Rows[this.dataGridView1.CurrentCellAddress.Y].Cells[0].Value.ToString() 
                                + "') and (회원번호='" + dataGridView1.Rows[this.dataGridView1.CurrentCellAddress.Y].Cells[4].Value.ToString() + "'); "
                                + "UPDATE 회원 SET No_Show = No_Show + 1 WHERE (회원번호 = '" + dataGridView1.Rows[this.dataGridView1.CurrentCellAddress.Y].Cells[4].Value.ToString() + "');";
                mCommand.ExecuteNonQuery();
                Console.WriteLine(mCommand.CommandText);
                mConnection.Close();

                MessageBox.Show("No-Show 처리 완료되었습니다.");
            }
            else
            {
                MessageBox.Show("아직 방문시간으로부터 30분이 지나지 않아 No-Show 처리를 할 수 없습니다.");
            }
            Console.WriteLine(Convert.ToInt32(dataGridView1.Rows[this.dataGridView1.CurrentCellAddress.Y].Cells[2].Value.ToString()) +"랑"+ Convert.ToInt32(DateTime.Now.AddMinutes(30).ToString("HHmm"))
            + "랑" + Convert.ToInt32(dataGridView1.Rows[this.dataGridView1.CurrentCellAddress.Y].Cells[1].Value.ToString()) + "랑" + Convert.ToInt32(DateTime.Now.ToString("yyyyMMdd")));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
