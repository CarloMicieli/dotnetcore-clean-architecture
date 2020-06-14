using FluentAssertions;
using Xunit;

namespace TreniniDotNet.Common.Lengths
{
    class TestMultipleValues : MultipleValues<Length>
    {
        public TestMultipleValues(MeasureUnit measureUnit1, MeasureUnit measureUnit2)
            : base(measureUnit1, measureUnit2, (v, mu) => Length.Of(v, mu))
        {
        }
    }

    public class MultipleValuesTests
    {
        private readonly MultipleValues<Length> Subject;

        public MultipleValuesTests()
        {
            Subject = new TestMultipleValues(
                MeasureUnit.Inches,
                MeasureUnit.Millimeters);
        }

        [Fact]
        public void MultipleLengths_Create_ShouldCreateTheValueConvertingTheMissing()
        {
            var (len1, len2) = Subject.Create(42.0M, MeasureUnit.Inches);

            len1.Should().Be(Length.OfInches(42.0M));
            len2.Should().Be(Length.OfMillimeters(1066.80M));
        }

        [Fact]
        public void MultipleLengths_Create_ShouldCreateTwoValues()
        {
            var (len1, len2) = Subject.Create(42.0M, 24.0M);

            len1.Should().Be(Length.OfInches(42.0M));
            len2.Should().Be(Length.OfMillimeters(24.0M));
        }

        [Fact]
        public void MultipleLengths_Create_ShouldCreateTwoValuesConvertingTheMissingOne()
        {
            var (in1, mm1) = Subject.Create(0.65M, null);
            var (in2, mm2) = Subject.Create(null, 16.5M);

            in1.Should().Be(Length.OfInches(0.65M));
            mm1.Should().Be(Length.OfMillimeters(16.51M));
            in2.Should().Be(Length.OfInches(0.65M));
            mm2.Should().Be(Length.OfMillimeters(16.5M));
        }

        [Fact]
        public void MultipleLengths_TryCreate_ShouldReturnTrueAndCreateNewValue()
        {
            var result = Subject.TryCreate(11M, 12M, out var ml);

            result.Should().BeTrue();
            ml.Should().NotBeNull();
            ml.Value.Item1.Should().Be(Length.OfInches(11M));
            ml.Value.Item2.Should().Be(Length.OfMillimeters(12M));

        }

        [Fact]
        public void MultipleLengths_TryCreate_ShouldReturnFalse_WhenBothInputAreNull()
        {
            var result = Subject.TryCreate(null, null, out var ml);

            result.Should().BeFalse();
            ml.Should().BeNull();
        }

        [Fact]
        public void MultipleLengths_TryCreate_ShouldReturnFalse_WhenBothInputAreNegative()
        {
            var result = Subject.TryCreate(-10M, -10M, out var ml);

            result.Should().BeFalse();
            ml.Should().BeNull();
        }
    }
}