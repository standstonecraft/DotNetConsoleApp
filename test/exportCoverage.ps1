$resultGuid = (Get-ChildItem .\TestResults -Directory)[0].Name
Write-Output "generate coverage report of $resultGuid"
dotnet tool restore
dotnet tool run reportgenerator `
    "-reports:TestResults\$resultGuid\coverage.cobertura.xml" `
    "-targetdir:TestResults/CoverageReport" `
    -reporttypes:Html