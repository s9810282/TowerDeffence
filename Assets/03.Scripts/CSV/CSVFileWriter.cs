using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class CSVFileWriter : CSVFileCommon, IDisposable
{
    private StreamWriter Writer;
    private string OneQuote = null;
    private string TwoQuote = null;
    private string QuotedFormat = null;

    public CSVFileWriter(Stream stream)
    {
        Writer = new StreamWriter(stream);
    }

    public CSVFileWriter(string path)
    {
        Writer = new StreamWriter(path);
    }

    public void WriteRow(List<string> columns)
    {
        if (columns == null)
            return;

        if (OneQuote == null || OneQuote[0] != Quote)
        {
            OneQuote = string.Format("{0}", Quote);
            TwoQuote = string.Format("{0}{0}", Quote);
            QuotedFormat = string.Format("{0}{{0}}{0}", Quote);
        }

        for (int i = 0; i < columns.Count; i++)
        {
            if (i > 0)
                Writer.Write(Delimiter);

            if (columns[i].IndexOfAny(SpecialChars) == -1)
                Writer.Write(columns[i]);
            else
                Writer.Write(QuotedFormat, columns[i].Replace(OneQuote, TwoQuote));
        }

        Writer.WriteLine();

    }

    public void Dispose()
    {
        Writer.Dispose();
    }

}