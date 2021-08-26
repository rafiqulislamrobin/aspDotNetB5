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

    public class EditBatchModel
    {
        public int Id { get; set; }
        [Required]
        public int ClassNumber { get; set; }
        [Required]
        public int RoomNumber { get; set; }


        private readonly ISchoolService _iSchoolService;
        private readonly IMapper _mapper;

        public EditBatchModel()
        {
            _iSchoolService = Startup.AutofacContainer.Resolve<ISchoolService>();
            _mapper = Startup.AutofacContainer.Resolve<IMapper>();
        }
        public EditBatchModel(ISchoolService iSchoolService, IMapper mapper)
        {
            _iSchoolService = iSchoolService;
            _mapper = mapper;
        }


        internal void LoadModelData(int id)
        {
            var batch = _iSchoolService.Getbatches(id);

            Id = batch.Id;
          
            ClassNumber = batch.ClassNumber;
            RoomNumber = batch.RoomNumber;
        }

        internal void Update()
        {
            var batch = _mapper.Map<Batch>(this);
            _iSchoolService.Updatebatch(batch);
        }
    }
}
