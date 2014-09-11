Write-Output "LOOP"

$rootFolder = "C:\Projects\ClearMeasure\Internal\PowerShellRunnerService"
Write-Output $rootFolder
$nunit = $("$rootFolder\tools\nunit2.6.3\nunit-console.exe" )
$targetDll = $("$rootFolder\src\ExampleTests\bin\Debug\ExampleTests.dll")
$resultXml = $("$rootFolder\TestResult.xml")
 
$buildOptions = $("/xml:$resultXml ")

Write-Output $buildOptions
& $nunit $nunit $targetDll $buildOptions

$testResults = Get-Content $resultXml
Write-Output $testResults

$testResults.IndexOf('False')

if($testResults.IndexOf('False') -lt 0)
{
	Write-Error "Test execution has failed"
	exit 1
}

