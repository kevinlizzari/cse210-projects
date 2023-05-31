public class WritingAssignment : Assignment
{
    private string _writingInformation;

    public WritingAssignment(string studentName, string topic, string writingInformation)
        : base(studentName, topic)
    {
        _writingInformation = writingInformation;
    }

    public string GetWritingInformation()
    {
        return _writingInformation;
    }
}