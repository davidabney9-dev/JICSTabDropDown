
set wshShell = Wscript.createobject("wscript.shell")

wshShell.run "%COMSPEC% /k C:\WINDOWS\system32\ldifde.exe  -i -f CNTRTabDropDown.ldf -c ""CN=Schema,CN=Configuration,CN=X"" ""#schemaNamingContext"" -k -s localhost -v"