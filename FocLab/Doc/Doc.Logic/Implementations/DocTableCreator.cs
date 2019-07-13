using Doc.Logic.Entities;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Wordprocessing;
using System.Collections.Generic;

namespace Doc.Logic.Implementations
{
    public static class DocTableCreator
    {
        public static Table GetTable(DocumentTable model)
        {
            // Use the file name and path passed in as an argument 
            // to open an existing Word 2007 document.

            // Create an empty table.
            Table table = new Table();

            // Create a TableProperties object and specify its border information.
            TableProperties tblProp = new TableProperties(
                
                new TableBorders(
                    new TopBorder()
                    {
                        Val = new EnumValue<BorderValues>(BorderValues.Dashed),
                        Size = 4
                    },
                    new BottomBorder
                    {
                        Val = new EnumValue<BorderValues>(BorderValues.Dashed),
                        Size = 4
                    },
                    new LeftBorder
                    {
                        Val = new EnumValue<BorderValues>(BorderValues.Dashed),
                        Size = 4
                    },
                    new RightBorder
                    {
                        Val = new EnumValue<BorderValues>(BorderValues.Dashed),
                        Size = 4
                    },
                    new InsideHorizontalBorder
                    {
                        Val = new EnumValue<BorderValues>(BorderValues.Dashed),
                        Size = 4
                    },
                    new InsideVerticalBorder
                    {
                        Val = new EnumValue<BorderValues>(BorderValues.Dashed),
                        Size = 4
                    }
                ),
                new TableWidth
                {
                    Type = new EnumValue<TableWidthUnitValues>(TableWidthUnitValues.Auto)
                }
            );

            // Append the TableProperties object to the empty table.
            table.AppendChild(tblProp);

            table.Append(AppendHeader(model));

            foreach (var tr in model.Data)
            {
                table.Append(CreateTableRowWithSomeStyles(tr, 24, false));
            }

            return table;
        }

        public static TableRow AppendHeader(DocumentTable model)
        {
            return CreateTableRowWithSomeStyles(model.Header, 24, true, JustificationValues.Center);
        }

        public static TableRow CreateTableRowWithSomeStyles(List<string> values, int fontSize, bool isBold, JustificationValues justification = JustificationValues.Left)
        {
            TableRow tr = new TableRow();

            foreach (var val in values)
            {
                // Create a cell.
                TableCell tc = new TableCell();
                
                var props = new TableCellProperties(new TableCellWidth()
                {
                    Type = TableWidthUnitValues.Auto
                });
                
                tc.Append(props);

                var run = GetRun(fontSize, isBold);

                run.Append(new Text(val));

                var para = new Paragraph(run);

                para.Append(new ParagraphProperties
                {
                    Justification = new Justification
                    {
                        Val = new EnumValue<JustificationValues>(justification)
                    }
                });

                // Specify the table cell content.
                tc.Append(para);

                // Append the table cell to the table row.
                tr.Append(tc);
            }

            return tr;
        }

        private static Run GetRun(int fontSize, bool isBold)
        {
            var runProps = new RunProperties
            {
                FontSize = new FontSize()
                {
                    Val = fontSize.ToString()
                }
            };

            if (isBold)
            {
                runProps.Bold = new Bold();
            }

            var run = new Run(runProps);

            return run;
        }
    }
}
