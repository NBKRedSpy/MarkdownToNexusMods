// Copyright (c) Alexandre Mutel. All rights reserved.
// This file is licensed under the BSD-Clause 2 license.
// See the license.txt file in the project root for more information.

using SC = Spectre.Console;
using System.Globalization;
using MDT = Markdig.Extensions.Tables;
using Markdig.Renderers;
using Markdig.Renderers.Html;
using Markdig.Renderers.Roundtrip.Inlines;
using Markdig.Syntax;
using Spectre.Console;
using Markdig.Extensions.Tables;
using System.Text;

namespace MarkdownToNexusMods.Renderers;

/// <summary>
/// A HTML renderer for a <see cref="Table"/>
/// </summary>
/// <seealso cref="HtmlObjectRenderer{TableBlock}" />
public class HtmlTableRendererBBCode : HtmlObjectRenderer<MDT.Table>
{

    /// <summary>
    /// The maxiumum width of the ASCII table in characters.
    /// </summary>
    public int MaxTableWidth { get; set; } = 100;
    protected override void Write(HtmlRenderer renderer, MDT.Table table)
    {
        if (renderer.EnableHtmlForBlock)
        {
            var scTable = new SC.Table();
            scTable.Border = SC.TableBorder.Ascii;
            scTable.ShowRowSeparators = true;
            scTable.AsciiDoubleHeadBorder();

            bool isHeaderRow = true;


            foreach (var rowObj in table)
            {
                var row = (MDT.TableRow)rowObj;
                List<string> rowText = new();


                for (int i = 0; i < row.Count; i++)
                {
                    var cellObj = row[i];
                    var cell = (TableCell)cellObj;
                    
                    var text = (cell.FirstOrDefault() as ParagraphBlock)?.Inline?.FirstChild?.ToString() ?? "";

                    rowText.Add(text);
                }

                if(isHeaderRow)
                {
                    isHeaderRow = false;
                    scTable.AddColumns(rowText.ToArray());
                }
                else
                {
                    scTable.AddRow(rowText.ToArray());  
                }
            }

            string asciiTableText = TableToString(MaxTableWidth, scTable);

            //Nexus Mod's editor requires the [font to be on the same line as the table start,
            //  but doesn't care about the extra \n before the closing tag.
            var tableText = $"[font=Courier New]{asciiTableText}[/font]";

            renderer.Write(tableText);
            renderer.WriteLine();

        }
        else
        {
            var implicitParagraph = renderer.ImplicitParagraph;

            renderer.ImplicitParagraph = true;
            foreach (var rowObj in table)
            {
                var row = (MDT.TableRow)rowObj;
                for (int i = 0; i < row.Count; i++)
                {
                    var cellObj = row[i];
                    var cell = (TableCell)cellObj;
                    renderer.Write(cell);
                    //write a space after each cell to avoid text being merged with the next cell
                    renderer.Write(' ');
                }
            }
            renderer.ImplicitParagraph = implicitParagraph;
        }
    }


    private string TableToString(int maxWidth, SC.Table table)
    {
        StringBuilder outBuilder = new StringBuilder();
        StringWriter stringWriter = new(outBuilder);
        AnsiConsoleOutput output = new(stringWriter);


        var console = AnsiConsole.Create(new AnsiConsoleSettings()
        {
            Out = output,
        });

        if (maxWidth != 0)
        {
            console.Profile.Width = maxWidth;
        }

        console.Write(table);
        outBuilder.Replace("\r\n", "\n");

        return outBuilder.ToString();
    }
}
