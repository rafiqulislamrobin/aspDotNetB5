﻿using Gallery.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gallery.MemberAndImages.Entities
{
    public class Photo : IEntity<int>
    {
        public int Id { get; set; }
        public string PhotoUrl { get; set; }
        public Member Member { get; set; }
    }
}
