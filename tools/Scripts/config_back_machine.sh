sudo bash

sudo apt update && sudo apt upgrade

apt install net-tools
apt install nano

#Если необходимо переустановить раздел диска
#-------------------------------------------
sudo fdisk /dev/vda
p
d
n #Параметры по умолчанию
w
sudo partprobe /dev/vda
sudo resize2fs /dev/vda1
#-------------------------------------------

wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
rm packages-microsoft-prod.deb
sudo apt-get update; \
  sudo apt-get install -y apt-transport-https && \
  sudo apt-get update && \
  sudo apt-get install -y dotnet-sdk-6.0
dotnet tool install --global dotnet-ef
export PATH="$PATH:$HOME/.dotnet/tools/"

sudo apt-add-repository ppa:git-core/ppa
sudo apt-get update
sudo apt-get install git

sudo apt -y install curl
sudo apt-get remove docker docker-engine docker.io containerd runc
sudo apt-get install \
    ca-certificates \
    curl \
    gnupg \
    lsb-release
sudo mkdir -p /etc/apt/keyrings
curl -fsSL https://download.docker.com/linux/ubuntu/gpg | sudo gpg --dearmor -o /etc/apt/keyrings/docker.gpg
echo \
  "deb [arch=$(dpkg --print-architecture) signed-by=/etc/apt/keyrings/docker.gpg] https://download.docker.com/linux/ubuntu \
  $(lsb_release -cs) stable" | sudo tee /etc/apt/sources.list.d/docker.list > /dev/null
sudo apt-get update
sudo apt-get install docker-ce docker-ce-cli containerd.io docker-compose-plugin
sudo apt-get update

sudo curl -L "https://github.com/docker/compose/releases/download/v2.4.1/docker-compose-$(uname -s)-$(uname -m)" -o /usr/local/bin/docker-compose
sudo chmod +x /usr/local/bin/docker-compose
sudo apt-get install docker-compose-plugin

#GIT
mkdir GreenCity
cd GreenCity
git clone https://gitlab.com/orlovartem123/green-city.git
cd green-city
git fetch --all
git checkout predprod

#AUTH-SERVICE
cd back/AuthService/AuthService
sed -i 's/builder.Logging.AddDatabase(builder.Services.BuildServiceProvider().GetService<IDbLogging>());/\/\/builder.Logging.AddDatabase(builder.Services.BuildServiceProvider().GetService<IDbLogging>());/' Program.cs
sed -i 's/localhost\\\\SQLEXPRESS/192.168.100.11/' appsettings.Development.json
sed -i "s/GreenCity_Dev_Passw0rd_1/${GREENCITY_PROD_DB_PASSWORD:?set GREENCITY_PROD_DB_PASSWORD}/" appsettings.Development.json
dotnet ef database update --context AuthLogDbContext
sed -i 's/\/\/builder.Logging.AddDatabase(builder.Services.BuildServiceProvider().GetService<IDbLogging>());/builder.Logging.AddDatabase(builder.Services.BuildServiceProvider().GetService<IDbLogging>());/' Program.cs
dotnet ef database update --context AuthDbContext -- --environment Prod
sed -i 's/192.168.100.15/localhost\\\\SQLEXPRESS/' appsettings.Development.json
sed -i "s/${GREENCITY_PROD_DB_PASSWORD}/GreenCity_Dev_Passw0rd_1/" appsettings.Development.json
cd ../../

#EXT-API-SERVICE
cd ExternalApiService/ExternalApiService
sed -i 's/builder.Logging.AddDatabase(builder.Services.BuildServiceProvider().GetService<IDbLogging>());/\/\/builder.Logging.AddDatabase(builder.Services.BuildServiceProvider().GetService<IDbLogging>());/' Program.cs
sed -i 's/localhost\\\\SQLEXPRESS/192.168.100.15/' appsettings.Development.json
sed -i "s/GreenCity_Dev_Passw0rd_1/${GREENCITY_PROD_DB_PASSWORD:?set GREENCITY_PROD_DB_PASSWORD}/" appsettings.Development.json
dotnet ef database update --context ExternalApiDbLogDbContext
sed -i 's/192.168.100.15/localhost\\\\SQLEXPRESS/' appsettings.Development.json
sed -i "s/${GREENCITY_PROD_DB_PASSWORD}/GreenCity_Dev_Passw0rd_1/" appsettings.Development.json
sed -i 's/\/\/builder.Logging.AddDatabase(builder.Services.BuildServiceProvider().GetService<IDbLogging>());/builder.Logging.AddDatabase(builder.Services.BuildServiceProvider().GetService<IDbLogging>());/' Program.cs
cd ../../

