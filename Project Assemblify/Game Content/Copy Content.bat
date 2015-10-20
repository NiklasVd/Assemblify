set root="..\..\..\.."
set gamePath="%root%\..\AssemblifyGame"
set serverPath="%root%\..\AssemblifyServer"

robocopy /e /mir %root%\Content %gamePath%\Content
robocopy /e /mir %root%\Content\bin\Windows "%gamePath%\bin\Windows\Debug\Content
PAUSE