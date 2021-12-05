namespace Calctic.Interfaces
{
    public interface IBasicCalculator
    {
        string Inputs { get; set; }

        double Execute();
    }
}