using MarkdownToNexusMods;

namespace MarkdownToNexusModsTests
{
	public class CodeInlineRendererBBCodeTest : ConvertTestBase
	{
		[Fact]
		public void InlineWrite_Success()
		{
			string input = @"
```test test```
";

			string expected = @" 
[font=Courier New]test test[/font]
";
			Run(input, expected);
		}


	}
}