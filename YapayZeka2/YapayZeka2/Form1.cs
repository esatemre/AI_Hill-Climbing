using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YapayZeka2
{
    public partial class Form1 : Form
    {
        private Panel[,] _chessBoardPanels;// Dama şeklinde boyamak için panel matrisi
        private byte[,] _matrix;
        private const int tileSize = 40;
        private const int gridSize = 9;//9x9 tahta
        private int vezir = 0;
        private int at = 0;
        private List<Tuple<Point, Point>> map;
        enum DirectionType { Row = 1, Column = 2 };
        /// <summary>
        /// The offsets for knight moves from a 0,0 point
        /// </summary>
        static readonly List<Point> positions = new List<Point>()
        {
            new Point(){x=0, y=0},
            new Point(){x=-1, y=-2},
            new Point(){x=-2, y=-1},
            new Point(){x=-2, y= 1},
            new Point(){x=-1, y= 2},
            new Point(){x= 1, y= 2},
            new Point(){x= 2, y= 1},
            new Point(){x= 2, y=-1},
            new Point(){x= 1, y=-2},
        };

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _matrix = new byte[gridSize, gridSize];
            Array.Clear(_matrix, 0, gridSize);
            map = new List<Tuple<Point, Point>>();
            btnRun.Enabled = false;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Paint();
        }

        /// <summary>
        /// dama şekli oluşturmak için fonk
        /// </summary>
        private void Paint()
        {
            var clr1 = Color.DarkGray;
            var clr2 = Color.White;

            // initialize the "chess board"
            _chessBoardPanels = new Panel[gridSize, gridSize];

            // double for loop to handle all rows and columns
            for (var n = 0; n < gridSize; n++)
            {
                for (var m = 0; m < gridSize; m++)
                {
                    // create new Panel control which will be one
                    // chess board tile
                    var newPanel = new Panel
                    {
                        Name = Convert.ToString(n + "_" + m),
                        Size = new Size(tileSize, tileSize),
                        Location = new System.Drawing.Point(tileSize * n, tileSize * m)
                    };
                    //newPanel.Click += newPanel_Click;
                    // add to Form's Controls so that they show up
                    panel1.Controls.Add(newPanel);

                    // add to our 2d array of panels for future use
                    _chessBoardPanels[n, m] = newPanel;

                    // color the backgrounds
                    if (n % 2 == 0)
                        newPanel.BackColor = m % 2 != 0 ? clr1 : clr2;
                    else
                        newPanel.BackColor = m % 2 != 0 ? clr2 : clr1;
                }
            }
        }

        /// <summary>
        /// yerleştir
        /// </summary>
        private void btnPut_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            Paint();
            _matrix = new byte[gridSize, gridSize];
            Array.Clear(_matrix, 0, gridSize);
            map = new List<Tuple<Point, Point>>();
            if (GetNumbers())
            {
                int tmp = 0;
                /**Random sekilde yerlestir**/
                Random rnd = new Random();
                for (int i = 0; i < at + vezir; i++)
                {
                    tmp = i % gridSize;
                    byte y = (byte)rnd.Next(0, gridSize);
                    while (_matrix[tmp, y] != 0)
                    {
                        y = (byte)rnd.Next(0, gridSize);
                    }
                    Panel pnl = (Panel)panel1.Controls.Find(tmp + "_" + y, true).First();
                    if (i < vezir)
                    {
                        _matrix[tmp, y] = 1;
                        pnl.Controls.Add(new Label() { Text = "VEZIR" });
                    }
                    else
                    {
                        _matrix[tmp, y] = 2;
                        pnl.Controls.Add(new Label() { Text = "AT" });
                    }
                }
                int totalThreats = GetTotalThreat();
                txtSon.Text = "Başlangıç Toplam Tehdit: " + totalThreats.ToString();
                btnRun.Enabled = true;
            }
        }

        /// <summary>
        /// çalıştır
        /// </summary>
        private void btnRun_Click(object sender, EventArgs e)
        {
            DateTime time1 = DateTime.Now;
            int iter = 0; //iterasyon sayisi
            int localMaxDetect = 0; //local Max tespit için
            while (localMaxDetect < (vezir + at)) /**hill climbing alg. göre cöz**/
            {
                SortedDictionary<byte, List<Point>> points = GetNodes(); //tahta üzerindeki taşları ele aldık
                foreach (List<Point> array in points.Values)
                {
                    foreach (Point item in array)
                    {
                        List<KeyValuePair<int, Point>> sorted = new List<KeyValuePair<int, Point>>();
                        byte type = _matrix[item.x, item.y]; //at mı vezir mi
                        int minVal = GetTotalThreat(); //şu anki toplam tehdit sayısı
                        _matrix[item.x, item.y] = 0;
                        for (int i = 0; i < gridSize; i++)
                        {
                            if (_matrix[item.x, i] == 0)
                            {
                                _matrix[item.x, i] = type;
                                sorted.Add(new KeyValuePair<int, Point>(GetTotalThreat(), new Point(item.x, i)));
                                _matrix[item.x, i] = 0; // mümkün nokta için dene ve tehdit sayısıyla listeye ekle
                            }
                        }
                        int tmp = sorted.Min(p => p.Key);
                        if (tmp < minVal) // eğer mevcut durumdan düşük durum bulunduysa ona geç, yoksa lokal max olabilir
                        {
                            Point pTmp = sorted.Where(p => p.Key == tmp).First().Value;
                            _matrix[pTmp.x, pTmp.y] = type;
                            localMaxDetect = 0;
                        }
                        else
                        {
                            _matrix[item.x, item.y] = type;
                            localMaxDetect++;
                        }
                        iter++;
                        txtSon.Text += System.Environment.NewLine + iter + ".Iterasyon Toplam Tehdit: " + GetTotalThreat().ToString();
                    }
                }

            }
            /**Algoritma bitti**/
            DateTime time2 = DateTime.Now;
            TimeSpan time = (time2 - time1);
            txtTime.Text = Decimal.Parse(time.Seconds.ToString()) + Decimal.Parse(time.Milliseconds.ToString()) / 1000 + " sec";
            txtMax.Text = (GC.GetTotalMemory(true) / 1024).ToString() + " K";
            btnRun.Enabled = false;
            panel1.Controls.Clear();
            Paint();
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    if (_matrix[i, j] != 0)
                    {
                        Panel pnl = (Panel)panel1.Controls.Find(i + "_" + j, true).First();
                        if (_matrix[i, j] == 1)
                        {
                            pnl.Controls.Add(new Label() { Text = "VEZIR" });
                        }
                        else
                        {
                            pnl.Controls.Add(new Label() { Text = "AT" });
                        }
                    }
                }
            }
        }

        /// <summary>
        /// inputları al
        /// </summary>
        private bool GetNumbers()
        {
            try
            {
                vezir = Byte.Parse(txtVez.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Gecerli bir vezir sayısı giriniz!");
                return false;
            }
            try
            {
                at = Byte.Parse(txtAt.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Gecerli bir at sayısı giriniz!");
                return false;
            }
            return true;
        }

        /// <summary>
        /// matristeki at ve vezirleri bul
        /// </summary>
        private SortedDictionary<byte, List<Point>> GetNodes()
        {
            SortedDictionary<byte, List<Point>> points = new SortedDictionary<byte, List<Point>>();
            for (byte i = 0; i < gridSize; i++)
            {
                for (byte j = 0; j < gridSize; j++)
                {
                    if (_matrix[i, j] != 0)
                    {
                        List<Point> tmpList = new List<Point>();
                        if (points.TryGetValue(_matrix[i, j], out tmpList))
                        {
                            tmpList.Add(new Point(i, j));
                        }
                        else
                        {
                            tmpList = new List<Point>();
                            tmpList.Add(new Point(i, j));
                            points.Add(_matrix[i, j], tmpList);
                        }
                    }
                }
            }
            return points;
        }

        /// <summary>
        /// matristeki toplam tehdit sayısını dön
        /// </summary>
        private int GetTotalThreat()
        {
            map = new List<Tuple<Point, Point>>(); // tehditler birbiriyle örtüşmesin diye bir mapper.
            int totalThreats = 0;
            for (byte i = 0; i < gridSize; i++)
                for (byte j = 0; j < gridSize; j++)
                    if (_matrix[i, j] != 0)
                        totalThreats = totalThreats + GetThreats(i, j, _matrix[i, j]);// toplam.tehdit.sayisini.bul**/

            return totalThreats; //tahtadaki her taş için tehdit bulunup toplanır.
        }

        /// <summary>
        /// bir eleman için tehdit sayısını dön
        /// </summary>
        private int GetThreats(byte x, byte y, byte type)
        {
            /**vezir tehditlerini bul**/
            int threatCount = 0;
            List<Point> list = new List<Point>();
            if (type == 1)
            {
                int i;
                for (i = 0; i < gridSize; i++)
                {
                    if (isOk(DirectionType.Row, x, y, i, list))
                    {
                        threatCount++;
                        list.Add(new Point(i, y));
                        map.Add(new Tuple<Point, Point>(new Point(x, y), new Point(i, y)));
                    }
                    if (isOk(DirectionType.Column, x, y, i, list))
                    {
                        threatCount++;
                        list.Add(new Point(x, i));
                        map.Add(new Tuple<Point, Point>(new Point(x, y), new Point(x, i)));
                    }
                }
                int j;
                for (i = x, j = y; i >= 0 && j >= 0; i--, j--)
                {
                    if (isOkForCross(x, y, i, j, list, 1))
                    {
                        threatCount++;
                        list.Add(new Point(i, j));
                        map.Add(new Tuple<Point, Point>(new Point(x, y), new Point(i, j)));
                    }
                }
                for (i = x, j = y; i >= 0 && j < gridSize; i--, j++)
                {
                    if (isOkForCross(x, y, i, j, list, 1))
                    {
                        threatCount++;
                        list.Add(new Point(i, j));
                        map.Add(new Tuple<Point, Point>(new Point(x, y), new Point(i, j)));
                    }
                }
                for (i = x, j = y; i < gridSize && j < gridSize; i++, j++)
                {
                    if (isOkForCross(x, y, i, j, list, 1))
                    {
                        threatCount++;
                        list.Add(new Point(i, j));
                        map.Add(new Tuple<Point, Point>(new Point(x, y), new Point(i, j)));
                    }
                }
                for (i = x, j = y; i < gridSize && j >= 0; i++, j--)
                {
                    if (isOkForCross(x, y, i, j, list, 1))
                    {
                        threatCount++;
                        list.Add(new Point(i, j));
                        map.Add(new Tuple<Point, Point>(new Point(x, y), new Point(i, j)));
                    }
                }
            }
            if (type == 2)
            {
                /**asagida at tehditlerini bul**/
                foreach (Point item in positions)
                {
                    Point p = new Point(item.x + x, item.y + y);
                    if (p.x >= 0 && p.y >= 0 && p.x < gridSize && p.y < gridSize)
                    {
                        if (isOkForCross(x, y, p.x, p.y, list, 2))
                        {
                            threatCount++;
                            list.Add(new Point(p.x, p.y));
                            map.Add(new Tuple<Point, Point>(new Point(x, y), new Point(p.x, p.y)));
                        }
                    }
                }
            }
            return threatCount;
        }

        /// <summary>
        /// yan ve dikey tehditler bakılabilir mi?
        /// </summary>
        private bool isOk(DirectionType type, int x, int y, int i, List<Point> list)
        {
            if ((int)type == 1)
            {
                if (_matrix[i, y] != 0)
                {
                    if (!list.Any(p => p.x == i && p.y == y))
                    {
                        if (map.Where(p => p.Item1.y == y && p.Item2.y == y && ((p.Item1.x == i && p.Item2.x == x)) || (p.Item1.x == x && p.Item2.x == i)).ToList().Count == 0)
                        {
                            return true;
                        }
                    }
                }
            }
            else if ((int)type == 2)
            {
                if (_matrix[x, i] != 0)
                {
                    if (!list.Any(p => p.x == x && p.y == i))
                    {
                        if (map.Where(p => p.Item1.x == x && p.Item2.x == x && ((p.Item1.y == i && p.Item2.y == y)) || (p.Item1.y == y && p.Item2.y == i)).ToList().Count == 0)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// çapraz tehditler bakılabilir mi?
        /// </summary>
        private bool isOkForCross(int x, int y, int i, int j, List<Point> list, byte type)
        {
            if (_matrix[i, j] != 0)
            {
                if (!list.Any(p => p.x == i && p.y == j))
                {
                    if (map.Where(p => p.Item1.x == x && p.Item1.y == y && p.Item2.x == i && p.Item2.y == j).ToList().Count == 0)
                    {
                        if (map.Where(p => p.Item1.x == i && p.Item1.y == j && p.Item2.x == x && p.Item2.y == y).ToList().Count == 0)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }


    }
}
