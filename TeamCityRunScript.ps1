$testUri = "http://tjgtrlwsxt106.tjgdev.ds:8040/api/test/FakeProject/" + $env:TeamName + "/Smoke"
$result = Invoke-RestMethod $testUri
write-host $result.Message
write-host $result.TestResult
IF ($result.Success)
{
    Exit 0
}
ELSE
{
    Exit 1
}