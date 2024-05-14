param(
    [string]$name
)

if($name)
{
    Add-Migration -StartUpProject "SistemaTC.Api" -Project "SistemaTC.Data" $name
}
else
{
    Write-Output "Please enter a migration name"
}