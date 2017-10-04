cls
echo off
echo Generiranje 1KB testne datoteke
Tester.exe -r 1024 ByteRnd_1K.file
echo Generiranje 10KB testne datoteke
Tester.exe -r 10240 ByteRnd_10K.file
echo Generiranje 1KB testne datoteke
Tester.exe -r 102400 ByteRnd_100K.file
echo Generiranje 1MB testne datoteke
Tester.exe -r 1048576 ByteRnd_1M.file
echo Generiranje 10MB testne datoteke
Tester.exe -r 10485760 ByteRnd_10M.file
echo Generiranje 100MB testne datoteke
Tester.exe -r 104857600 ByteRnd_100M.file
echo KONEC
echo on



