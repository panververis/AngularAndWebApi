namespace AngularAndWebApi.Models.DTOs
{
    public class StaffDTO
    {
        public int      ID          { get; set; }
        public string   FirstName   { get; set; }
        public string   LastName    { get; set; }
        public JobType  JobType     { get; set; }
    }
}