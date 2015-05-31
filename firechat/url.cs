namespace firechat
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("url")]
    public partial class url
    {
        public int id { get; set; }

        [StringLength(500)]
        public string value { get; set; }
    }
}
