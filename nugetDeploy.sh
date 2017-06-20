ApiKey=$1
Source=$2

nuget pack ./OkonkwoOandaV20/OkonkwoOandaV20/OkonkwoOandaV20.csproj -Verbosity detailed -OutputDirectory ./OkonkwoOandaV20/pkg

nuget push ./OkonkwoOandaV20/pkg/*.nupkg -Verbosity detailed -ApiKey $ApiKey -Source $Source