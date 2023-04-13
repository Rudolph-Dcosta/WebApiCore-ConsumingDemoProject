namespace WebApiCoreDemoProject.Models.DTO
{
    public class RegionDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string AreaName { get; set; }

        public double Area { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public long Population { get; set; }
    }
}
