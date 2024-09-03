[b][size=5]Markdown To Nexus Mods Utility[/size][/b] 
 
Converts a Markdown formatted file to Nexus Mods's proprietary BBCode format.
 
[b]Important[/b]: The resulting text must be pasted into the Nexus Mod's editor in WYSIWYG mode, not the raw BBCode mode.
 
[b][size=5]Need[/size][/b] 
 
Nexus Mods uses a proprietary version of BBCode and does not support tables.
This utility addresses those issues.
 
[b][size=5]Usage[/size][/b] 
[code]
Converts a Markdown file to Nexus Mod's BBCode dialect.
Important! The resulting text must be pasted into the Nexus Mod's editor in WYSIWYG mode, not the raw BBCode mode.
 
  -i, --input                     Required. The full path to the ReadMe.md file
                                  to parse.
  -o, --output                    The file to output the result to.  If not
                                  provided, will output to the console.
  -r, --remove-relative-images    Removes images that have a relative path
  -b, --base-url                  (Default: ) Any relative URI's will be
                                  converted to absolute URLs using this URL as
                                  the base.
  -w, --table-width               The maximum width of any ascii tables in
                                  characters.  Defaults to 100
  --help                          Display this help screen.
  --version                       Display version information.
 
 
[/code]
 
[b][size=5]NexusMods Formatting Differences[/size][/b] 
 
[b][size=4]Inline Code Blocks[/size][/b] 
 
Markdown inline code blocks are rendered in the "Courier New" font since NexusMods only supports multiline code blocks.
 
[b][size=4]Relative Image Links[/size][/b] 
 
Image links can be processes in a couple of ways:
 
[b][size=3]Remove[/size][/b] 
 
Using the -r option, relative images links will be completely removed.
 
This can be used to avoid relying on a resource external to NexusMods and avoid needing to remove the links manually.  The user would normally upload the images to the Mod's images area.
 
[b][size=3]Absolute Uri Base Path[/size][/b] 
 
Using the -b option, the user can provide a base URI.  That URI will be combined with any relative image URIs to create an absolute URI.
 
For example, directly linking github images:
 
Base URI:
[font=Courier New]https://raw.githubusercontent.com/SomeUser/SomeRepo/master/[/font]
 
Markdown image relative link:
[font=Courier New]![Counters Example](media/Example%20Diagram.png)[/font]
 
BBCode Result:
[font=Courier New][img]https://raw.githubusercontent.com/SomeUser/SomeRepo/master/media/Example%20Diagram.png[/img][/font]
 
[b][size=4]Plain Links[/size][/b] 
 
A link which is either plain text or use the same text for the link and the literal will not be rendered as a NexusMods [font=Courier New][url][/font] tag.
 
For example, [font=Courier New]https://example.com[/font] and [font=Courier New](https://example.com)[https://example.com][/font] will both be rendered as [font=Courier New]https://example.com[/font]
 
There is no visual or functional difference in the NexusMods translation.
 
[b][size=5]Additional Elements[/size][/b] 
 
If there is a Markdown element that is not translated, feel free to create an issue or contribute to this repo.
