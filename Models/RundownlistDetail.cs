namespace ApiServer.Models
{
    public class RundownlistDetail
    {
        

        public int id { get; set; }
        public int targetID { get; set; }
        public int roleid { get; set; }
        public int   rundownId { get; set; }
        public string status { get; set; }
        public string slugName { get; set; }
        public string slugType { get; set; }
        public string reporter { get; set; }
        public string assignTo { get; set; }
        public string acceptedBy { get; set; }
        public string createDate { get; set; }
        public string gfx_attachment { get; set; }
        public string video_attachment { get; set; }
        public string remarks { get; set; }
         public string story_editor { get; set; }


     
    }
    
}