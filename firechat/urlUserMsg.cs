namespace firechat
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("urlUserMsg")]
    public partial class urlUserMsg
    {
        public int id { get; set; }

        public int? urlId { get; set; }

        public int? msgId { get; set; }

        public int? userId { get; set; }
    }
}
