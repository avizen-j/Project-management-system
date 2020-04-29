## Project-management-system

#How to install Docker desktop?

**If you have windows pro/enterprise license:**
    1. Download and install from official docker hub: https://hub.docker.com/editions/community/docker-ce-desktop-windows/
	2. In the last step of installation leave "linux containers on windows"
	
**If you have windows home**
	1. Workaround: https://itnext.io/install-docker-on-windows-10-home-d8e621997c1d
	2. Download and install from official docker hub: https://hub.docker.com/editions/community/docker-ce-desktop-windows/
	3. In the last step of installation leave "linux containers on windows"
**DISCLAIMER:** InstallHyperV.bat and InstallContainers.bat are going to run for 10 minutes each. So be patient.

#How to run?

**Test changes locally (w/o Docker)**
	1. Select project_management_system (NOT IIS EXPRESS)
	2. And do things as always
	
**Test changes locally (with Docker)**
	1. Open terminal
	2. CD to the directory where is your Dockerfile
	3. Type (with the dot at the end as here): docker build -t project-management-system:v1 .
	4. Verify that image is created using: docker images
	5. If it is created then: docker run -it --rm -p 8080:80 project-management-system:v1
	6. Application is available on localhost:8080
	7. To stop running CTRL+C in terminal
**DISCLAIMER:** First time it takes +- 5min to pull all needed images while building. Next time it will build in a second.