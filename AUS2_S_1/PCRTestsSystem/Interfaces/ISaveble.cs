namespace AUS2_S_1.PCRTestsSystem.Interfaces
{
    public interface ISaveble<T>
    {
        string CreateCSVLine(T data);
        T LoadFromCSVLine(string line);
    }
}
