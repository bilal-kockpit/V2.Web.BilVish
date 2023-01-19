using V2.Models.Master;

namespace V2.ViewModels.Master
{
    public class ApprovalTemplateViewModel
    {
        public ApprovalTemplate ApprovalTemplate { get; set; }
        public ApprovalTemplateDocument ApprovalTemplateDocument { get; set; }
        public ApprovalTemplateOriginator ApprovalTemplateOriginator { get; set; }
        public ApprovalTemplateStage ApprovalTemplateStage { get; set; }
    }
}
