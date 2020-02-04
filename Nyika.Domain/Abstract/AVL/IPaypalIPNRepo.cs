using Nyika.Domain.Entities.AVL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nyika.Domain.Abstract.AVL
{
    public interface IPaypalIPNRepo
    {
        IEnumerable<PaypalIPN> PaypalIPN { get; }
        void SavePaypalIPN(PaypalIPN PaypalIPN);
        void CSActive(string Custom, string TXNID,string ItemName);
        PaypalIPN DeletePaypalIPN(long PaypalIPNID);
    }
}
