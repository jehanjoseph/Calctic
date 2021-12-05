using System;
namespace Calctic.Model.Health.BMI
{
    public enum Gender
    {
        Male,
        Female
    }

    public class BmiResult
    {
        public Gender Gender { get; set; }
        public string Remark { get; set; }
        public double Result { get; set; }
    }
}
