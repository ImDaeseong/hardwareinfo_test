@echo off
REM 배치 파일: OpenHardwareMonitorWrapper.dll 등록
REM 관리자 권한 필요

REM DLL 파일 경로 설정
SET DLL_PATH=E:\hardwareinfo_test\OpenHardwareMonitorWrapper\bin\Release\OpenHardwareMonitorWrapper.dll
SET TLB_FILE=E:\hardwareinfo_test\OpenHardwareMonitorWrapper\bin\Release\OpenHardwareMonitorWrapper.tlb

REM regasm 경로 확인 (환경 변수 설정 필요 시)
SET REGASM_PATH=C:\Windows\Microsoft.NET\Framework\v4.0.30319\regasm.exe

REM 파일 존재 여부 확인
IF NOT EXIST "%DLL_PATH%" (
    echo Error: DLL file not found at %DLL_PATH%.
    pause
    exit /b 1
)

REM regasm 실행
echo Registering OpenHardwareMonitorWrapper.dll...
"%REGASM_PATH%" /codebase /tlb:"%TLB_FILE%" "%DLL_PATH%"

REM 작업 결과 확인
IF %ERRORLEVEL% EQU 0 (
    echo Registration completed successfully.
) ELSE (
    echo Registration failed with error code %ERRORLEVEL%.
)

pause
