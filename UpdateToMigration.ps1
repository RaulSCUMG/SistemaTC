param(
    [string]$name
)

if($name)
{
    Update-Database $name
}
else
{
    Write-Output "Please enter a migration name"
}