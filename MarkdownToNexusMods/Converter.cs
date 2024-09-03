using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Markdig.Renderers;
using Markdig.Syntax;
using Markdig;
using MarkdownToNexusMods.Renderers;
using Markdig.Renderers.Html;
using Markdig.Renderers.Html.Inlines;
using System.Text.RegularExpressions;

namespace MarkdownToNexusMods
{
	public class Converter
	{
		/// <summary>
		/// Converts the MD file to the NexusMods BBCode format.
		/// Important! The text must be pasted in the editor with the WYSIWYG mode, not the raw BBCode mode.
		/// Otherwise the tables will not be rendered correctly.
		/// </summary>
		/// <param name="markdownFile"></param>
		/// <param name="writer"></param>
		/// <param name="removeRelativeImages"></param>
		/// <param name="baseUri"></param>
		/// <param name="tableWidth"></param>
		public void Convert(TextReader markdownFile, TextWriter writer, bool removeRelativeImages, Uri? baseUri,
			int tableWidth)
		{

			var pipe = new MarkdownPipelineBuilder()
				.UseAdvancedExtensions()
				.UseSoftlineBreakAsHardlineBreak()
				.Build();


			var mdDoc = Markdig.Markdown.Parse(markdownFile.ReadToEnd(), pipe);


            //hack
            StringBuilder sb = new StringBuilder();
			TextWriter stringWriter = new StringWriter(sb);

			var renderer = new HtmlRenderer(stringWriter);
			renderer.EnableHtmlEscape = false;

			renderer.ObjectWriteBefore += Renderer_ObjectWriteBefore;
			bool removeResult;

			pipe.Setup(renderer);

			removeResult = renderer.ObjectRenderers.Replace<ParagraphRenderer>(new ParagraphRenderBbCode());
			removeResult = renderer.ObjectRenderers.Replace<Markdig.Extensions.Tables.HtmlTableRenderer>(new HtmlTableRendererBBCode() { MaxTableWidth = tableWidth });
			removeResult = renderer.ObjectRenderers.Replace<ListRenderer>(new ListRendererBbCode());
			removeResult = renderer.ObjectRenderers.Replace<HeadingRenderer>(new HeadingRendererBbCode());
			removeResult = renderer.ObjectRenderers.Replace<LinkInlineRenderer>(new LinkInlineRendererBbCode(removeRelativeImages, baseUri));
			removeResult = renderer.ObjectRenderers.Replace<LineBreakInlineRenderer>(new LineBreakInlineRendererBbCode());
			removeResult = renderer.ObjectRenderers.Replace<EmphasisInlineRenderer>(new EmphasisInlineRendererBbCode());
			removeResult = renderer.ObjectRenderers.Replace<QuoteBlockRenderer>(new QuoteBlockRendererBbCode());
			removeResult = renderer.ObjectRenderers.Replace<CodeInlineRenderer>(new CodeInlineRendererBBCode());
			removeResult = renderer.ObjectRenderers.Replace<CodeBlockRenderer>(new CodeBlockBBCode());

			renderer.Render(mdDoc);
            stringWriter.Flush();

			string output = sb.ToString();
			//The Nexus Mods editor will always trim multiple new lines when pasting in the WYSIYG mode.
			//A trick is to prefix every empty line with a space.
			output = Regex.Replace(output, "^\n", " \n", RegexOptions.Multiline);

			writer.Write(output);
		}

		//Used for debugging
		private static void Renderer_ObjectWriteBefore(IMarkdownRenderer arg1, MarkdownObject arg2)
		{
		}
	}
}
