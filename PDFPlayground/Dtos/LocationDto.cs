namespace PDFPlayground.Dtos{
    public class LocationDto{
        required public string Name { get; set; }
        required public string RNC { get; set; }
        required public string Address { get; set; }
        required public string PhoneNumber { get; set; }
        required public string Email { get; set; }
        public byte[]? LogoImg { get; set; }
    }
}