#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat 

#FROM mcr.microsoft.com/dotnet/framework/runtime:4.7.2-windowsservercore-ltsc2019
#
#SHELL ["powershell"]
#
#ARG source
#WORKDIR /inetpub/wwwroot
#COPY ${source:-obj/Docker/publish} .
#
#EXPOSE 80

#FROM mcr.microsoft.com/windows/servercore/iis
#
#RUN powershell -NoProfile -Command Remove-Item -Recurse C:\inetpub\wwwroot\*
## Install Url Rewrite
#ADD https://download.microsoft.com/download/1/2/8/128E2E22-C1B9-44A4-BE2A-5859ED1D4592/rewrite_amd64_en-US.msi /install/rewrite_amd64.msi
#RUN msiexec.exe /i c:\install\rewrite_amd64.msi /passive
#
#ADD https://download.visualstudio.microsoft.com/download/pr/014120d7-d689-4305-befd-3cb711108212/0fd66638cde16859462a6243a4629a50/ndp48-x86-x64-allos-enu.exe /install/dotnet48.exe
#RUN msiexec.exe /i c:\install\dotnet48.exe /passive
#
#ARG source
#WORKDIR /inetpub/wwwroot
#COPY ${source:-obj/Docker/publish} .
#
#EXPOSE 80
#
#ENTRYPOINT ["C:\\ServiceMonitor.exe", "w3svc"]

ARG REPO=mcr.microsoft.com/dotnet/framework/runtime
FROM $REPO:4.8-windowsservercore-ltsc2016

SHELL ["powershell", "-Command", "$ErrorActionPreference = 'Stop'; $ProgressPreference = 'SilentlyContinue';"]

RUN Add-WindowsFeature Web-Server; \
     Add-WindowsFeature NET-Framework-45-ASPNET; \
     Add-WindowsFeature Web-Asp-Net45; \
     Remove-Item -Recurse C:\inetpub\wwwroot\*; \
     [Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12; \
     Invoke-WebRequest -Uri https://dotnetbinaries.blob.core.windows.net/servicemonitor/2.0.1.10/ServiceMonitor.exe -OutFile C:\ServiceMonitor.exe

# Install Roslyn compilers and ngen binaries
RUN [Net.ServicePointManager]::SecurityProtocol = [Net.SecurityProtocolType]::Tls12; \
    Invoke-WebRequest https://api.nuget.org/packages/microsoft.net.compilers.2.9.0.nupkg -OutFile c:\microsoft.net.compilers.2.9.0.zip; \
    Expand-Archive -Path c:\microsoft.net.compilers.2.9.0.zip -DestinationPath c:\RoslynCompilers; \
    Remove-Item c:\microsoft.net.compilers.2.9.0.zip -Force; \
    &C:\Windows\Microsoft.NET\Framework64\v4.0.30319\ngen.exe update; \
    &C:\Windows\Microsoft.NET\Framework\v4.0.30319\ngen.exe update; \
    &C:\Windows\Microsoft.NET\Framework64\v4.0.30319\ngen.exe install c:\RoslynCompilers\tools\csc.exe /ExeConfig:c:\RoslynCompilers\tools\csc.exe | \
    &C:\Windows\Microsoft.NET\Framework64\v4.0.30319\ngen.exe install c:\RoslynCompilers\tools\vbc.exe /ExeConfig:c:\RoslynCompilers\tools\vbc.exe | \
    &C:\Windows\Microsoft.NET\Framework64\v4.0.30319\ngen.exe install c:\RoslynCompilers\tools\VBCSCompiler.exe /ExeConfig:c:\RoslynCompilers\tools\VBCSCompiler.exe | \
    &C:\Windows\Microsoft.NET\Framework\v4.0.30319\ngen.exe install c:\RoslynCompilers\tools\csc.exe /ExeConfig:c:\RoslynCompilers\tools\csc.exe | \
    &C:\Windows\Microsoft.NET\Framework\v4.0.30319\ngen.exe install c:\RoslynCompilers\tools\vbc.exe /ExeConfig:c:\RoslynCompilers\tools\vbc.exe | \
    &C:\Windows\Microsoft.NET\Framework\v4.0.30319\ngen.exe install c:\RoslynCompilers\tools\VBCSCompiler.exe  /ExeConfig:c:\RoslynCompilers\tools\VBCSCompiler.exe

ENV ROSLYN_COMPILER_LOCATION c:\RoslynCompilers\tools

RUN powershell -NoProfile -Command Remove-Item -Recurse C:\inetpub\wwwroot\*
# Install Url Rewrite
ADD https://download.microsoft.com/download/1/2/8/128E2E22-C1B9-44A4-BE2A-5859ED1D4592/rewrite_amd64_en-US.msi /install/rewrite_amd64.msi
RUN msiexec.exe /i c:\install\rewrite_amd64.msi /passive

ARG source
WORKDIR /inetpub/wwwroot
COPY ${source:-obj/Docker/publish} .

EXPOSE 80

ENTRYPOINT ["C:\\ServiceMonitor.exe", "w3svc"]

#RUN dotnet restore
#RUN dotnet build /p:Version=$Version -c Release --no-restore  
#RUN dotnet pack /p:Version=$Version -c Release --no-restore --no-build -o /sln/artifacts 
#RUN dotnet build -c Release 

#ENTRYPOINT ["dotnet", "nuget", "push", "/sln/artifacts/*.nupkg"]
#CMD ["--source", "https://api.nuget.org/v3/index.json"]
#
# escape=\

#ARG REPO=mcr.microsoft.com/dotnet/framework/runtime
#FROM $REPO:4.7.2-windowsservercore-ltsc2019
#
### Next, this Dockerfile creates a directory for your application
##RUN mkdir C:\webapp
## configure the new site in IIS.
#RUN powershell -NoProfile -Command \
#Import-module IISAdministration; \
#New-IISSite -Name “ASPNET” -PhysicalPath C:\inetpub\wwwroot -BindingInformation “*:8000:”
## This instruction tells the container to listen on port 8000. 
#EXPOSE 8080
## The final instruction copies the site you published earlier into the container.
#Copy FingerprintPayment/obj/Docker/publish /inetpub/wwwroot

#docker build --tag jabjaisystem .
#docker run --name web_test1 -t -it -p 9902:80 2dff608b1e74


