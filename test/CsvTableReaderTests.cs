using System.Text;
using TddPractice;

namespace TddPractice.Tests;

public class CsvTableReaderTests
{
    /// <summary>
    /// 先頭21行の仮ヘッダを読み飛ばし、22行目をカラム行として扱えることを確認する。
    /// </summary>
    [Fact]
    public void Read_IgnoresFirst21Lines_AndUsesLine22AsColumns()
    {
        var reader = new CsvTableReader();
        var csvText = BuildCsvText();

        var result = reader.Read(csvText);

        Assert.Equal(["Id", "Name", "City"], result.Columns);
        Assert.Equal(2, result.Rows.Count);
        Assert.Equal(["1", "Alice", "Tokyo"], result.Rows[0]);
        Assert.Equal(["2", "Bob", "Osaka"], result.Rows[1]);
    }

    /// <summary>
    /// 先頭21行がメタ情報、22行目がカラム名、23行目以降がデータ行のCSV文字列を組み立てる。
    /// </summary>
    private static string BuildCsvText()
    {
        var builder = new StringBuilder();

        for (var i = 1; i <= 21; i++)
        {
            builder.AppendLine($"header-{i}");
        }

        builder.AppendLine("Id,Name,City");
        builder.AppendLine("1,Alice,Tokyo");
        builder.AppendLine("2,Bob,Osaka");

        return builder.ToString();
    }
}
