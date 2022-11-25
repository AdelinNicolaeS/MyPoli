using MyPoli.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MyPoli.Entities

{
    public class BadWord : IEntity
    {
        public Guid Id { get; set; }
        public string Value { get; set; }
    }
}
