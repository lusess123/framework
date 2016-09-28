using System.Xml.Serialization;

namespace Ataw.Framework.Core
{
    public class ScoreConfig
    {
        [XmlAttribute]
        public bool showRateInfo { get; set; }//Default : true - Disable the rate info. Can be set to true or false禁用速率信息。可以设置为真或假的
        [XmlAttribute]
        public string bigStarsPath { get; set; }//Default : 'jquery/icons/stars.png' - Relative path of the large star picture (stars.png).相对路径的大星星照片(png)
        [XmlAttribute]
        public string smallStarsPath { get; set; }//Default : 'jquery/icons/small.png' - Relative path of the small star picture (small.png).相对路径的小星星照片(png)
        [XmlAttribute]
        public string phpPath { get; set; }//Default : 'php/jRating.php' - Relative path of the PHP page (jRating.php).相对路径的PHP页面(jRating.php)
        [XmlAttribute]
        public string type { get; set; }//Default : 'big' - Appearance type. Can be set to 'small' or 'big'.外观类型。可以设置为“小”或“大”。
        [XmlAttribute]
        public bool step { get; set; }//Default : false - If set to true, filling of the stars is done star by star (step by step).如果设置为真,灌满星星做星星的星星(逐步)
        [XmlAttribute]
        public bool isDisabled { get; set; }//Default : false - If set to true, jRating is disabled如果设置为真,jRating是禁用的
        [XmlAttribute]
        public int length { get; set; }//Default : 5 - Number of star to display.显示星星的数量。
        [XmlAttribute]
        public int decimalLength { get; set; }//Default : 0 - Number of decimals in the rate数量的小数的速率
        [XmlAttribute]
        public int rateMax { get; set; }//Default : 20 - Maximal rate最大速率
        [XmlAttribute]
        public int rateInfosX { get; set; }//Default : 45 - In pixel - Absolute left position of the information box during mousemove.绝对离开位置的信息框在mousemove。
        [XmlAttribute]
        public int rateInfosY { get; set; }//Default : 5 - In pixel - Absolute top position of the information box during mousemove.绝对的顶尖位置的信息框mousemove期间
        [XmlAttribute]
        public bool canRateAgain { get; set; }//Default : false - if true, visitor can rate {nbRates} times (see {nbRates} option below)如果真的,参观者可以速率{ nbRates }次(见{ nbRates }选项下面)
        [XmlAttribute]
        public int nbRates { get; set; }//Default : 1 - If {canRateAgin}, number of times that a visitor can rate如果{ canRateAgin },次数,访问者可以率
        [XmlAttribute]
        public int average { get; set; }//默认选几个星星
        [XmlAttribute]
        public int rate { get; set; }//选了几个星星
        [XmlAttribute]
        public string data_id { get; set; }
    }
}
