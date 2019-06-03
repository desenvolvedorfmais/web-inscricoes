public class GerenciaNet {
    public long Code { get; set; }
    public Boleto Data { get; set; }
}

public class Boleto {
    public System.Int64 Charge_Id { get; set; }
    public string BarCode { get; set; }
    public string Link { get; set; }
    public string Status { get; set; }
    public decimal Total { get; set; }
    public string Custom_Id { get; set; }
    public System.DateTime Created_At { get; set; }
    public System.DateTime Expire_At { get; set; }
    public string Payment { get; set; }
    public Pdf Pdf { get; set; }
}

public class Pdf {
    public string Charge { get; set; }
}