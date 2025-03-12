using Lab_2;

namespace Lab_2_tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestComplexExpression1()
        {
            string expression = "+ - + / * 2 20 2 * + 3 4 ^ 3 2 6 15";
            INode root = Parser.ParsePrefix(expression);
            double result = root.Evaluate(new Dictionary<string, double>());
            Assert.AreEqual(92, result);
        }
        [TestMethod]
        public void TestComplexExpression2()
        {
            string expression = "+ 78 / - 30 * 0.5 + 28 8 6";
            INode root = Parser.ParsePrefix(expression);
            double result = root.Evaluate(new Dictionary<string, double>());
            Assert.AreEqual(80, result);
        }
        public void TestComplexExpression3()
        {
            string expression = "- + sqrt * 4 9 log exp 2";
            INode root = Parser.ParsePrefix(expression);
            double result = root.Evaluate(new Dictionary<string, double>());
            Assert.AreEqual(-8, result);
        }
        [TestMethod]
        public void TestMultipleMinuses()
        {
            string expression = "- - - - - 5";
            INode root = Parser.ParsePrefix(expression);
            double result = root.Evaluate(new Dictionary<string, double>());
            Assert.AreEqual(-5, result);
        }
    }
}