# SPP-Pay-App

![](https://img.shields.io/badge/Type-Desktop%20App%2FAplikasi%20Desktop-purple)

### Hello👋

Language : Indonesia

---

Description :\
Contains a desktop application to pay school fees.

Operating system I use: Windows 11\
Required Software:
- Visual Studio 2019
- SQL Server 18

Instructions:
- Clone this repo or download this repo.
- [IMPORT DATABASE] Look at the database SQL Server from **stuff** folder in this repo **(stuff/db_spp.bacpac)**
- [IMPORT DATABASE] In SQL Server (Right Click)Databases -> Import Data-tier Application -> and then Import from local disk -> select `db_spp.bacpac` file\
> __Warning__ \
During import database, name the database as `db_spp` **IMPORTANT**
- [OPEN PROJECT] Open the `.sln` file **(AplikasiPembayaranSpp.2.0.0.sln)**
- [OPEN PROJECT] In Visual Studio go to **Server Explorer**
- [CONNECT TO DATABASE] Data Connection -> (Right Click)Add Connection -> Connect to your SQL Server database and select the **Server Name** and connect to database **db_spp**
- Then enjoy the application.

---

> __Note__ \
`Login Page as Administrator`\
ID : 878\
Username : Aakhif\
Password : admin\
`Login Page as Petugas`\
ID : 676\
Username : Aakhif\
Password : petugas\
`Login Page as Student`\
NISN : 1234567890\
Nama : Aakhif\
ID SPP : 342\
