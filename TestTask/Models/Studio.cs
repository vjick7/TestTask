﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestTask.Models
{
    public class Studio
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

    }
}
