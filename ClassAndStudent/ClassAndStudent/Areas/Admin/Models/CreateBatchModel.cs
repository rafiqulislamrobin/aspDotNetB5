using Autofac;
using AutoMapper;
using ClassAndStudent.School.Business_Object;
using ClassAndStudent.School.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ClassAndStudent.Areas.Admin.Models
{
    public class CreateBatchModel
    {

    
        
        [Required]
        public int ClassNumber { get; set; }
        [Required]
        public int RoomNumber { get; set; }

        private readonly ISchoolService _iSchoolService;
        private readonly IMapper _mapper;

        public CreateBatchModel()
        {
            _iSchoolService = Startup.AutofacContainer.Resolve<ISchoolService>();
            _mapper = Startup.AutofacContainer.Resolve<IMapper>();
        }
        public CreateBatchModel(ISchoolService iSchoolService, IMapper mapper)
        {
            _mapper = mapper;
            _iSchoolService = iSchoolService;
        }
        internal void CreateBatch()
        {

            var batch = _mapper.Map<Batch>(this);


            _iSchoolService.CreateBatch(batch);
        }
    }
}
