# tdd practice
C# を用いたテスト駆動開発の練習

## テストの実行

通常のテスト実行:

```bash
dotnet test test/TddPractice.Tests.csproj
```

変更監視しながらテスト実行:

```bash
dotnet watch test --project test/TddPractice.Tests.csproj
```

`test` プロジェクトだけでなく、`ProjectReference` されている `src` 側の変更も監視される。
