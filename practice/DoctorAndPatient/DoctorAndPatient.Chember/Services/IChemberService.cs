using DoctorAndPatient.Chember.Business_Object;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAndPatient.Chember.Services
{
    public interface IChemberService
    {
        (IList<Doctor> records, int total, int totalDisplay) GetDoctors(int pageIndex, int pageSize,
                                                                 string searchText, string sortText);
    }
}
