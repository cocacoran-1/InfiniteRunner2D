@echo off
setlocal

:: 사용자 입력 받기
set /p REPO_URL="GitHub 리포지토리 주소 입력 (예: https://github.com/username/repo.git): "

:: 기존 .git 제거
echo 기존 Git 기록 제거 중...
rmdir /s /q .git

:: .gitignore 생성
echo .gitignore 생성 중...
(
echo [Ll]ibrary/
echo [Tt]emp/
echo [Oo]bj/
echo [Bb]uild/
echo [Bb]uilds/
echo [Ll]ogs/
echo UserSettings/
echo Packages/PackageCache/
echo *.csproj
echo *.sln
echo *.user
echo *.unityproj
echo *.pidb
echo *.suo
echo *.booproj
echo *.svd
echo *.pdb
echo *.mdb
echo *.opendb
echo *.VC.db
echo .vscode/
echo *.apk
echo *.unitypackage
) > .gitignore

:: Git 초기화 및 커밋
echo Git 초기화 중...
git init
git add .
git commit -m "초기 커밋: .gitignore 적용 및 불필요 파일 제외"

:: 원격 리포지토리 등록 및 푸시
git remote add origin %REPO_URL%
git branch -M main
git push -u origin main --force

echo -----------------------------------------
echo 완료되었습니다! GitHub에 푸시가 완료되었습니다.
pause
