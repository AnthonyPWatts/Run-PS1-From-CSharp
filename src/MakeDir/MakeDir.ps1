param ($dirName)
if ($dirName -eq $null) {
    throw "No dirName was provided"
}

$path = 'C:/' + $dirName + (Get-Date -Format "HHmmss")
mkdir $path