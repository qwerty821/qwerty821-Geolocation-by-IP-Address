namespace WebApplication.Models
{
    public class Question
    {

        public int id { get; set; }
        public string intrebare { get; set; }
        public List<string> variante { get; set; }  
        public int corect {  get; set; }

        public Question(int idd, string con, List<string> ans, int cor)
        {
            id = id;
            intrebare = con;
            variante = ans;
            corect = cor;
        }
    }
}
