using System.ComponentModel.DataAnnotations;

namespace SGRP.Aliexpress.Data.Entities.Base
{
    public abstract class Entity
    {
        [Key]
        public virtual long Id { get; set; }
    }
}
