using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace TddPractice;

public class CsvTableReader
{
    private const int PreambleLineCount = 21;

    public CsvTable Read(string csvText)
    {
        var bodyText = ExtractBodyText(csvText);
        using var textReader = new StringReader(bodyText);
        using var csv = CreateCsvReader(textReader);

        var records = ReadRemainingRecords(csv).ToList();

        return new CsvTable
        {
            Columns = ReadColumns(records),
            Rows = ReadRows(records)
        };
    }

    private static CsvReader CreateCsvReader(StringReader textReader)
    {
        return new CsvReader(textReader, new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = false
        });
    }

    private static string ExtractBodyText(string csvText)
    {
        var lines = csvText
            .Split(Environment.NewLine)
            .Skip(PreambleLineCount)
            .ToArray();

        if (lines.Length == 0)
        {
            throw new InvalidOperationException("CSV must contain a column header line.");
        }

        return string.Join(Environment.NewLine, lines);
    }

    private static IReadOnlyList<string[]> ReadRemainingRecords(CsvReader csv)
    {
        var records = new List<string[]>();

        while (csv.Read())
        {
            if (csv.Parser.Record is { } record)
            {
                records.Add(record.ToArray());
            }
        }

        return records;
    }

    private static IReadOnlyList<string> ReadColumns(IReadOnlyList<string[]> records)
    {
        return records.FirstOrDefault()
            ?? throw new InvalidOperationException("CSV must contain a column header line.");
    }

    private static IReadOnlyList<IReadOnlyList<string>> ReadRows(IReadOnlyList<string[]> records)
    {
        return records
            .Skip(1)
            .Select(record => (IReadOnlyList<string>)record)
            .ToList();
    }
}
