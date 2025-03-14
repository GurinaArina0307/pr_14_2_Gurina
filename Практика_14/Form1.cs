using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq;
using System.IO;

namespace Практика_14
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        public void Imput(int n)
        {
            Queue<int> st = new Queue<int>();

            for (int i = 1; i <= (int)numericUpDown1.Value; i++)
            {
                st.Enqueue(i);
            }
            label2.Text += $"Размерность : {st.Count}\n";
            label2.Text += $"Верхний элемент : {(int)numericUpDown1.Value}\n";
            label2.Text += $"Очередь : ";
            foreach (var i in st)
            {
                label2.Text += i + " ";
            }

            st.Clear();
            label2.Text += $"\nНовая размерность : {st.Count}";
        }


        private void button1_Click(object sender, EventArgs e)
        {
            int n = Convert.ToInt32((int)numericUpDown1.Value);
            Imput(n);

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

            string filePath = textBox1.Text;
            // Проверка на ввод файла
            if (string.IsNullOrWhiteSpace(filePath))
            {
                MessageBox.Show("Путь к файлу не указан.");
                return;
            }
            if (!File.Exists(filePath))
            {
                MessageBox.Show("Файл не найден.");
                return;
            }
            // Чтение всех строк из файла
            var lines = File.ReadAllLines(filePath);
            // Формирования списка людей
            var people = lines.Select(line => line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries))
                  .Where(parts => parts.Length == 5)
                  .Select(parts => new
                  {
                      LastName = parts[0],
                      FirstName = parts[1],
                      MiddleName = parts[2],
                      Age = int.TryParse(parts[3], out var age) ? age : (int?)null,
                      Weight = int.TryParse(parts[4], out var weight) ? weight : (int?)null
                  })
                  .Where(p => p.Age.HasValue && p.Weight.HasValue) // Исключаем некорректные записи
                  .Select(p => new
                  {
                      p.LastName,
                      p.FirstName,
                      p.MiddleName,
                      Age = p.Age.Value,
                      Weight = p.Weight.Value
                  });
            listBox1.Items.Clear();

            // Разделение людей на две группы
            var youngPeople = people.Where(p => p.Age < 40);
            var oldPeople = people.Where(p => p.Age >= 40);
            // Вывод информации о людях младше 40
            foreach (var person in youngPeople)
            {
                listBox1.Items.Add($"{person.LastName} {person.FirstName} {person.MiddleName}, " +
                $"Возраст: {person.Age}, Вес: {person.Weight}");
            }
            // Вывод информации о людях страше 40
            foreach (var person in oldPeople)
            {
                listBox1.Items.Add($"{person.LastName} {person.FirstName} {person.MiddleName}, " +
                $"Возраст: {person.Age}, Вес: {person.Weight}");
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Пути к файлам
            string namesFilePath = textBox2.Text; // Файл с ФИО
            string detailsFilePath = textBox3.Text; // Файл с возрастом и весом

            // Проверка существования файлов
            if (!File.Exists(namesFilePath) || !File.Exists(detailsFilePath))
            {
                MessageBox.Show("Один из файлов не найден.");
                return;
            }

            try
            {
                // Чтение данных из файлов
                var namesLines = File.ReadAllLines(namesFilePath);
                var detailsLines = File.ReadAllLines(detailsFilePath);

                // Объединение данных
                var people = namesLines.Zip(detailsLines, (nameLine, detailLine) => new
                {
                    NameParts = nameLine.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries),
                    DetailParts = detailLine.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                })
                .Where(x => x.NameParts.Length == 3 && x.DetailParts.Length == 2) // Проверка корректности данных
                .Select(x => new
                {
                    LastName = x.NameParts[0],
                    FirstName = x.NameParts[1],
                    MiddleName = x.NameParts[2],
                    Age = int.TryParse(x.DetailParts[0], out var age) ? age : 0,
                    Weight = int.TryParse(x.DetailParts[1], out var weight) ? weight : 0
                })
                .Where(x => x.Age > 0 && x.Weight > 0) // Исключение некорректных записей
                .OrderBy(x => x.Age) // Сортировка по возрасту
                .GroupBy(x => x.LastName[0]); // Группировка по первой букве фамилии

                // Вывод результатов
                listBox1.Items.Clear();
                foreach (var group in people)
                {
                    listBox2.Items.Add($"Буква: {group.Key}");
                    foreach (var person in group)
                    {
                        listBox2.Items.Add($"{person.LastName} {person.FirstName} {person.MiddleName}, " +
                                           $"Возраст: {person.Age}, Вес: {person.Weight}");
                    }
                    listBox2.Items.Add(""); // Пустая строка между группами
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка: {ex.Message}");
            }        
       }
    }
}
