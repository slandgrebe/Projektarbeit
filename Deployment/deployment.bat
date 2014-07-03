:7-zip verzeichnis ersetzen
@echo off
set sevenZip=F:\Programme\Tools\7-Zip\7z.exe

:Eingabe
:@echo off 
:set /P version=d(Debug) oder r(Release):
:IF %version% == d GOTO Debug
:IF %version% == r GOTO Release
:IF 1==1 GOTO Fail

GOTO Debug



:Debug
set source=Debug
set target=Projektarbeit_Debug

GOTO START

:Release
set source=Release
set target="Dschungel Trainer"

GOTO START


:*******************************************
:START
:aufraeumen
del %0\..\%target%.exe /F /Q
del %0\..\%target% /F /Q
rmdir %0\..\%target%\

:dateien kopieren
mkdir %0\..\%target%

COPY %0\..\..\%source%\Visualization.dll %0\..\%target%\ /Y
:COPY %0\..\..\Debug\Visualization.dll %0\..\%target%\ /Y
COPY "%0\..\..\%source%\Dschungel Trainer.exe" %0\..\%target%\ /Y
:COPY %0\..\..\%source%\VisualizationExample.exe %0\..\%target%\ /Y
COPY %0\..\..\%source%\MotionDetection.dll %0\..\%target%\ /Y
COPY %0\..\..\%source%\View.dll %0\..\%target%\ /Y
COPY %0\..\..\%source%\Assimp32.dll %0\..\%target%\ /Y
COPY %0\..\..\%source%\log4net.dll %0\..\%target%\ /Y

mkdir %0\..\%target%\data
XCOPY %0\..\..\%source%\data %0\..\%target%\data /S /Y

:mkdir "%0\..\%target%\Resource Files"
:XCOPY %0\..\..\%source%\data "%0\..\%target%\Resource Files" /S /Y

:selbstentpackendes archiv erstellen
"%sevenZip%" a %target%.exe -mmt -mx5 -sfx7z.sfx "%0\..\%target%"

IF %source% == Debug GOTO Release

:Fail