using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;

namespace Ataw.Workflow.Core.DataAccess.Mapping
{
    public class WF_DataTableMap : EntityTypeConfiguration<WF_DataTable>
    {
       public WF_DataTableMap(params string[] tableColumnNames)
       {
           this.ToTable(tableColumnNames[0]);
           if (!string.IsNullOrEmpty(tableColumnNames[1]))
           this.Property(t => t.WF_Column1).HasColumnName(tableColumnNames[1]);
           if (!string.IsNullOrEmpty(tableColumnNames[2]))
           this.Property(t => t.WF_Column2).HasColumnName(tableColumnNames[2]);
       }
    }
}
