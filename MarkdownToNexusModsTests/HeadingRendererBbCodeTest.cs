using MarkdownToNexusMods;

namespace MarkdownToNexusModsTests
{
	public class HeadingRendererBbCodeTest : ConvertTestBase
	{
		[Fact]
		public void HeadingTest_1_6_Success()
		{

			List<string> input;
			List<string> expected = new();

			//headers, "# test" to "###### test"
			input = Enumerable.Range(1, 6)
				.Select(x => $"{new String('#', x)} test")
				.ToList();

			string format = "[b][size={0}]test[/size][/b] \n";

			expected.Add(String.Format(format, 5));
            expected.Add(String.Format(format, 4));
            expected.Add(String.Format(format, 3));
            expected.Add(String.Format(format, 2));
            expected.Add(String.Format(format, 2));
            expected.Add(String.Format(format, 2));


			var tests = input.Zip(expected).Select(x => (input: x.First, expected: x.Second));

			foreach (var test in tests)
			{
                Run(test.input, test.expected);
            }
		}


	}
}