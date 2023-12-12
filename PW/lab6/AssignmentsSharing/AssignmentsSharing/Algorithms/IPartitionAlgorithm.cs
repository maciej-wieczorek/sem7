namespace AssignmentsSharing.Algorithms
{
    public interface IPartitionAlgorithm<T>
    {
        Func<T, int> InterpretAsWeight { get; set; }
        int SubsetsCount { get; set; }
        List<HashSet<T>> Run(IEnumerable<T> set);
    }
}
