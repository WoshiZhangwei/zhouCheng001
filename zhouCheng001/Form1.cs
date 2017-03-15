using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace zhouCheng001
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int plan = 0; //表示采用哪个方案，1=方案1,2=方案2；1.每次更换一个，2.每次全部更换

        int makeNewTime() //生成新时间
        {
            int newTime=0;
            Random rand = new Random();
            int suijishu = rand.Next(0,100);

            if (suijishu >0&&suijishu<=10) { newTime = 1000; }
            if (suijishu >10 && suijishu <= 23) { newTime = 1100; }
            if (suijishu > 23 && suijishu <=48) { newTime = 1200; }
            if (suijishu > 48 && suijishu <=61) { newTime = 1300; }
            if (suijishu > 61 && suijishu <= 70) { newTime = 1400; }
            if (suijishu > 70 && suijishu <= 82) { newTime = 1500; }
            if (suijishu > 82 && suijishu <= 84) { newTime = 1600; }
            if (suijishu > 84 && suijishu <= 90) { newTime = 1700; }
            if (suijishu > 90 && suijishu <= 95) { newTime = 1800; }
            if (suijishu > 95 && suijishu <= 100) { newTime = 1900; }
            return newTime*60;
        }

        int ArriveTime() //维修人员到达时间延迟
        {
            int arrive = 0;
            Random rand = new Random();
            int randnumber  = rand.Next(0, 10);
            if (randnumber > 0 && randnumber <= 6) { arrive = 5; }
            if (randnumber > 6 && randnumber <= 9) { arrive = 10; }
            if (randnumber > 9 && randnumber <= 10) { arrive = 15; }
            return arrive;
        }

        int RepariTime() // 更换时间=延迟时间+更换时间
        {
            int repariTime = 0;
            //int arriveTime = ArriveTime();
            if (plan == 1)
            {
                repariTime = ArriveTime() + 20;
            }
            if (plan == 2)
            {
                repariTime = ArriveTime() + 40;
            }
            return repariTime;
        }

        int RepariCost()   // 更换成本
        {
            int repariCost = 0;
            if (plan == 1)
            {
                repariCost = 32+RepariTime()*10+30*20;
            }
            if (plan == 2)
            {
                repariCost = 32 * 3 + RepariTime()* 10 + 30 * 40;
            }
            return repariCost;
        }


       int  Plan1()
        {
            plan = 1;
            int sumCost1 = 0; //方案一总成本
            int timeA = 1400 * 60;
            int timeB = 1500 * 60;
            int timeC = 1100 * 60;
            for (int i = 0; i < 1200000; i++)
            {
                timeA -= 1;
                timeB -= 1;
                timeC -= 1;
                if (timeA == 0 || timeB == 0 || timeC == 0)
                {
                    sumCost1 += RepariCost();
                    i += RepariTime();
                    if (timeA == 0) { timeA = makeNewTime(); }
                    if (timeB == 0) { timeB = makeNewTime(); }
                    if (timeC == 0) { timeC = makeNewTime(); }
                }
            }
            return sumCost1;
        }

        int Plan2()
        {
            plan = 2;
            int sumCost2 = 0;//方案二总成本
            int timeA = 1400 * 60;
            int timeB = 1500 * 60;
            int timeC = 1100 * 60;
            for (int i = 0; i < 1200000; i++)
            {
                timeA -= 1;
                timeB -= 1;
                timeC -= 1;
                if (timeA == 0 || timeB == 0 || timeC == 0)
                {
                    sumCost2 += RepariCost();
                    i += RepariTime();
                    timeA = makeNewTime();
                    timeB = makeNewTime();
                    timeC = makeNewTime();
                }
            }
            return sumCost2;
        }


        private void button1_Click(object sender, EventArgs e)
        {
            //label1.Text = Plan1().ToString();

            int sum1 = 0;
            int xianshi1 = 0;
            Pen p1 = new Pen(Color.Blue, 1);
            Graphics g = this.CreateGraphics();
            int yOld = Plan1();
            for (int i = 1; i < 500; i++)
            {
                sum1 += Plan1();

                int y = 60000-Plan1();
                g.DrawLine(p1, i, y / 200, i-1, yOld/ 200);
                yOld = y;
            }
            g.DrawLine(p1, 0, 300, 500, 300);

            xianshi1 = sum1 / 500;
            label1.Text = xianshi1.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            // label2.Text = Plan2().ToString();

            int sum2 = 0;
            int xianshi2 = 0;

            Pen p1 = new Pen(Color.Red, 1);
            Graphics g = this.CreateGraphics();
            int yOld = Plan2();
            for (int i = 1; i < 500; i++)
            {
                sum2 += Plan2();
                int y = 60000 - Plan2();
                g.DrawLine(p1, i, y / 200, i - 1, yOld / 200);
                yOld = y;
            }

            xianshi2 = sum2 / 500;
            label2.Text = xianshi2.ToString();

        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
