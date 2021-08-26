using ClassAndStudent.School.Business_Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassAndStudent.School.Services
{
    public interface ISchoolService
    {
        void CreateBatch(Batch batch);
        void DeleteBatch(int id);
        (IList<Batch> records, int total, int totalDisplay) GetBatches(int pageIndex, int pageSize,
                                                                 string searchText, string sortText);
        Batch Getbatches(int id);
        void Updatebatch(Batch batch);
    }
}
