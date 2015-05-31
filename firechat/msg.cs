namespace firechat
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("msg")]
    public partial class msg
    {
        public int id { get; set; }

        [StringLength(50)]
        public string value { get; set; }
    }
}
