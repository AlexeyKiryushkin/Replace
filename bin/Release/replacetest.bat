echo off
replace.exe %1 %2 %3
If errorlevel 0 goto RepalaceOK 
if errorlevel 1 goto NoReplace
if errorlevel -1 goto BadArg
if errorlevel -2 goto ReplaceFailed
goto :EOF

:ReplaceOK
echo Test Repalace OK! 
goto :EOF

:NoReplace
echo Test No Replace
goto :EOF

:BadArg
echo Test Bad Cmdline
goto :EOF

:ReplaceFailed
echo Test Replace Failed 
goto :EOF
