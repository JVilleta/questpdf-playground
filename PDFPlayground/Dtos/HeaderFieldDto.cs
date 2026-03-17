namespace PDFPlayground.Dtos{
    public class HeaderFieldDto<T>{
       required public string Label { get; set; }
       required public Func<T, object?> ValueSelector { get; set; }
       public Func<object?, string>? Formatter { get; set; }
    }
}