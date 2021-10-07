#dir "*.csproj" -Recurse | %{dotnet build  $PSItem.FullName --output artifacts/$PSItem.Basename}

$projects = Get-ChildItem "src/*.csproj" -Recurse

foreach ($project in $projects)
{
	dotnet build $project.FullName
}
