using MarkdownToNexusMods;

namespace MarkdownToNexusModsTests
{
	public class HtmlTableRendererBBCodeTest : ConvertTestBase
	{
		[Fact]
		public void DynamicWidthTable_Success()
		{
			string input = @"
|Test|Test 2|Test 3|
|--|--|--|
|Some Value| Other value|  Other other value |
|Foo Value| Foo 2 Other value|  Foo 3 ........... Other other value |
";

            string expected =
"""
[font=Courier New]+------------+-------------------+-------------------------------------+
| Test       | Test 2            | Test 3                              |
|============+===================+=====================================|
| Some Value | Other value       | Other other value                   |
|------------+-------------------+-------------------------------------|
| Foo Value  | Foo 2 Other value | Foo 3 ........... Other other value |
+------------+-------------------+-------------------------------------+
[/font]

""";

            Run(input, expected);
		}


		[Fact]
		public void DynamicWidthTable_NoOuterPipes_Success()
		{
			string input = @"
|Test|Test 2|Test 3|
|--|--|--|
Some Value| Other value|  Other other value 
Foo Value| Foo 2 Other value|  Foo 3 ........... Other other value 
";

			string expected =
"""
[font=Courier New]+------------+-------------------+-------------------------------------+
| Test       | Test 2            | Test 3                              |
|============+===================+=====================================|
| Some Value | Other value       | Other other value                   |
|------------+-------------------+-------------------------------------|
| Foo Value  | Foo 2 Other value | Foo 3 ........... Other other value |
+------------+-------------------+-------------------------------------+
[/font]

""";


            Run(input, expected);
		}

        [Fact]
        public void Table_WidthLimited__Success()
        {
            string input = @"
|Test|Test 2|Test 3|
|--|--|--|
Some Value| Other value|  Other other value 
Foo Value| Foo 2 Other value|  1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 1234567890 
";

            string expected =
"""
[font=Courier New]+------------+-----------------+-----------------+
| Test       | Test 2          | Test 3          |
|============+=================+=================|
| Some Value | Other value     | Other other     |
|            |                 | value           |
|------------+-----------------+-----------------|
| Foo Value  | Foo 2 Other     | 1234567890      |
|            | value           | 1234567890      |
|            |                 | 1234567890      |
|            |                 | 1234567890      |
|            |                 | 1234567890      |
|            |                 | 1234567890      |
|            |                 | 1234567890      |
|            |                 | 1234567890      |
|            |                 | 1234567890      |
+------------+-----------------+-----------------+
[/font]

""";

            Run(input, expected, tableWidth: 50);
        }
    }
}