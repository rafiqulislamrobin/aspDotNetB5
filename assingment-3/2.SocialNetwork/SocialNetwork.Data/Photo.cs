﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Data
{
    public class Photo
    {
        public int Id { get; set; }
        public string PhotoFileName { get; set; }
        public Member memberId { get; set; }

    }
}
