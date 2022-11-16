using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;


namespace office_docs_demo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //The following code reads movie details in from a csv file and adds the data into a Word table
            List<Movie> movieList = null;
            using (var sr = new StreamReader("Top revenue earning SciFi movies per alphabetic letter.csv"))
            using (var reader = new CsvReader(sr, CultureInfo.InvariantCulture))
            {
                movieList = reader.GetRecords<Movie>().ToList();
                foreach (Movie m in movieList)
                {
                    Console.WriteLine($"{m.Movie_ID} is {m.Title}");
                }
            }

            // Read a template document, replace some text, and save as another document
            File.Copy("movie_data.docx", "clone.docx", true);

            int currentRow = 1;
            foreach (Movie movie in movieList)
            {
                ChangeTextInCell("clone.docx", currentRow, 0, movie.Movie_ID);
                ChangeTextInCell("clone.docx", currentRow, 1, movie.Title);
                ChangeTextInCell("clone.docx", currentRow, 2, movie.Release_Date);
                ChangeTextInCell("clone.docx", currentRow, 3, movie.Revenue.ToString("C"));
                ChangeTextInCell("clone.docx", currentRow, 4, movie.Tagline);
                currentRow++;
            }
        }

        public static void ChangeTextInCell(string filepath, int rowpos, int cellpos, string text)
        {
            using (WordprocessingDocument doc =
                WordprocessingDocument.Open(filepath, true))
            {
                // Find the first table in the document.
                Table table =
                doc.MainDocumentPart.Document.Body.Elements<Table>().First();
                int maxNumberOfColumns = 5;
                //Optional code that determines how many columns are needed in the table
                //foreach (TableRow tr in table.Elements<TableRow>())
                //{
                //    if (tr.Elements<TableCell>().Count() > maxNumberOfColumns)
                //    {
                //        maxNumberOfColumns = tr.Elements<TableCell>().Count();
                //    }
                //}

                // Find the row in the table (add rows and cells if they don't exist).
                if (table.Elements<TableRow>().Count() <= rowpos)
                {
                    for (int i = table.Elements<TableRow>().Count() - 1; i < rowpos; i++)
                    {
                        var tr = new TableRow();
                        table.Append(tr);

                    }
                }
                TableRow row = table.Elements<TableRow>().ElementAt(rowpos);
                for (int j = row.Elements<TableCell>().Count(); j < maxNumberOfColumns; j++)
                {
                    var tc = new TableCell();
                    row.Append(tc);
                }

                TableCell cell = row.Elements<TableCell>().ElementAt(cellpos);

                // Find the first paragraph in the table cell and add one if necessary.
                if (cell.Elements<Paragraph>().Count() == 0)
                {
                    var para = new Paragraph();
                    cell.Append(para);
                }

                Paragraph p = cell.Elements<Paragraph>().First();

                if (p.InnerText == String.Empty)
                {
                    string newText = text;
                    p.RemoveAllChildren();
                    p.AppendChild(new Run(new Text(newText)));
                }
                // Find the first run in the paragraph.
                Run r = p.Elements<Run>().First();

                // Set the text for the run.
                Text t = r.Elements<Text>().First();
                t.Text = text;
            }
        }
    }
}

