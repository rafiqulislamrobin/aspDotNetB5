using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork.Gallery.Busieness_Object
{
    public class PhotoBusinessObject
    {
        public int Id { get; set; }
        public string PhotoFileName { get; set; }
        public int MemberId { get; set; }
    }
}
