using System;

namespace Ataw.Workflow.Core.DataAccess
{
    public class WF_WORKFLOW_DEF
    {
        public string WD_ID { get; set; }
        public string WD_SHORT_NAME { get; set; }//��ʶ��
        public string WD_NAME { get; set; }//����
        public string WD_DESCRIPTION { get; set; }//����
        public string WD_CONTENT { get; set; }//��������
        public Nullable<int> WD_IS_USED { get; set; }//�Ƿ�ʹ��
        public string WD_VERSION { get; set; }//�汾��
        public string WD_VERSION_DESC { get; set; }//�汾����
        public string WD_CREATE_ID { get; set; }//������
        public System.DateTime WD_CREATE_DATE { get; set; }//����ʱ��
        public string WD_UPDATE_ID { get; set; }
        public System.DateTime WD_UPDATE_DATE { get; set; }
        public Nullable<int> WD_UNUSED1 { get; set; }
        public Nullable<int> WD_UNUSED2 { get; set; }
        public string WD_UNUSED3 { get; set; }
        public string WD_UNUSED4 { get; set; }
        public string WD_UNUSED5 { get; set; }
        public string FControlUnitID { get; set; }
    }
}
