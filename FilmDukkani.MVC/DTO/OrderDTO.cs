namespace FilmDukkani.MVC.DTO
{
    public class OrderDTO
    {
        public string OrderNumber { get; set; }

        public string UserId { get; set; }
        public string UserName { get; set; }
        public bool IsActive { get; set; }
        public bool IsShipped { get; set; }

        public DateTime? OrderDate { get; set; }

        public List<MovieDTO> Movies { get; set; } = new List<MovieDTO>();
    }
}

