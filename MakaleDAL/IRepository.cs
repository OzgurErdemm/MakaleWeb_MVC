using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MakaleDAL
{
    public interface IRepository<T>
    {
        int Insert(T nesne);
        int Update(T nesne);    
        int Delete(T nesne);
        List<T>  Liste();
        List<T> Liste(Expression<Func<T,bool>> kosul);
        IQueryable<T> ListQueryable();
        T Find(Expression<Func<T, bool>> kosul);
    }
}
