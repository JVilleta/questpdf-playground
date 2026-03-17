using PDFPlayground.Enums;

namespace PDFPlayground.Dtos
{
    public class PlayrollDto
    {
        required public string EmployeeName { get; set; }
        required public string EmployeeCode { get; set; }
        required public string EmployeeIdentification { get; set; }
        required public string EmployeePosition { get; set; }
        required public string EmployeeDepartment { get; set; }
        required public decimal EmployeeSalary { get; set; }
        required public DateTime EmployeePaymentStartDate { get; set; }
        required public DateTime EmployeePaymentEndDate { get; set; }
        required public DateTime PaymentDate { get; set; }
        required public PaymentMethodType PaymentMethod { get; set; }
        required public EmployeePayBasis PaymentFrequency { get; set; }
        required public ContractType ContractType { get; set; }
        public List<PlayrollDetailDto> PlayrollDetails { get; set; } = new();
        public List<PlayrollDeductionsDto> PlayrollDeductions { get; set; } = new();
        public List<PlayrollContributionsDto> PlayrollContributions { get; set; } = new();
    }
}
