$resultGuid = (Get-ChildItem .\TestResults -Directory)[0].Name
echo "generate coverage report of $resultGuid"
dotnet tool run reportgenerator `
    "-reports:TestResults\$resultGuid\coverage.cobertura.xml" `
    "-targetdir:TestResults/CoverageReport" `
    -reporttypes:Html