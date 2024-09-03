// Copyright (c) Alexandre Mutel. All rights reserved.
// This file is licensed under the BSD-Clause 2 license. 
// See the license.txt file in the project root for more information.

using Markdig.Renderers.Html;
using Markdig.Syntax;
using System.Security.Cryptography.X509Certificates;

namespace MarkdownToNexusMods;

/// <summary>
/// An HTML renderer for a <see cref="HeadingBlock"/>.
/// </summary>
/// <seealso cref="HtmlObjectRenderer{HeadingBlock}" />
public class HeadingRendererBbCode : HtmlObjectRenderer<HeadingBlock>
{
	protected override void Write(Markdig.Renderers.HtmlRenderer renderer, HeadingBlock obj)
	{
		//Headings 
		//	The "normal size" text is 2.  Using Big (5) for the top level as Extra Big is very large.
		int headingSize = Math.Clamp(6 - obj.Level, 2, 5);

		if(renderer.IsFirstInContainer == false)
		{
			renderer.Write(" ");  //trick to force Nexus Mod Editor to not trim the \n
			renderer.WriteLine();
		}
		else
		{
			renderer.EnsureLine();
		}

		renderer.Write($"[b][size={headingSize}]");
		renderer.WriteLeafInline(obj);
		renderer.Write("[/size][/b]");

        //Compensates for markdown which has a line feed after the header,
        //which is usually ignored by MD
        renderer.Write(" ");  //trick to force Nexus Mod Editor to not trim the \n
		renderer.WriteLine();


    }
}