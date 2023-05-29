using System.ComponentModel.DataAnnotations;


namespace ProiectWebData.Entities
{
    public class Items
    {
        [Key]
        public int ItemId { get; set; }
        
        public string Title { get; set; } 

        public string Brand { get; set; }

        public string Description { get; set; }

        public float Price { get; set; }   

        public byte[]? ThumbNail { get; set; }

        public byte[]? Image { get; set; } 

        public DateTime? DateOfCreation { get; set; } = DateTime.Now;    

        public bool IsPromoted { get; set; }

        public bool IsPopular { get; set; } 
    }
}
