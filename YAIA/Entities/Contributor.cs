namespace Entities
{
    public class Contributor
    {
        public int NIF { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public override string ToString()
        {
            return $"NIF={NIF}; Name={Name}; Address={Address};";
        }
    }
}