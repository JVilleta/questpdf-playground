using System.ComponentModel;

namespace PDFPlayground.Enums
{
    public enum ContractType
    {
        [Description("Permanent")]
        PERMANENT = 199,
        [Description("Temporary")]
        TEMPORARY = 200,
        [Description("Internship")]
        INTERNSHIP = 201,
        [Description("Part time")]
        PART_TIME = 202
    }
}