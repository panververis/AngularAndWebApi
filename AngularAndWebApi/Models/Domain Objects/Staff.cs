using System.Collections.Generic;

namespace AngularAndWebApi.Models
{

    public enum JobType
    {
        SalesRep,
        Accounting,
        Manager
    }

    public class Staff
    {
        public int                ID              { get; set; }
        public string             FirstName       { get; set; }
        public string             LastName        { get; set; }
        public JobType            JobType         { get; set; }

        // Foreign Key (==> to Dealer)
        public int                DealerID        { get; set; }

        // Navigation property (==> to Dealer)
        public virtual Dealer     Dealer          { get; set; }

        // "Detail" collection property (==> to Sales)
        public virtual List<Sale> Sales           { get; set; }
    }
}