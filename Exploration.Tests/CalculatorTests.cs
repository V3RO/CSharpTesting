using Xunit;
using FluentAssertions;

namespace Exploration.Tests;

public class CalculatorTests
{
    private readonly Calculator _calculator;

    public CalculatorTests()
    {
        _calculator = new Calculator();
    }

    [Fact]
    public void Calculate_WhenAdd_ReturnsCorrectResult()
    {
        // Arrange
        int x = 5;
        int y = 2;
        int expected = 7;

        // Act
        var actual = _calculator.Add(x, y);

        // Assert
        actual.Should().Be(expected);
    }

    [Fact]
    public void Calculate_WhenSubtract_ReturnsCorrectResult()
    {
        int x = 5;
        int y = 2;
        int expected = 3;
        
        var actual = _calculator.Subtract(x, y);
        
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Calculate_WhenDivideByZero_ThrowsDivideByZeroException()
    {
        Assert.Throws<DivideByZeroException>(() => _calculator.Divide(10, 0));
    }

    [Theory]
    [MemberData(nameof(MultiplyTestData))]
    public void Calculate_WhenMultiply_ReturnsCorrectResult(int x, int y, int expected)
    {
        var actual = _calculator.Multiply(x, y);
        
        actual.Should().Be(expected);
    }

    public static TheoryData<int, int, int> MultiplyTestData =>
        new()
        {
            { 1, 2, 2 },
            { 2, 5, 10 },
            { 3, 6, 18 },
        };
}