// Модуль, содержащий функцию бинарного поиска


namespace Work2RPS {
    public class ArrayFunctions {
        public static double[] BubbleSort(double[] array)
        {
            bool isSwap = false;
            double temp;

            for (int i = 0; i < array.Length; i++)
            {
                for (int j = 0; j < array.Length - i - 1; j++)
                {
                    if (array[j] > array[j + 1])
                    {
                        temp = array[j];
                        array[j] = array[j + 1];
                        array[j + 1] = temp;
                        isSwap = true;
                    }
                }
                if (!isSwap) break;
            }
            return array;
        }

        public static string StartBinarySearch(double[] array, double desiredNumber, out double[] sortedArray)
        {
            sortedArray = BubbleSort(array);
            List<int> indexesList = new List<int>();
            int leftBoard = 0;
            int rightBoard = sortedArray.Length - 1;

            while (leftBoard <= rightBoard)
            {
                int mid = (leftBoard + rightBoard) / 2;

                if (sortedArray[mid] == desiredNumber)
                {
                    indexesList.Add(mid); // Добавляем индекс, если найдено искомое число
                                          // Продолжаем поиск слева и справа от найденного индекса
                    int left = mid - 1;
                    int right = mid + 1;
                    // Пока не достигнут конец массива и не найдены все элементы desiredNumber
                    while (left >= 0 && sortedArray[left] == desiredNumber)
                    {
                        indexesList.Add(left--);
                    }
                    while (right < sortedArray.Length && sortedArray[right] == desiredNumber)
                    {
                        indexesList.Add(right++);
                    }
                    break; // Выходим из цикла бинарного поиска, так как все вхождения найдены
                }
                else if (sortedArray[mid] > desiredNumber) rightBoard = mid - 1;
                else leftBoard = mid + 1;
            }

            indexesList.Sort();
            // Формируем строку с индексами
            string indexes = "";
            foreach (int index in indexesList)
            {
                indexes += index.ToString() + "; ";
            }

            return indexes;
        }

    }
}
