using Work2RPS;

namespace Tests {
    [TestClass]
    public class TestsWork2 {
        [TestMethod]
        public void TestSearchOneElem()
        {
            const int DESIRED_NUMBER = 3;
            const string DESIRED_INDEXES = "1; ";
            double[] array = { 2, 3, 6, 7, 10, 12, 15 };
            string indexes = ArrayFunctions.StartBinarySearch(array, DESIRED_NUMBER, out double[] sortedArray);
            Assert.AreEqual(DESIRED_INDEXES, indexes);
        }

        [TestMethod]
        public void TestSearchOneMoreElem()
        {
            const int DESIRED_NUMBER = 3;
            const string DESIRED_INDEXES = "1; 2; ";
            double[] array = { 2, 3, 3, 7, 10, 12, 15 };
            string indexes = ArrayFunctions.StartBinarySearch(array, DESIRED_NUMBER, out double[] sortedArray);
            Assert.AreEqual(DESIRED_INDEXES, indexes);
        }

        [TestMethod]
        public void TestSearchZeroElem()
        {
            const int DESIRED_NUMBER = 5;
            const string DESIRED_INDEXES = "";
            double[] array = { 2, 3, 6, 7, 10, 12, 15 };
            string indexes = ArrayFunctions.StartBinarySearch(array, DESIRED_NUMBER, out double[] sortedArray);
            Assert.AreEqual(DESIRED_INDEXES, indexes);
        }

        [TestMethod]
        public void TestSortArray()
        {
            double[] SORTED_ARRAY = { 4, 5, 6, 7, 8, 10, 15 };
            const int DESIRED_NUMBER = 3;
            double[] array = { 5, 4, 6, 7, 10, 8, 15 };
            string indexes = ArrayFunctions.StartBinarySearch(array, DESIRED_NUMBER, out double[] sortedArray);
            CollectionAssert.AreEqual(SORTED_ARRAY, sortedArray); // CollectionAssert.AreEqual, чтобы сравнить содержимое массивов
        }

        [TestMethod]
        public void TestSearchAllEqualElem()
        {
            const int DESIRED_NUMBER = 3;
            const string DESIRED_INDEXES = "0; 1; 2; 3; ";
            double[] array = { 3, 3, 3, 3 };
            string indexes = ArrayFunctions.StartBinarySearch(array, DESIRED_NUMBER, out double[] sortedArray);
            Assert.AreEqual(DESIRED_INDEXES, indexes);
        }
    }
}