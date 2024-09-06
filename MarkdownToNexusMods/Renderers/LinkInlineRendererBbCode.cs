// Copyright (c) Alexandre Mutel. All rights reserved.
// This file is licensed under the BSD-Clause 2 license. 
// See the license.txt file in the project root for more information.

using Markdig.Renderers;
using Markdig.Renderers.Html;
using Markdig.Syntax;
using Markdig.Syntax.Inlines;

namespace MarkdownToNexusMods.Renderers;

/// <summary>
/// A HTML renderer for a <see cref="LinkInline"/>.
/// </summary>
/// <seealso cref="HtmlObjectRenderer{LinkInline}" />
public class LinkInlineRendererBbCode : HtmlObjectRenderer<LinkInline>
{

    public bool RemoveRelativeImageLinks { get; set; }
	public Uri? BaseUri { get; }

	public LinkInlineRendererBbCode(bool renderRelativeImageLinks, Uri? baseUri)
    {
        RemoveRelativeImageLinks = renderRelativeImageLinks;
		BaseUri = baseUri;
	}

    protected override void Write(HtmlRenderer renderer, LinkInline link)
    {
        ArgumentNullException.ThrowIfNull(link.Url);

		string uriString = link.Url;

        if(RemoveRelativeImageLinks && link.IsImage && 
            Uri.TryCreate(uriString, UriKind.Relative, out _))
        {
			ParagraphRenderBbCode.SkipNewLine = true;
			return;
        }


        bool isDocumentLink = uriString.Trim().StartsWith("#");

        if(isDocumentLink == false && BaseUri != null)
        {
            if(Uri.TryCreate(BaseUri, link.Url, out Uri? absoluteUri))
            {
                uriString = absoluteUri.ToString();
			}
            else
            {
                throw new ApplicationException($"Unable to convert link to absolute link: '{link.Url}");
            }
        }
        
        string? linkLiteralText = GetLinkText(link);

        if (!link.IsImage)
        {
            //Plain text link conversion
            if (link.Url == linkLiteralText)
            {
                //Use the link as the text and the link as NexusMods formats it differently.
                renderer.Write($"[url={uriString}]{uriString}[/url]");
                return;
            }

            //Render document referencing link as simple text
            if (isDocumentLink)
            {
                renderer.Write($"[font=Courier New]{linkLiteralText}[/font]");
                return;
            }
        }


		renderer.Write(link.IsImage ? "[img]" : "[url=");

        renderer.WriteEscapeUrl(link.GetDynamicUrl != null ? link.GetDynamicUrl() ?? uriString : uriString);

        if (link.IsImage)
        {
            renderer.Write("[/img]");
        }
        else
        {
			renderer.Write(']');

			LiteralInline? linkText = link.Descendants<LiteralInline>().FirstOrDefault();
            if (linkText is not null)
            {
                renderer.Write(linkText.Content);
            }
            renderer.Write("[/url]");
        }
    }

    private string? GetLinkText(LinkInline link)
    {
        var content = link.Descendants<LiteralInline>().FirstOrDefault()?.Content;

        if (content is null)
        {
            return null;
        }

        return content.Value.Text.Substring(content.Value.Start, content.Value.Length);
    }
}