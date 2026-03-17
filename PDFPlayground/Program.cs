using Microsoft.Extensions.DependencyInjection;
using PDFPlayground.Dtos;
using PDFPlayground.Enums;
using PDFPlayground.Extensions;
using PDFPlayground.Templates;
using QuestPDF.Infrastructure;

QuestPDF.Settings.License = LicenseType.Community;

var services = new ServiceCollection();

services.AddBusinessEngineServices();
services.AddScoped<PlayrollTemplate>();

using var provider = services.BuildServiceProvider();

_ = provider.GetRequiredService<PlayrollTemplate>();

Console.WriteLine("DI configured.");

var playrollTemplate = provider.GetRequiredService<PlayrollTemplate>();
playrollTemplate.BuildDocument(new PlayrollDto()
{
    EmployeeName = "John Doe",
    EmployeeCode = "1234567890",
    EmployeeIdentification = "1234567890",
    EmployeePosition = "Software Engineer",
    EmployeeDepartment = "IT",
    EmployeeSalary = 100000,
    PaymentDate = DateTime.Now,
    PaymentMethod = PaymentMethodType.Cash,
    PaymentFrequency = EmployeePayBasis.Monthly,
    ContractType = ContractType.PERMANENT,
    EmployeePaymentStartDate = DateTime.Now,
    EmployeePaymentEndDate = DateTime.Now.AddDays(30),
    PlayrollDetails = new List<PlayrollDetailDto>()
    {
        new ()
        {
            Name = "Sueldo base",
            Note = "Mensual DOP$ 50.000,00",
            Amount = 25000,
        },
        new ()
        {
            Name = "Horas extras",
            Note = "6h x $1000,00",
            Amount = 6000,
        },
        new ()
        {
            Name = "Incentivo de puntualidad",
            Note = "-",
            Amount = 1000,
        },
        new ()
        {
            Name = "Bono de productividad",
            Note = "-",
            Amount = 1000,
        },
    },
    PlayrollDeductions = new List<PlayrollDeductionsDto>()
    {
        new PlayrollDeductionsDto()
        {
            Name = "AFP",
            Note = "Sobre salario",
            Amount = 845.50,
        },
        new ()
        {
            Name = "ISR",
            Note = "Sobre salario",
            Amount = 1000,
        },
        new ()
        {
            Name = "Seguro de salud",
            Note = "Sobre salario",
            Amount = 1000,
        },
    },
    PlayrollContributions = new List<PlayrollContributionsDto>()
    {
        new PlayrollContributionsDto()
        {
            Name = "AFP",
            Tasa = 0.08455,
            Amount = 845.50,
        },
        new PlayrollContributionsDto()
        {
            Name = "SFS",
            Tasa = 0.03,
            Amount = 300,
        },
        new PlayrollContributionsDto()
        {
            Name = "SRL",
            Tasa = 0.03,
            Amount = 300,
        },
        new PlayrollContributionsDto()
        {
            Name = "INFOTEP",
            Tasa = 0.03,
            Amount = 300,
        },
    },
});