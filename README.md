# DotNetConsoleApp

## Contents of this sample project

- Testing and debugging
  - xUnit
  - setup / teardown
  - Ominisharp runsettings
  - result report and coverage report
- Logging by NLog
- Database
  - SQL Server
  - Object mapping by Dapper
  - DAO(Data Acces Object) generator
- Config file(.json)
- Embedding files and load
- Documentation by Oxygen

## File description

| フォルダ/ファイル                  | 説明                         |
| ---------------------------------- | ---------------------------- |
| 📦DotNetConsoleApp                 | ルートフォルダ               |
| ┣ 📂.vscode                        | VS Code のワークスペース設定 |
| ┃ ┣ 📜launch.json                  | デバッグ起動設定             |
| ┃ ┣ 📜settings.json                | エディタや拡張機能の設定     |
| ┃ ┗ 📜tasks.json                   | タスクの定義                 |
| ┣ 📂document                       | 自動生成ドキュメント         |
| ┃ ┣ 📂html                         | ドキュメント生成結果         |
| ┃ ┗ 📜Doxyfile                     | ドキュメント生成設定         |
| ┣ 📂resource                       | 外部ファイル                 |
| ┃ ┗ 📂config                       | プログラム設定ファイル       |
| ┃ ┃ ┗ 📜AppConfig.json             | プログラム設定               |
| ┣ 📂src                            | ソース                       |
| ┃ ┣ 📂cs                           | .cs ファイル                 |
| ┃ ┃ ┣ 📂dao                        | DAO                          |
| ┃ ┃ ┃ ┗ 📜\*.cs                    | DAO ソース                   |
| ┃ ┃ ┣ 📂util                       | 共通部品                     |
| ┃ ┃ ┃ ┗ 📜\*.cs                    | 共通部品ソース               |
| ┃ ┃ ┗ 📜Program.cs                 | メイン処理ソース             |
| ┃ ┣ 📂sql                          | SQL                          |
| ┃ ┃ ┣ 📜\*.sql                     | SQL ソース                   |
| ┃ ┗ 📜DotNetConsoleApp.csproj      | .NET プロジェクト            |
| ┣ 📂test                           | テスト                       |
| ┃ ┣ 📂.config                      | ツールマニフェスト           |
| ┃ ┃ ┗ 📜dotnet-tools.json          | ツールマニフェスト\*1        |
| ┃ ┣ 📂cs                           | テストソース                 |
| ┃ ┃ ┗ 📜\*.cs                      | テストソース                 |
| ┃ ┣ 📜DotNetConsoleApp.Test.csproj | .NET プロジェクト(テスト）)  |
| ┃ ┣ 📜exportCoverage.ps1           | カバレッジ出力スクリプト\*2  |
| ┃ ┗ 📜unitTest.runsettings         | xUnit 設定                   |
| ┣ 📜.editorconfig                  | .cs コーディングスタイル設定 |
| ┣ 📜.gitignore                     | Git 無視設定                 |
| ┣ 📜LICENSE                        | ライセンス                   |
| ┗ 📜README.md                      | このファイル                 |

\*1:現状はカバレッジレポート出力ツールのためだけに使用。  
\*2:tasks.json で使用。

## Code inspection and formatting

コーディングスタイルの検査とフォーマットの設定を `.editorconfig` で行っている。
この設定を Omnisharp 拡張で読み込ませるため、 `%USERPROFILE%\.omnisharp`
に下記のファイルを配置する。またここで Omnisharp 用の設定も記述する。

omnisharp.json

```json
{
  "$desc": [
    "設定項目はこちらを参照",
    "https://github.com/OmniSharp/omnisharp-roslyn/wiki/Configuration-Options"
  ],
  "RoslynExtensionsOptions": {
    "enableAnalyzersSupport": true
  },
  "FormattingOptions": {
    "enableEditorConfigSupport": true,
    "OrganizeImports": true
  }
}
```
