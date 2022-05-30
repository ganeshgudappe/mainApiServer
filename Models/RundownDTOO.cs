namespace ApiServer.Models
{
    public class RundownDTOO
    {
            public int rundownId { get; set; }
            public string rundownName { get; set; }
            public string date { get; set; }
            public string time { get; set; }
            public string rundownStatus { get; set; }
            public string rundownRemarks { get; set; }
            public string createdBy { get; set; }
            public string creationTime { get; set; }
    }
}