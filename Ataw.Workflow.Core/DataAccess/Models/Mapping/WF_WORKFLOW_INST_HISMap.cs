using System.ComponentModel.DataAnnotations;
using System.Data.Entity.ModelConfiguration;

namespace Ataw.Workflow.Core.DataAccess.Mapping
{
    public class WF_WORKFLOW_INST_HISMap : EntityTypeConfiguration<WF_WORKFLOW_INST_HIS>
    {
        public WF_WORKFLOW_INST_HISMap()
        {
            // Primary Key
            this.HasKey(t => t.WI_ID);

            // Properties
            this.Property(t => t.WI_ID);
            // .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.WI_WD_NAME)
                .HasMaxLength(500);

            this.Property(t => t.WI_NAME)
                .HasMaxLength(500);

            this.Property(t => t.WI_CREATE_USER)
                .HasMaxLength(64);

            this.Property(t => t.WI_END_STATE);
            //   .IsFixedLength()
            //.HasMaxLength(1);

            this.Property(t => t.WI_END_USER)
                .HasMaxLength(64);

            this.Property(t => t.WI_REF_LIST)
                .HasMaxLength(2000);

            this.Property(t => t.WI_INFO1)
                .HasMaxLength(255);

            this.Property(t => t.WI_INFO2)
                .HasMaxLength(255);

            this.Property(t => t.WI_UNUSED3)
                .HasMaxLength(255);

            this.Property(t => t.WI_UNUSED4)
                .HasMaxLength(255);

            this.Property(t => t.WI_UNUSED5)
                .HasMaxLength(255);

            // Table & Column Mappings
            this.ToTable("WF_WORKFLOW_INST_HIS");
            this.Property(t => t.WI_ID).HasColumnName("WI_ID");
            this.Property(t => t.WI_WD_NAME).HasColumnName("WI_WD_NAME");
            this.Property(t => t.WI_CONTENT_XML).HasColumnName("WI_CONTENT_XML");
            this.Property(t => t.WI_NAME).HasColumnName("WI_NAME");
            this.Property(t => t.WI_CREATE_DATE).HasColumnName("WI_CREATE_DATE");
            this.Property(t => t.WI_CREATE_USER).HasColumnName("WI_CREATE_USER");
            this.Property(t => t.WI_END_STATE).HasColumnName("WI_END_STATE");
            this.Property(t => t.WI_WE_ID).HasColumnName("WI_WE_ID");
            this.Property(t => t.WI_END_DATE).HasColumnName("WI_END_DATE");
            this.Property(t => t.WI_END_USER).HasColumnName("WI_END_USER");
            this.Property(t => t.WI_REF_LIST).HasColumnName("WI_REF_LIST");
            this.Property(t => t.WI_IS_TIMEOUT).HasColumnName("WI_IS_TIMEOUT");
            this.Property(t => t.WI_TIMEOUT_DATE).HasColumnName("WI_TIMEOUT_DATE");
            this.Property(t => t.WI_PRIORITY).HasColumnName("WI_PRIORITY");
            this.Property(t => t.WI_CONTENT_DATA).HasColumnName("WI_CONTENT_DATA");
            this.Property(t => t.WI_INFO1).HasColumnName("WI_INFO1");
            this.Property(t => t.WI_INFO2).HasColumnName("WI_INFO2");
            this.Property(t => t.WI_UNUSED1).HasColumnName("WI_UNUSED1");
            this.Property(t => t.WI_UNUSED2).HasColumnName("WI_UNUSED2");
            this.Property(t => t.WI_UNUSED3).HasColumnName("WI_UNUSED3");
            this.Property(t => t.WI_UNUSED4).HasColumnName("WI_UNUSED4");
            this.Property(t => t.WI_UNUSED5).HasColumnName("WI_UNUSED5");
            this.Property(t => t.WI_ERROR_TYPE).HasColumnName("WI_ERROR_TYPE");
            this.Property(t => t.WI_RETRY_TIMES).HasColumnName("WI_RETRY_TIMES");
            this.Property(t => t.WI_MAX_RETRY_TIMES).HasColumnName("WI_MAX_RETRY_TIMES");
            this.Property(t => t.WI_STATUS).HasColumnName("WI_STATUS");
            this.Property(t => t.WI_NEXT_EXE_DATE).HasColumnName("WI_NEXT_EXE_DATE");
            this.Property(t => t.WI_PARENT_ID).HasColumnName("WI_PARENT_ID");
            this.Property(t => t.WI_PC_FLAG).HasColumnName("WI_PC_FLAG");
            this.Property(t => t.WI_APPROVE).HasColumnName("WI_APPROVE");
            this.Property(t => t.WI_APPROVE_NOTE).HasColumnName("WI_APPROVE_NOTE");
            this.Property(t => t.FControlUnitID).HasColumnName("FControlUnitID");
        }
    }
}
