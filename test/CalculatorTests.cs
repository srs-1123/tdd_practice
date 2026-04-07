using TddPractice;

namespace TddPractice.Tests;

public class CalculatorTests
{
    [Fact]
    public void Add_Returns3_WhenInputIs1And2()
    {
        var calculator = new Calculator();

        var result = calculator.Add(1, 2);

        Assert.Equal(3, result);
    }
}
