"Naloga 1 AC.exe" -E ByteRnd_10M.file izhod.AC
"Naloga 1 AC.exe" -D izhod.AC ByteRnd_10M_Result.file

"Naloga 1 AC.exe" -E slika.png izhod.AC
"Naloga 1 AC.exe" -D izhod.AC slika_result.png

"Naloga 1 AC.exe" -E song.MP3 izhod.AC
"Naloga 1 AC.exe" -D izhod.AC song_result.MP3

"Naloga 1 AC.exe" -E test.txt izhod.AC
"Naloga 1 AC.exe" -D izhod.AC test_result.txt

Test:
Tester.exe -c ByteRnd_10M.file ByteRnd_10M_Result.file

Tester.exe -c download.csv download_Result.csv