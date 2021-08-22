# TFL Route Status Checker

Find out the route status by calling TFL open data REST API at https://api.tfl.gov.uk 

## Configuration

Open TflRouteStatus\TflRouteStatus\appSettings.json file and set App_Id and App_Key

## Build

Open Powershell and go to code folder (eg: cd C:\TflRouteStatus) and execute below command

```bash
dotnet build
```

## Run the output

Open Powershell and execute below command

```bash
C:\TflRouteStatus\TflRouteStatus\bin\Debug\net5.0\TflRouteStatus.exe A2
echo $lastexitcode
```

## Run the test cases

Open Powershell and go to code folder (eg: cd C:\TflRouteStatus) and execute below command

```bash
dotnet test
```

## Assumptions
Here, I am assuming you are placing code in C: drive to run the above commands
