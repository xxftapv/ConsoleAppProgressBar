using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    class ProgressBar
    {

        public ProgressBar() { }
        private int _minimum = 0;
        private int _maximum = 50;
        private int _step = 2;
        private int _value;
        private int CurPercent = 1;
        /// <summary>
        /// 最小值
        /// </summary>
        public int Minimum { get { return _minimum; } set { _minimum = value; } }
        /// <summary>
        /// 最大值
        /// </summary>
        public int Maximum { get { return _maximum; } set { _maximum = value; } }
        public int Step { get { return _step; } set { _step = value; } }
        public int Value
        {
            get { return _value; }
            set
            {
                if (_minimum <= value && value <= (_maximum * _step))
                {
                    _value = value;
                    SetProgress(value);
                }
                else
                {
                    Console.WriteLine("");
                    Console.WriteLine("值超出了范围。");
                }
            }
        }
        protected static void WriteAt(string s, int x, int y)
        {
            try
            {
                int origRow = Console.CursorTop;
                int origCol = Console.CursorLeft;
                Console.SetCursorPosition(origCol + x, origRow + y);
                Console.Write(s);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }
        public void NewProgress()
        {
            ConsoleColor colorBack = Console.BackgroundColor;
            CurPercent = 1;
            //绘制背景界面
            Console.BackgroundColor = ConsoleColor.DarkCyan;
            for (int i = Minimum; i < Maximum; i++)
            {
                Console.Write("=");
            }
            //Console.WriteLine(" ");  
            Console.BackgroundColor = colorBack;
            Console.Write("{0}%", 0);
        }
        private void SetProgress(int value)
        {
            ConsoleColor colorBack = Console.BackgroundColor;
            ConsoleColor colorFore = Console.ForegroundColor;

            #region 绘制状态界面           
            // 开始控制进度条和进度变化
            for (int i = CurPercent; i <= value; i++)
            {
                //绘制进度条进度                 
                Console.BackgroundColor = ConsoleColor.Yellow;//设置进度条颜色                
                Console.SetCursorPosition(0, Console.CursorTop);
                int x = (i / Step);
                WriteAt(">", x, 0);
                Console.BackgroundColor = colorBack;//恢复输出颜色  

                //更新进度百分比,原理同上.                                                   
                Console.ForegroundColor = ConsoleColor.Green;
                Console.SetCursorPosition(Maximum, Console.CursorTop);
                Console.Write("{0}%", i);
                Console.ForegroundColor = colorFore;

            }
            CurPercent = value;

            #endregion


        }
    }
}
