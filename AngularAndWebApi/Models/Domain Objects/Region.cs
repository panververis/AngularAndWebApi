namespace AngularAndWebApi.Models
{
    public class Region
    {
        public int              ID          { get; set; }
        public string           Name        { get; set; }

        // Foreign Key (==> to Area)
        public int              AreaID      { get; set; }
        // Navigation property (==> to Area)
        public virtual  Area    Area        { get; set; }
    }
}