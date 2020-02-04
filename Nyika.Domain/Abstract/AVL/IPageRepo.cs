using Nyika.Domain.Entities.AVL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyika.Domain.Abstract.AVL
{
    public interface IPageRepo
    {
        IEnumerable<Page> Page { get; }
        void SavePage(Page Page);
        Page DeletePage(long PageID);

    }
}
