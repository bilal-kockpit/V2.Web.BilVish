namespace V2.Models.Master
{
    public class StageHeader
    {
        public int StageId { get; set; }
        public string StageName { get; set; }
        public string Description { get; set; }
        public int ApproverRequired { get; set; }
        public int RejectionRequired { get; set; }

    }
}
