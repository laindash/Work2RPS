using System.Data;


namespace Work2RPS {
    public partial class Work2 : Form
    {
        public Work2()
        {
            InitializeComponent();
            FormBorderStyle = FormBorderStyle.FixedSingle;

            // Запретить открытие на полный экран
            MaximizeBox = false;
            MessageBox.Show(GetHello(), "Приветствие", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        public static string GetHello()
        {
            string hello = "Егорова Ксения гр.425" + Environment.NewLine +
            "Контрольная работа номер 2" + Environment.NewLine +
            "Вариант 5" + Environment.NewLine +
            "Двоичный поиск заданного значения в упорядоченном массиве";
            return hello;
        }

        private void Btn_find_Click(object sender, EventArgs e)
        {
            Btn_find.Enabled = false;
            label_result.Text = "...";

            if (!double.TryParse(desNum_in.Text, out double desiredNumber))
            {
                MessageBox.Show("Введено некорректное значение элемента для поиска", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (desNum_in.Text != desiredNumber.ToString())
                {
                    desNum_in.Text = desiredNumber.ToString();
                    MessageBox.Show("Некорректные данные в значении элемента для поиска были исправлены.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                try
                {
                    // Удаление завершающих символов-разделителей (;) справа от строки
                    string valuesAsString = string.Join(";", array_input.Text.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries));

                    // Преобразование строковых значений в массив double
                    double[] array = valuesAsString.Split(';').Select(double.Parse).ToArray();

                    // Нахождение индексов элемента двоичным поиском
                    string indexes = ArrayFunctions.StartBinarySearch(array, desiredNumber, out double[] sortedArray);


                    string sortedValuesAsString = string.Join(";", sortedArray.Select(num => num.ToString()));

                    if (array_input.Text != sortedValuesAsString)
                    {
                        array_input.Text = sortedValuesAsString;
                        MessageBox.Show("Некорректные данные были исправлены. Массив упорядочен по возрастанию.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }


                    if (!string.IsNullOrEmpty(indexes))
                    {
                        label_result.Text = "Элемент " + desiredNumber.ToString() + " имеет индекс: " + indexes;
                    }
                    else
                    {
                        label_result.Text = "Элемент " + desiredNumber.ToString() + " в массиве отсутствует";
                    }
                }
                catch (FormatException)
                {
                    // Вывод сообщения об ошибке при некорректном вводе
                    MessageBox.Show("Ошибка ввода. Пожалуйста, убедитесь, что вводимые данные корректны.",
                        "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            Btn_find.Enabled = true;
        }


        private void array_input_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Разрешаем вводить только цифры, запятые и точки с запятой
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != ';' && e.KeyChar != '-')
            {
                e.Handled = true;
                return;
            }
            label_result.Text = "...";
        }

        private void desNum_in_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Разрешаем вводить только цифры и запятые
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '-')
            {
                e.Handled = true;
                return;
            }
            label_result.Text = "...";
        }

        private void Btn_input_initial_Click(object sender, EventArgs e)
        {
            Btn_input_initial.Enabled = false;
            label_result.Text = "...";

            // Диалог выбора файла
            OpenFileDialog file_open_dialog = new OpenFileDialog();
            file_open_dialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
            file_open_dialog.FilterIndex = 1;
            file_open_dialog.RestoreDirectory = true;

            if (file_open_dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Чтение данных из файла
                    string file_path = file_open_dialog.FileName;
                    string initial_desNum = File.ReadLines(file_path).First();
                    string initial_data = File.ReadLines(file_path).Last();

                    // Присвоение значения элемента для поиска
                    desNum_in.Text = initial_desNum;

                    // Присвоение значений массива
                    array_input.Text = initial_data;

                    MessageBox.Show($"Данные успешно загружены из файла {file_path}", "Загрузка", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка при загрузке данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            Btn_input_initial.Enabled = true;
        }


        private void Btn_save_original_Click(object sender, EventArgs e)
        {
            Btn_save_original.Enabled = false;

            try
            {
                // Преобразование исходного массива в строку
                string initialDataAsString = string.Join(";", array_input.Text.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries));

                // Диалог выбора места сохранения файла
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
                saveFileDialog.FilterIndex = 1;
                saveFileDialog.RestoreDirectory = true;

                string data = desNum_in.Text + Environment.NewLine + initialDataAsString;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Получение пути к выбранному файлу
                    string file_path = saveFileDialog.FileName;

                    // Запись данных в файл
                    File.WriteAllText(file_path, data);
                    MessageBox.Show($"Данные успешно сохранены в файл {file_path}", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (FormatException)
            {
                // Вывод сообщения об ошибке при некорректном вводе
                MessageBox.Show("Ошибка ввода. Пожалуйста, убедитесь, что вводимые данные корректны.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Произошла ошибка при сохранении данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Btn_save_original.Enabled = true;
        }


        private void Btn_save_result_Click(object sender, EventArgs e)
        {
            Btn_save_result.Enabled = false;

            if (!double.TryParse(desNum_in.Text, out double desiredNumber))
            {
                MessageBox.Show("Введено некорректное значение элемента для поиска", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (desNum_in.Text != desiredNumber.ToString())
                {
                    desNum_in.Text = desiredNumber.ToString();
                    MessageBox.Show("Некорректные данные в значении элемента для поиска были исправлены.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                try
                {
                    // Удаление завершающих символов-разделителей (;) справа от строки
                    string valuesAsString = string.Join(";", array_input.Text.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries));

                    // Преобразование строковых значений в массив double
                    double[] array = valuesAsString.Split(';').Select(double.Parse).ToArray();

                    // Нахождение индексов элемента двоичным поиском
                    string indexes = ArrayFunctions.StartBinarySearch(array, desiredNumber, out double[] sortedArray);


                    string sortedValuesAsString = string.Join(";", sortedArray.Select(num => num.ToString()));

                    if (array_input.Text != sortedValuesAsString)
                    {
                        array_input.Text = sortedValuesAsString;
                        MessageBox.Show("Некорректные данные были исправлены. Массив упорядочен по возрастанию.", "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    string result;
                    if (!string.IsNullOrEmpty(indexes))
                    {
                        result = "Элемент " + desiredNumber.ToString() + " имеет индекс: " + indexes;
                        label_result.Text = result;
                    }
                    else
                    {
                        result = "Элемент " + desiredNumber.ToString() + " в массиве отсутствует";
                        label_result.Text = result;
                    }

                    // Сбор общей строки для сохранения
                    string result_data = $"Исходный массив: {sortedValuesAsString}{Environment.NewLine}Результат: {result}";

                    // Диалог выбора файла для сохранения
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
                    saveFileDialog.FilterIndex = 1;
                    saveFileDialog.RestoreDirectory = true;

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            // Создание пути к файлу
                            string file_path = saveFileDialog.FileName;

                            // Запись данных в файл
                            File.WriteAllText(file_path, result_data);
                            MessageBox.Show($"Данные успешно сохранены в файл {file_path}", "Сохранение", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Произошла ошибка при сохранении данных: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (FormatException)
                {
                    // Вывод сообщения об ошибке при некорректном вводе
                    MessageBox.Show("Ошибка ввода. Пожалуйста, убедитесь, что вводимые данные корректны.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Произошла ошибка: {ex.Message}", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

            Btn_save_result.Enabled = true;
        }
    }
}
