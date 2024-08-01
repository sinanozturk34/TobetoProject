using Core.DataAccess.Repositories;
using DataAccess.Abstracts;
using DataAccess.Contexts;
using Entities.Concretes;

namespace DataAccess.Concretes;

public class EfStudentDal : EfRepositoryBase<Student, int, TobetoContext>, IStudentDal
{
    public EfStudentDal(TobetoContext context) : base(context)
    {
    }
}
