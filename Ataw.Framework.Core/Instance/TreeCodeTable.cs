
namespace Ataw.Framework.Core
{
    public class TreeCodeTableEntity
    {
        //   [CODE_VALUE] [char](6) NOT NULL,
        //[CODE_NAME] [varchar](100) NOT NULL,
        //[CODE_PY] [varchar](50) NULL,
        //[CODE_SORT] [int] NULL,
        //[CODE_DEL] [int] NULL,
        public string CODE_VALUE
        {
            get;
            set;
        }

        public string CODE_NAME
        {
            get;
            set;
        }

        public string CODE_PY
        {
            get;
            set;
        }

        public int CODE_SORT
        {
            get;
            set;
        }
        public int CODE_DEL
        {
            get;
            set;
        }
    }
}
