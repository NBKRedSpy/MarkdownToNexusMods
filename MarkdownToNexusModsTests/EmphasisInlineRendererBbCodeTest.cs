using MarkdownToNexusMods;

namespace MarkdownToNexusModsTests
{
	public class EmphasisInlineRendererBbCodeTest : ConvertTestBase
	{
		[Fact]
		public void Italics_Success()
		{
			string input = @"*test*";

			string expected = @" 
[i]test[/i]
";

			Run(input, expected);
		}

		[Fact]
		public void Bold_Success()
		{
			string input = @"**test**";

			string expected = @" 
[b]test[/b]
";

			Run(input, expected);
		}

		[Fact]
		public void Strikethrough_Success()
		{
			string input = @"~~test~~";

			//Todo:  Not sure why these add newlines.
			string expected = @" 
[s]test[/s]
";

			Run(input, expected);
		}


	}
}