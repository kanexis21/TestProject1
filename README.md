запускаем TestProject\TestProject.sln, или файл по пути TestProject\TestProject\bin\Debug\net6.0\TestProject.exe
Разработка и моделирование БД осуществяляась на реляционной модели MySQL, которую проделал с помощью Entity Framework Core и внутренних миграций.
По пути TestProject\TestProject\Domain\AppDbContext.cs, в данном классе следует изменить строку подключения к Базе данных.
База должна иметь таблицу Processes такого состава,
CREATE TABLE Processes (
    ProcessID INT IDENTITY(1,1) PRIMARY KEY,
    ProcessCode NVARCHAR(255) NOT NULL,
    ProcessName NVARCHAR(255) NOT NULL,
    CategoryName NVARCHAR(255) NOT NULL,
    OwnerDepartmentName NVARCHAR(255) NULL
);
