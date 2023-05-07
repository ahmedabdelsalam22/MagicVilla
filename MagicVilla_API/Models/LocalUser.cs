namespace MagicVilla_API.Models
{
	public class LocalUser
	{
        public int Id { get; set; }
        public String UserName { get; set; }
        public String Name { get; set; }
		public String Password { get; set; }
		public string Role { get; set; }
    }
}
