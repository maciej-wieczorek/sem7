namespace AssignmentsSharing.Algorithms
{
    public interface IPartitionAlgorithm<T>
    {
        Func<T, int> InterpretAsWeight { get; set; }
        uint SubsetsCount { get; set; }
        List<HashSet<T>> Run(IEnumerable<T> set);
    }
}
