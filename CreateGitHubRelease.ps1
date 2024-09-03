param(
    [Parameter(
        Mandatory,
        Position = 0
        )]
    [string] $Version
)

$packages
gh release create $Version (get-item .\publish\packages\*.zip)
