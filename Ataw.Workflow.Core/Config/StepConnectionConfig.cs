
using System.Xml.Serialization;
using Ataw.Framework.Core;
namespace Ataw.Workflow.Core
{
    public class StepConnectionConfig : IRegName
    {
        public StepConnectionConfig()
        {
        }

        /// <summary>
        /// Initializes a new instance of the StepConnectionConfig class.
        /// </summary>
        public StepConnectionConfig(string stepName, int fromX, int fromY, int toX, int toY)
        {
            FromX = fromX;
            FromY = fromY;
            ToX = toX;
            ToY = toY;
            StepName = stepName;
        }

        [XmlAttribute]
        public int FromX { get; set; }

        [XmlAttribute]
        public int FromY { get; set; }

        [XmlAttribute]
        public int ToX { get; set; }

        [XmlAttribute]
        public int ToY { get; set; }

        [XmlAttribute]
        public string StepName { get; set; }

        [XmlIgnore]
        public string RegName
        {
            get { return StepName; }
        }
    }
}
