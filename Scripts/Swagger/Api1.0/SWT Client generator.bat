echo off

SET SwaggerFile=%~dp0SwaggerFile.json

SET OutputFile=%~dp0..\..\..\SourceCode\OrphanageV3\Services\ApiClient.cs

%~dp0..\NSWAG\nswag.exe swagger2csclient /input:%SwaggerFile% /classname:ApiClient /ExceptionClass:ApiClientException /namespace:OrphanageV3.Services /output:%OutputFile% /operationgenerationmode:SingleClientFromOperationId /GenerateClientInterfaces:true /GenerateDtoTypes:false

cmd /k