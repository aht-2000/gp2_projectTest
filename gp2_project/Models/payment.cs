namespace gp2_project.Models
{
    public class payment
    {
        public int Id { get; set; }
        public int orderNo { get; set; }
        public String? nameOnCard { get; set; }
        public int bankAccNo { get; set; }
        public float total { get; set; }
    }
}
