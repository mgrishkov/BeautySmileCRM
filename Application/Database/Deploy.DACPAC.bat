call "%programfiles(x86)%\Microsoft SQL Server\110\DAC\bin\SQLPackage.exe" /Action:Publish /SourceFile:"Database.dacpac" /Profile:"Database.publish.xml" > log.txt
