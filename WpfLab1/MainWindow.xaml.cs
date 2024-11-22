using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WpfLab1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Dictionary<string, string> matrix;
        Dictionary<string, int> matrixNew;
        List<Elements> matrixList;
        Random random;
        int[,] matrixRandom;
        public MainWindow()
        {
            InitializeComponent();

            random = new Random();
            matrixRandom = new int[10, 5];
            matrixNew = new Dictionary<string, int>();
            matrix = new Dictionary<string, string>()
            {
                { "tb00", tb00.Text },{ "tb01", tb01.Text },{ "tb02", tb02.Text },{ "tb03", tb03.Text },{ "tb04", tb04.Text },
                { "tb10", tb10.Text },{ "tb11", tb11.Text },{ "tb12", tb12.Text },{ "tb13", tb13.Text },{ "tb14", tb14.Text },
                { "tb20", tb20.Text },{ "tb21", tb21.Text },{ "tb22", tb22.Text },{ "tb23", tb23.Text },{ "tb24", tb24.Text },
                { "tb30", tb30.Text },{ "tb31", tb31.Text },{ "tb32", tb32.Text },{ "tb33", tb33.Text },{ "tb34", tb34.Text },
                { "tb40", tb40.Text },{ "tb41", tb41.Text },{ "tb42", tb42.Text },{ "tb43", tb43.Text },{ "tb44", tb44.Text },
                { "tb50", tb50.Text },{ "tb51", tb51.Text },{ "tb52", tb52.Text },{ "tb53", tb53.Text },{ "tb54", tb54.Text },
                { "tb60", tb60.Text },{ "tb61", tb61.Text },{ "tb62", tb62.Text },{ "tb63", tb63.Text },{ "tb64", tb64.Text },
                { "tb70", tb70.Text },{ "tb71", tb71.Text },{ "tb72", tb72.Text },{ "tb73", tb73.Text },{ "tb74", tb74.Text },
                { "tb80", tb80.Text },{ "tb81", tb81.Text },{ "tb82", tb82.Text },{ "tb83", tb83.Text },{ "tb84", tb84.Text },
                { "tb90", tb90.Text },{ "tb91", tb91.Text },{ "tb92", tb92.Text },{ "tb93", tb93.Text },{ "tb94", tb94.Text },
            };

            matrixList = new List<Elements>
            {
                new Elements(tb00),new Elements(tb01),new Elements(tb02),new Elements(tb03),new Elements(tb04),
                new Elements(tb10),new Elements(tb11),new Elements(tb12),new Elements(tb13),new Elements(tb14),
                new Elements(tb20),new Elements(tb21),new Elements(tb22),new Elements(tb23),new Elements(tb24),
                new Elements(tb30),new Elements(tb31),new Elements(tb32),new Elements(tb33),new Elements(tb34),
                new Elements(tb40),new Elements(tb41),new Elements(tb42),new Elements(tb43),new Elements(tb44),
                new Elements(tb50),new Elements(tb51),new Elements(tb52),new Elements(tb53),new Elements(tb54),
                new Elements(tb60),new Elements(tb61),new Elements(tb62),new Elements(tb63),new Elements(tb64),
                new Elements(tb70),new Elements(tb71),new Elements(tb72),new Elements(tb73),new Elements(tb74),
                new Elements(tb80),new Elements(tb81),new Elements(tb82),new Elements(tb83),new Elements(tb84),
                new Elements(tb90),new Elements(tb91),new Elements(tb92),new Elements(tb93),new Elements(tb94),

            };
        }

        private void bt_Click(object sender, RoutedEventArgs e)
        {
            // Заполняем матрицу случайными числами от -10 до 10
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    matrixRandom[i, j] = random.Next(-10, 11); // Генерация случайного числа от -10 до 10
                }
            }

            // Заполняем словарь значениями из массива A
            for (int i = 0; i < matrixRandom.GetLength(0); i++) // Проходим по строкам
            {
                for (int j = 0; j < matrixRandom.GetLength(1); j++) // Проходим по столбцам
                {
                    // Создаем ключ в формате "строка,столбец"
                    string key = $"tb{i}{j}";
                    // Записываем значение в словарь
                    matrixNew[key] = matrixRandom[i, j];
                }
            }
            foreach(var item in matrixNew)
            {
                    matrix[item.Key] = item.Value.ToString();
            }

            FillMatrix();
            FillList();
            
            Brush br = null;
            foreach (Elements el in matrixList)
            {
                    el.TextBoxRef.Background = br;
            }


        }

        private void btCalculate_Click(object sender, RoutedEventArgs e)
        {
            // Создаем одномерный массив N для хранения количества положительных элементов
            int[] N = new int[10];

            GetDataMatrix();
            
            int count = 0;
            foreach (var item in matrix)
            {
                count++;
                if (count > 5 )
                {
                    consoleRight.Text += "\r\n";
                    count = 1;
                }
                consoleRight.Text += $"{item.Key}:{item.Value} ";
            }
            consoleRight.Text += "\r\n\r\n";

            


            FillList();

            int number;
            int countIt = 0;
            int countEl = 0;
            Brush br = null;
            foreach (Elements el in matrixList)
            {
                try
                {
                    number = int.Parse(el.Chislo);

                }
                catch (Exception)
                {
                    countIt++;
                    continue;
                }
                if (number > 0)
                {
                    el.TextBoxRef.Background = Brushes.Salmon;
                    if(countIt < 5)
                    {
                        N[countEl]++;

                    }
                }
                else
                    el.TextBoxRef.Background = br ;

                countIt++;
                if (countIt == 5)
                {
                    countEl++;
                    countIt = 0;
                }
            }

            // Выводим массив N
            consoleRight.Text += "\nКоличество положительных элементов в каждой строке (массив N):\r\n";
            for (int i = 0; i < N.Length; i++)
            {
                consoleRight.Text += $"Строка {i}: {N[i]}\r\n";
            }


        }

        private void btClearConsole_Click(object sender, RoutedEventArgs e)
        {
            consoleRight.Clear();
        }

        public void Output()
        {
            int count = 0;
            foreach (var item in matrix)
            {
                count++;
                if (count > 5)
                {
                    consoleRight.Text += "\r\n";
                    count = 1;
                }
                consoleRight.Text += $"{item.Key}:{item.Value} ";
            }
            consoleRight.Text += "\r\n";
        }

        public void FillMatrix()
        {
            tb00.Text = matrix["tb00"];
            tb01.Text = matrix["tb01"];
            tb02.Text = matrix["tb02"];
            tb03.Text = matrix["tb03"];
            tb04.Text = matrix["tb04"];
            tb10.Text = matrix["tb10"];
            tb11.Text = matrix["tb11"];
            tb12.Text = matrix["tb12"];
            tb13.Text = matrix["tb13"];
            tb14.Text = matrix["tb14"];
            tb20.Text = matrix["tb20"];
            tb21.Text = matrix["tb21"];
            tb22.Text = matrix["tb22"];
            tb23.Text = matrix["tb23"];
            tb24.Text = matrix["tb24"];
            tb30.Text = matrix["tb30"];
            tb31.Text = matrix["tb31"];
            tb32.Text = matrix["tb32"];
            tb33.Text = matrix["tb33"];
            tb34.Text = matrix["tb34"];
            tb40.Text = matrix["tb40"];
            tb41.Text = matrix["tb41"];
            tb42.Text = matrix["tb42"];
            tb43.Text = matrix["tb43"];
            tb44.Text = matrix["tb44"];
            tb50.Text = matrix["tb50"];
            tb51.Text = matrix["tb51"];
            tb52.Text = matrix["tb52"];
            tb53.Text = matrix["tb53"];
            tb54.Text = matrix["tb54"];
            tb60.Text = matrix["tb60"];
            tb61.Text = matrix["tb61"];
            tb62.Text = matrix["tb62"];
            tb63.Text = matrix["tb63"];
            tb64.Text = matrix["tb64"];
            tb70.Text = matrix["tb70"];
            tb71.Text = matrix["tb71"];
            tb72.Text = matrix["tb72"];
            tb73.Text = matrix["tb73"];
            tb74.Text = matrix["tb74"];
            tb80.Text = matrix["tb80"];
            tb81.Text = matrix["tb81"];
            tb82.Text = matrix["tb82"];
            tb83.Text = matrix["tb83"];
            tb84.Text = matrix["tb84"];
            tb90.Text = matrix["tb90"];
            tb91.Text = matrix["tb91"];
            tb92.Text = matrix["tb92"];
            tb93.Text = matrix["tb93"];
            tb94.Text = matrix["tb94"];
        }
        public void GetDataMatrix()
        {
            matrix["tb00"] = tb00.Text;
            matrix["tb01"] = tb01.Text;
            matrix["tb02"] = tb02.Text;
            matrix["tb03"] = tb03.Text;
            matrix["tb04"] = tb04.Text;
            matrix["tb10"] = tb10.Text;
            matrix["tb11"] = tb11.Text;
            matrix["tb12"] = tb12.Text;
            matrix["tb13"] = tb13.Text;
            matrix["tb14"] = tb14.Text;
            matrix["tb20"] = tb20.Text;
            matrix["tb21"] = tb21.Text;
            matrix["tb22"] = tb22.Text;
            matrix["tb23"] = tb23.Text;
            matrix["tb24"] = tb24.Text;
            matrix["tb30"] = tb30.Text;
            matrix["tb31"] = tb31.Text;
            matrix["tb32"] = tb32.Text;
            matrix["tb33"] = tb33.Text;
            matrix["tb34"] = tb34.Text;
            matrix["tb40"] = tb40.Text;
            matrix["tb41"] = tb41.Text;
            matrix["tb42"] = tb42.Text;
            matrix["tb43"] = tb43.Text;
            matrix["tb44"] = tb44.Text;
            matrix["tb50"] = tb50.Text;
            matrix["tb51"] = tb51.Text;
            matrix["tb52"] = tb52.Text;
            matrix["tb53"] = tb53.Text;
            matrix["tb54"] = tb54.Text;
            matrix["tb60"] = tb60.Text;
            matrix["tb61"] = tb61.Text;
            matrix["tb62"] = tb62.Text;
            matrix["tb63"] = tb63.Text;
            matrix["tb64"] = tb64.Text;
            matrix["tb70"] = tb70.Text;
            matrix["tb71"] = tb71.Text;
            matrix["tb72"] = tb72.Text;
            matrix["tb73"] = tb73.Text;
            matrix["tb74"] = tb74.Text;
            matrix["tb80"] = tb80.Text;
            matrix["tb81"] = tb81.Text;
            matrix["tb82"] = tb82.Text;
            matrix["tb83"] = tb83.Text;
            matrix["tb84"] = tb84.Text;
            matrix["tb90"] = tb90.Text;
            matrix["tb91"] = tb91.Text;
            matrix["tb92"] = tb92.Text;
            matrix["tb93"] = tb93.Text;
            matrix["tb94"] = tb94.Text;
        }
        public void FillList()
        {
            foreach (var el in matrixList)
            {
                foreach (var mat in matrix)
                {
                    if (el.TextBoxRef.Name == mat.Key)
                    {
                        el.Name = mat.Key;
                        el.Chislo = mat.Value;
                    }
                }
            }
        }
    }
    public class Elements
    {
        public TextBox TextBoxRef;
        public string Name;
        public string Chislo;

        public Elements()
        {

        }
        public Elements(TextBox textBoxRef)
        {
            TextBoxRef = textBoxRef;
        }

        public Elements(TextBox textBoxRef, string name, string chislo)
        {
            TextBoxRef = textBoxRef;
            Name = name;
            Chislo = chislo; ;
        }
    }
}