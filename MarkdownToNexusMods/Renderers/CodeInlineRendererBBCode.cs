using Markdig.Renderers;
using Markdig.Renderers.Html;
using Markdig.Syntax.Inlines;

namespace MarkdownToNexusMods.Renderers
{
    internal class CodeInlineRendererBBCode : HtmlObjectRenderer<CodeInline>
    {
        /// <summary>
        /// Uses fixed width font since BBCode doesn't have an inline code.
        /// </summary>
        /// <param name="renderer"></param>
        /// <param name="obj"></param>
        protected override void Write(HtmlRenderer renderer, CodeInline obj)
        {
            renderer.Write($"[font=Courier New]{obj.ContentSpan}[/font]");
        }
    }
}
