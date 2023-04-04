using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace labaoaip9
{
   public static class OperatorContainer
    {
        public static List<Operator> operators = new List<Operator>();
        static OperatorContainer()
        {
            operators.Add(new Operator('S'));
            operators.Add(new Operator('M'));
            operators.Add(new Operator('D'));
           
            operators.Add(new Operator(','));
            operators.Add(new Operator('('));
            operators.Add(new Operator(')'));
        }
        public static Operator FindOperator(char s)
        {
            foreach (Operator op in operators)
            {
                if (op.symbolOperator == s)
                {
                    return op;
                }
            }
            return null;
        }

    }
}
