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
        Random rand = new Random();
        int makeNewTime() //生成新时间
        {
            int newTime=0;
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


        int sumCost()  //计算总费用
        {
            int sumCost = 0;
            int timeA = 1400 * 60;
            int timeB = 1500 * 60;
            int timeC = 1100 * 60;
            for (int i = 0; i < 1200000; i++)
            {
                timeA -= 1;
                timeB -= 1;
                timeC -= 1;
                if (timeA <= 0 || timeB <= 0 || timeC <= 0)
                {
                    sumCost+= RepariCost();
                    i += RepariTime();
                    if (plan == 1)
                    {
                        if (timeA <= 0) { timeA = makeNewTime(); }
                        if (timeB <= 0) { timeB = makeNewTime(); }
                        if (timeC <= 0) { timeC = makeNewTime(); }
                    }
                    if (plan == 2)
                    {
                        timeA = makeNewTime();
                        timeB = makeNewTime();
                        timeC = makeNewTime();
                    }                  
                }
            }
            return sumCost;
        }

        int click()   //click时间发生
        {
            int sum = 0;
            int xianshi = 0;
            Pen p1 = new Pen(Color.Blue, 2);
            Pen p2 = new Pen(Color.Red,2);
            Graphics g = this.CreateGraphics();
            g.DrawLine(p1, 0, 300, 1000, 300);
            g.DrawLine(p1, 0, 1, 1000, 0);
            g.DrawLine(p1, 0, 1, 0, 300);
            g.DrawLine(p1, 1000, 0, 1000, 300);
            int yOld = sumCost();
            for (int i = 1; i < 1000; i++)
            {
                sum += sumCost();
                int y = 60000 - sumCost();
                if (plan == 1)
                {
                    g.DrawLine(p1, i, y / 200, i - 1, yOld / 200);
                }
                if (plan == 2)
                {
                    g.DrawLine(p2, i, y / 200, i - 1, yOld / 200);
                }
                yOld = y;
            }
            
            xianshi = sum / 1000;
            return xianshi;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            plan = 1;
            click();
            label1.Text = sumCost().ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            plan = 2;
            click();
            label2.Text = sumCost().ToString();
        }


        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
          
        }
    }
}
