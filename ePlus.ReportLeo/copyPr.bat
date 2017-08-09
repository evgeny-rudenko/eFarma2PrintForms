rem call copyPr.bat SolutionDir projectName

mkdir %1\Test

xcopy /y /r %1\%2\bin\Debug\%2.dll %1\Test

