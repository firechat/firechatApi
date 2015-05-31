namespace firechat
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class firechatDb : DbContext
    {
        public firechatDb()
            : base("name=firechatDb")
        {
        }

        public virtual DbSet<msg> msgs { get; set; }
        public virtual DbSet<url> urls { get; set; }
        public virtual DbSet<urlUserMsg> urlUserMsgs { get; set; }
        public virtual DbSet<user> users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
