using MarkdownToNexusMods;

namespace MarkdownToNexusModsTests
{
	public class ListRendererBbCodeTest : ConvertTestBase
	{
		[Fact]
		public void BulletedList_Success()
		{
			string input = @"
* Test
* test2
";

			string expected = 
@"[list]
[*]Test
[*]test2
[/list]
";

			Run(input, expected);
		}

		[Fact]
		public void BulletedIndentedList_RendersSubList()
		{
			string input = @"
* Test
	* test2
";

			string expected =
@"[list]
[*]Test
[list]
[*]test2
[/list]
[/list]
";

			Run(input, expected);

		}
		[Fact]
		public void NumberedList_ForcedBullet()
		{
			string input = @"
1. Test
2. test2
";

			string expected =
@"[list=1]
[*]Test
[*]test2
[/list]
";

			Run(input, expected);
		}

		[Fact]
		public void NumberedList_AllOnes_ForcedBullet()
		{
			string input = @"
1. Test
1. test2
";

			string expected =
@"[list=1]
[*]Test
[*]test2
[/list]
";

			Run(input, expected);
		}

		[Fact]
		public void OrderedList_RendersSubList()
		{
			string input = @"
1. Test
	1. test2
";

			string expected =
@"[list=1]
[*]Test
[list=1]
[*]test2
[/list]
[/list]
";

			Run(input, expected);

		}

	}
}