using AssignmentsSharing.Algorithms;
using AssignmentsSharing.Models;
using NUnit.Framework;

namespace AssignmentsSharingAlgorithmsTests
{
    public class AssignmentsBinaryPartitionAlgorithmsTests<T>
        where T : IPartitionAlgorithm<Assignment>, new()
    {
        IPartitionAlgorithm<Assignment> _algorithm;

        [SetUp]
        public void Setup()
        {
            _algorithm = new T();
            _algorithm.InterpretAsWeight = a => a.TimeCost;
        }

        [Test]
        public void TestSetSubsetsCount([Range(3, 11, 2)] int param)
        {
            Assert.That(() => { _algorithm.SubsetsCount = param; }, Throws.Exception);
        }

        public static IEnumerable<int[]> GetCollection()
        {
            yield return new[] { 1, 2, -3 };
            yield return new[] { -1, 2, 3 };
            yield return new[] { 1, 2, 3 };
        }

        //[TestCaseSource(nameof(GetCollection))]
        public void TestRunNegativeValue(params int[] parameters)
        {
        }
    }
}
