using System.Collections.Generic;

namespace Ataw.Workflow.Web
{
    public class MapModel
    {
        public StepView CurrentStep { get; set; }

        public StepView LastStep { get; set; }//上一步

        public StepView NextStep { get; set; }

        public List<StepView> OtherSteps { get; set; }
    }
}