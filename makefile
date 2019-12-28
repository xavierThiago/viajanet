# Disclaimer: Linux/macOS enviroment only.

.PHONY: rebuild, cbt, pack, sonar, purge

rebuild:
	@echo Cleaning...
	@dotnet clean -v q > /dev/null
	@echo Building \(up to 5 threads\)...
	@dotnet build -p:maxcpucount=5 -v q > /dev/null
	@echo Done.

# Minimum line code coverage threshold of 90% (changeable from parameter).
threshold ?= 50
cbt: rebuild
	@printf "\nTesting...\n\n"
	@dotnet test -v q --no-build /p:CollectCoverage=true /p:Threshold=${threshold} /p:ThresholdType=method /p:CoverletOutputFormat=opencover
	@echo Done.

id ?= viaja-net-job-application
sonar:
	@echo Preparing files to Sonarqube...
	@dotnet sonarscanner begin /k:${id} /d:sonar.login=admin /d:sonar.password=admin /d:sonar.language="cs" /d:sonar.cs.opencover.reportsPaths="**/**opencover.xml" /d:sonar.coverage.exclusions="**Tests*.cs" /d:sonar.verbose=false
	@dotnet test /p:MaxCpuCount=5 /p:CollectCoverage=true /p:CoverletOutputFormat=opencover -v q
	@dotnet sonarscanner end /d:sonar.login=admin /d:sonar.password=admin
	@echo Done.

purge:
	@echo Purging bin and obj folders...
	@find ./src/ -name "bin" -o -name "obj" | xargs rm -drf
	@find ./src/tests -name "bin" -o -name "obj" | xargs rm -drf
	@echo Done.
