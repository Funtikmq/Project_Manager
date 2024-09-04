# Script pentru a obține autorul și numărul de pagini dintr-un document Word
param (
    [string]$filePath
)

# Creează o instanță a aplicației Word
$word = New-Object -ComObject Word.Application
$word.Visible = $false

# Deschide documentul Word
$doc = $word.Documents.Open($filePath)

# Obține autorul din proprietățile documentului
$author = $doc.BuiltInDocumentProperties("Author").Value

# Obține numărul de pagini
$pageCount = $doc.ComputeStatistics([Microsoft.Office.Interop.Word.WdStatistic]::wdStatisticPages)

# Închide documentul și aplicația Word
$doc.Close()
$word.Quit()

# Returnează rezultatele
Write-Output "Author: $author"
Write-Output "PageCount: $pageCount"