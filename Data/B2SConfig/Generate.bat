rm "%cd%\Database.cs"
echo "File Removed"
dotnet  "%USERPROFILE%\.nuget\packages\petapoco.dbentitygenerator\0.1.1\tools\net5.0\PetaPoco.DBEntityGenerator.dll" --config "%cd%\config.json" -o File --outputFile "%cd%\Database.cs"
echo "Success"