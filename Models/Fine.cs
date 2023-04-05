public class Fine{

    public string FineID {get; set; }
    public string FullName {get; set;}
    public double AmountDue {get; set;} //in GBP
    public DateOnly DateIssued {get; set;}

    public bool IsPaid {get; set;}

    public Fine(string fineID, string fullName, double amountDue, DateOnly dateIssued, bool isPaid)
    {
        this.FineID = fineID;
        this.FullName = fullName;
        this.AmountDue = amountDue;
        this.DateIssued = dateIssued;
        this.IsPaid = isPaid;
    }
}