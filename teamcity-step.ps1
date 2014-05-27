$artifactUrl = "repository/download/%system.teamcity.buildConfName%/%teamcity.build.id%:id/VAPOUR_PROJ_NAME.%system.build.number%.nupkg"
$testUri = "http://VAPOUR_SERVER_NAME:8040/Test/%teamcity.project.id%/%build.defaultTeam%/Smoke?artifactUrl=" + $artifactUrl
write-host $testUri
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