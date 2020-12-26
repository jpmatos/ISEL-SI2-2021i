namespace Entity
{
    public class Contributor
    {
        public int Nif { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public override string ToString()
        {
            return $"NIF={Nif}; Name={Name}; Address={Address};";
        }
    }
}