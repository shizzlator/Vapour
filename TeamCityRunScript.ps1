# Get the team name
$doc = New-Object System.Xml.XmlDocument
$username = "%build.triggeredBy.username%"
$username  = ($username.Split("\") | Select-Object -Last 1)
write-host "username = $username"
$doc.Load("http://teamcity.tjgdev.ds:8500/guestAuth/app/rest/users/" + $username)

$team = ($doc.user.groups.group | where { $_.key.startsWith("TEAM")}) 
$teamName = $team.name
write-host Team Name: $teamName
write-host "##teamcity[setParameter name='env.TeamName' value='$teamName']"

# Run the tests
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