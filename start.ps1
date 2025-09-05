# Run backend (.NET API)
Start-Process powershell -ArgumentList "cd ../ShopForHome-Starter/ShopForHome.Api; dotnet run"

# Run frontend (Angular)
Start-Process powershell -ArgumentList "cd shopforhome-ui-frontend; ng serve --open --proxy-config proxy.conf.json"
