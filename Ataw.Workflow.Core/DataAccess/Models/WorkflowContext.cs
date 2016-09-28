using System.Data.Entity;
using Ataw.Framework.Core;
using Ataw.Workflow.Core.DataAccess.Mapping;

namespace Ataw.Workflow.Core.DataAccess
{
    public class WorkflowDbContext : AtawDbContext
    {
        public bool isCustomer;
        public string[] Table_Column = { "WF_WORKFLOW_DEF", "WD_SHORT_NAME", "WD_NAME" };
        static WorkflowDbContext()
        {
            Database.SetInitializer<WorkflowDbContext>(null);
        }

        public WorkflowDbContext(string connStr)
            : base(connStr)
        {
        }

        public WorkflowDbContext()
            : base(AtawAppContext.Current.DefaultConnString)
        {
        }

        public DbSet<WF_STEP_INST> WF_STEP_INST { get; set; }
        public DbSet<WF_STEP_INST_HIS> WF_STEP_INST_HIS { get; set; }
        public DbSet<WF_WORKFLOW_DEF> WF_WORKFLOW_DEF { get; set; }
        public DbSet<WF_WORKFLOW_INST> WF_WORKFLOW_INST { get; set; }
        public DbSet<WF_WORKFLOW_INST_HIS> WF_WORKFLOW_INST_HIS { get; set; }
        public DbSet<WF_APPROVE_HISTORY> WF_APPROVE_HISTORY { get; set; }

        public DbSet<WF_WORKFLOW_TRANSFER> WF_WORKFLOW_TRANSFER { get; set; }

       // public DbSet<WF_DataTable> WF_DataTable { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new WF_STEP_INSTMap());
            modelBuilder.Configurations.Add(new WF_STEP_INST_HISMap());
            modelBuilder.Configurations.Add(new WF_WORKFLOW_DEFMap());
            modelBuilder.Configurations.Add(new WF_WORKFLOW_INSTMap());
            modelBuilder.Configurations.Add(new WF_WORKFLOW_INST_HISMap());
            modelBuilder.Configurations.Add(new WF_WORKFLOW_TRANSFERMap());

            modelBuilder.Configurations.Add(new WF_APPROVE_HISTORYMap());

            //if (isCustomer)
            //{
            //    modelBuilder.Configurations.Add(new WF_DataTableMap(Table_Column));
            //}
        }
    }
}
