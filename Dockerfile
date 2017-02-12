FROM mono:4.6-onbuild

RUN nuget install NUnit.ConsoleRunner -Version 3.6.0

CMD find . -type f -name "*Tests.dll" | xargs -I {} mono ./NUnit.ConsoleRunner.3.6.0/tools/nunit3-console.exe {} --noresult
