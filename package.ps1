$ErrorActionPreference = 'Stop'


md -Force -ErrorAction SilentlyContinue ./publish/targets/ > $null
md -Force -ErrorAction SilentlyContinue ./publish/packages/ > $null


# Requires a clean or the partial trimming will sometimes give trimming issues.
dotnet clean -c Release
dotnet build -c Release

function publishOs($target)
{
    Write-Output ("----- Building " + $target)
    # Not sure why, but the assembly trimming no longer works with the CommandLine.  The Specter.Console has a similar issue.
    #   Tried with the partial trim mode, but still failed.
    #   This will cause the .exe to go from 14 MB to about 75 MB.
    dotnet publish /p:DebugType=None /p:DebugSymbols=false --configuration Release --runtime $target --sc -o ./publish/targets/$target /p:PublishSingleFile=true .\MarkdownToNexusMods\MarkdownToNexusMods.csproj
    Compress-Archive -Force -DestinationPath ("./publish/packages/" + "MarkdownToNexusMods-" + $target + ".zip") ./publish/targets/$target/*
}

publishOs "osx-arm64"
publishOs "win-x64"
publishOs "linux-x64"
publishOs "linux-arm64"

"------------  Files"

dir -Recurse *.zip | %{write-host ($_.FullName + " ") -NoNewline }
Write-Host
