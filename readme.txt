"Naloga 1 AC.exe" -E ByteRnd_10M.file izhod.AC
"Naloga 1 AC.exe" -D izhod.AC ByteRnd_10M_Result.file

"Naloga 1 AC.exe" -E slika.png izhod.AC
"Naloga 1 AC.exe" -D izhod.AC slika_result.png

"Naloga 1 AC.exe" -E download.csv izhod.AC
"Naloga 1 AC.exe" -D izhod.AC download_Result.csv

Test:
Tester.exe -c ByteRnd_10M.file ByteRnd_10M_Result.file

Tester.exe -c download.csv download_Result.csv