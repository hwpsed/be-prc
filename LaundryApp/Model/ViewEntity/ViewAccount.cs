using System.ComponentModel.DataAnnotations;

namespace LaundryApp.Model.ViewEntity
{
    public class ViewAccount
    {
        public int AccountId { get; set; }
        [Required]
        [StringLength(8, MinimumLength = 2)]
        public string Password { get; set; }
        public string AccountName { get; set; }
        public string Username { get; set; }
        public string Address { get; set; }
        public string Token { get; set; }
    }
}
