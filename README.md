# Markdown To Nexus Mods Utility

Converts a Markdown formatted file to Nexus Mods's proprietary BBCode format.  

**Important**: The resulting text must be pasted into the Nexus Mod's editor in WYSIWYG mode, not the raw BBCode mode.

# Need
Nexus Mods uses a proprietary version of BBCode and does not support tables.
This utility addresses those issues.

# Usage

```
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


  ```


# NexusMods Formatting Differences

## Inline Code Blocks

Markdown inline code blocks are rendered in the "Courier New" font since NexusMods only supports multiline code blocks.

## Relative Image Links
Image links can be processes in a couple of ways:

### Remove
Using the -r option, relative images links will be completely removed.  

This can be used to avoid relying on a resource external to NexusMods and avoid needing to remove the links manually.  The user would normally upload the images to the Mod's images area.

### Absolute Uri Base Path
Using the -b option, the user can provide a base URI.  That URI will be combined with any relative image URIs to create an absolute URI.

For example, directly linking github images:

Base URI:
```https://raw.githubusercontent.com/SomeUser/SomeRepo/master/```

Markdown image relative link:
```![Counters Example](media/Example%20Diagram.png)```

BBCode Result:
```[img]https://raw.githubusercontent.com/SomeUser/SomeRepo/master/media/Example%20Diagram.png[/img]```



## Plain Links
A link which is either plain text or use the same text for the link and the literal will not be rendered as a NexusMods `[url]` tag.

For example, ```https://example.com``` and ```(https://example.com)[https://example.com]``` will both be rendered as ```https://example.com```

There is no visual or functional difference in the NexusMods translation.

# Additional Elements
If there is a Markdown element that is not translated, feel free to create an issue or contribute to this repo.