#SD-SERVICE
cd SDService/SDService.Main
sed -i 's/localhost\\\\SQLEXPRESS/192.168.100.15/' appsettings.Development.json
sed -i "s/GreenCity_Dev_Passw0rd_1/${GREENCITY_PROD_DB_PASSWORD:?set GREENCITY_PROD_DB_PASSWORD}/" appsettings.Development.json
sed -i 's/builder.Logging.AddDatabase(builder.Services.BuildServiceProvider().GetService<IDbLogging>());/\/\/builder.Logging.AddDatabase(builder.Services.BuildServiceProvider().GetService<IDbLogging>());/' Program.cs
cd ..
cd SDService.Database
dotnet ef database --startup-project ../SDService.Main/SDService.Main.csproj update --context SdLogDbContext
cd ..
cd SDService.Main
sed -i 's/\/\/builder.Logging.AddDatabase(builder.Services.BuildServiceProvider().GetService<IDbLogging>());/builder.Logging.AddDatabase(builder.Services.BuildServiceProvider().GetService<IDbLogging>());/' Program.cs
cd ..
cd SDService.Database
dotnet ef database --startup-project ../SDService.Main/SDService.Main.csproj update --context SdDbContext -- --environment Prod
cd ..
cd SDService.Main
sed -i 's/192.168.100.15/localhost\\\\SQLEXPRESS/' appsettings.Development.json
sed -i "s/${GREENCITY_PROD_DB_PASSWORD}/GreenCity_Dev_Passw0rd_1/" appsettings.Development.json
cd ../../

#TRANSPORT-SERVICE
cd TransportService/TransportService.Main
sed -i 's/localhost\\\\SQLEXPRESS/192.168.100.15/' appsettings.Development.json
sed -i "s/GreenCity_Dev_Passw0rd_1/${GREENCITY_PROD_DB_PASSWORD:?set GREENCITY_PROD_DB_PASSWORD}/" appsettings.Development.json
sed -i 's/builder.Logging.AddDatabase(builder.Services.BuildServiceProvider().GetService<IDbLogging>());/\/\/builder.Logging.AddDatabase(builder.Services.BuildServiceProvider().GetService<IDbLogging>());/' Program.cs
cd ..
cd TransportService.Database
dotnet ef database --startup-project ../TransportService.Main/TransportService.Main.csproj update --context TransportLogDbContext -- --environment Prod
cd ..
cd TransportService.Main
sed -i 's/\/\/builder.Logging.AddDatabase(builder.Services.BuildServiceProvider().GetService<IDbLogging>());/builder.Logging.AddDatabase(builder.Services.BuildServiceProvider().GetService<IDbLogging>());/' Program.cs
cd ..
cd TransportService.Database
dotnet ef database --startup-project ../TransportService.Main/TransportService.Main.csproj update --context TransportDbContext -- --environment Prod 
cd ..
cd TransportService.Main
sed -i 's/192.168.100.15/localhost\\\\SQLEXPRESS/' appsettings.Development.json
sed -i "s/${GREENCITY_PROD_DB_PASSWORD}/GreenCity_Dev_Passw0rd_1/" appsettings.Development.json
cd ../../

#SCHEDULE-SERVICE
cd ScheduleService/ScheduleService.Main
sed -i 's/localhost\\\\SQLEXPRESS/192.168.100.15/' appsettings.Development.json
sed -i "s/GreenCity_Dev_Passw0rd_1/${GREENCITY_PROD_DB_PASSWORD:?set GREENCITY_PROD_DB_PASSWORD}/" appsettings.Development.json
sed -i 's/builder.Logging.AddDatabase(builder.Services.BuildServiceProvider().GetService<IDbLogging>());/\/\/builder.Logging.AddDatabase(builder.Services.BuildServiceProvider().GetService<IDbLogging>());/' Program.cs
cd ..
cd ScheduleService.Database
dotnet ef database --startup-project ../ScheduleService.Main/ScheduleService.Main.csproj update --context ScheduleLogDbContext
cd ..
cd ScheduleService.Main
sed -i 's/\/\/builder.Logging.AddDatabase(builder.Services.BuildServiceProvider().GetService<IDbLogging>());/builder.Logging.AddDatabase(builder.Services.BuildServiceProvider().GetService<IDbLogging>());/' Program.cs
cd ..
cd ScheduleService.Database
dotnet ef database --startup-project ../ScheduleService.Main/ScheduleService.Main.csproj update --context ScheduleDbContext -- --environment Prod 
cd ..
cd ScheduleService.Main
sed -i 's/192.168.100.15/localhost\\\\SQLEXPRESS/' appsettings.Development.json
sed -i "s/${GREENCITY_PROD_DB_PASSWORD}/GreenCity_Dev_Passw0rd_1/" appsettings.Development.json
cd ../../

#Запусе проекта
docker-compose -f docker-compose.prod.yml up -d --build

#Для конфигурации nginx
#docker container exec -it green-city-green-city-front-1 /bin/bash

#Команды docker
docker builder prune #очистка кеша докера

#Rebuild docker container
docker-compose -f docker-compose.prod.yml up -d --no-deps --force-recreate --build <service_name>
 
