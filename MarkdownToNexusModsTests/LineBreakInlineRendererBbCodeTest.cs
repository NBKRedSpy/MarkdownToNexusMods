using MarkdownToNexusMods;

namespace MarkdownToNexusModsTests
{
	public class LineBreakInlineRendererBbCodeTest : ConvertTestBase
	{
		[Fact]
		public void HardlineBreak()
		{
			string input = @"
test
test
";

			string expected = @" 
test
test
";

			Run(input, expected);
		}

	}
}