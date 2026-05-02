namespace Aromo.Models
{
    public class PerfumeFeaturesViewModel
    {
        public IEnumerable<Brand> Brands { get; set; } = new List<Brand>();
        public IEnumerable<Season> Seasons { get; set; } = new List<Season>();
        public IEnumerable<Gender> Genders { get; set; } = new List<Gender>();
        public IEnumerable<Scent> Scents { get; set; } = new List<Scent>();
    }
}
