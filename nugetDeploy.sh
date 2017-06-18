ApiKey=$1
Source=$2

nuget pack ./TreasureGen/TreasureGen.nuspec -Verbosity detailed
nuget pack ./TreasureGen.Domain/TreasureGen.Domain.nuspec -Verbosity detailed

nuget push ./DnDGen.TreasureGen.*.nupkg -Verbosity detailed -ApiKey $ApiKey -Source $Source
nuget push ./DnDGen.TreasureGen.Domain.*.nupkg -Verbosity detailed -ApiKey $ApiKey -Source $Source