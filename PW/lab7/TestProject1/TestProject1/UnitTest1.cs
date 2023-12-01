namespace TestProject1
{
    public class Tests
    {
        [Test(ExpectedResult = 10)]
        public int Test1()
        {
            return 10;
        }
    }

    public interface ISumUpAlgorithm
    {
        public int Run(IEnumerable<int> values);
    }
    public class SumUpImpl : ISumUpAlgorithm
    {
        public virtual int Run(IEnumerable<int> values)
        {
            return values.Sum(); 
        }
    }

    [TestFixture(typeof(SumUpImpl), new int[] { 1, 2, 6 }, 9)]
    public class Tests2<SumUpAlgorithm>
        where SumUpAlgorithm : ISumUpAlgorithm, new()
    {
        SumUpAlgorithm _algorithm;
        int[] _valuesToSumUp;
        int _expectedResult;

        public Tests2(int[] valuesToSumUp, int expectedResult)
        {
            _valuesToSumUp = valuesToSumUp;
            _expectedResult = expectedResult;
            _algorithm = new SumUpAlgorithm();
        }

        [Test]
        public void SumUpTest()
        {
            Assert.That(_algorithm.Run(_valuesToSumUp), Is.EqualTo(_expectedResult));
        }
    }

}