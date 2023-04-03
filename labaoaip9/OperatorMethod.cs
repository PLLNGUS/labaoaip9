using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace labaoaip9
{
    public class OperatorMethod
    {
        public delegate void EmptyOperatorMethod();
        public delegate void UnaryOperatorMethod(object operand);
        public delegate void BinaryOperatorMethod(object operand1, object operand2);
        public delegate void TrinaryOperatorMethod(object operand1, object operand2, object operand3);

    }
}
