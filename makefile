# Disclaimer: Linux/macOS enviroment only.

.PHONY: init, rebuild, cbt, pack, sonar, purge

init:
	@printf 'Installing dependencies, testing and distributing assets...\n\n'
	@rm -dfr ./dist
	@npm install

dist:
	@rm -dfr ./dist
	@grunt test && grunt dist

rebuild:
	@echo Cleaning...
	@dotnet clean -v q > /dev/null
	@echo Building \(up to 5 threads\)...
	@dotnet build -p:maxcpucount=5 -v q > /dev/null
	@echo Done.

# Minimum line code coverage threshold of 0% (changeable from parameter).
threshold ?= 0
cbt: rebuild
	@printf "\nTesting...\n\n"
	@dotnet test -v q --no-build /p:CollectCoverage=true /p:Threshold=${threshold} /p:ThresholdType=method /p:CoverletOutputFormat=opencover
	@echo Done.

id ?= viajanet-job-application
sonar:
	@echo Preparing files to Sonarqube...
	@dotnet sonarscanner begin /k:${id} /d:sonar.login=admin /d:sonar.password=admin /d:sonar.language="cs" /d:sonar.cs.opencover.reportsPaths="**/**opencover.xml" /d:sonar.coverage.exclusions="**Tests*.cs" /d:sonar.verbose=false
	@dotnet test /p:MaxCpuCount=5 /p:CollectCoverage=true /p:CoverletOutputFormat=opencover -v q
	@dotnet sonarscanner end /d:sonar.login=admin /d:sonar.password=admin
	@echo Done.

pack: cbt
	@printf "\nDeleting previous .nupkg...\n"
	@rm -drf ./dist/nupkg
	@echo Done.
	@printf "\nPacking...\n\n"
	@dotnet pack -c Release --no-build -o ./dist/nupkg -v q
	@echo Done.

nuget: pack
	@echo Publishing to GitHub...
	@dotnet nuget push ./dist/nupkg/*.nupkg --source github
	@Done.

purge:
	@echo Purging dist, bin and obj folders...
	@rm -drf ./dist
	@find ./src/ -name "bin" -o -name "obj" | xargs rm -drf
	@find ./src/tests -name "bin" -o -name "obj" | xargs rm -drf
	@echo Done.
