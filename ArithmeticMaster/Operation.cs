using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sansagol.ArithmeticMaster
{
    public sealed class OperationType
    {
        private readonly String name;
        private readonly int value;
        private readonly string operationSymbol;

        public static readonly OperationType Addition = new OperationType(1, "Сложение (+)", "+");
        public static readonly OperationType Subtraction = new OperationType(2, "Вычитание (-)", "-");
        public static readonly OperationType Multiplication = new OperationType(3, "Умножение (*)", "*");
        public static readonly OperationType Division = new OperationType(4, "Деление (/)", "/");

        private OperationType(int value, String name, string symbol)
        {
            this.name = name;
            this.value = value;
            operationSymbol = symbol;
        }

        public override String ToString()
        {
            return name;
        }

        public string GetSymbol()
        {
            return operationSymbol;
        }
    }

    public class OperationExecuter
    {
        public static decimal ExecOperation(decimal FirstOperand, decimal SecondOperand, OperationType TypeOfOperation)
        {
            if (TypeOfOperation == OperationType.Addition)
            {
                return FirstOperand + SecondOperand;
            }
            else if (TypeOfOperation == OperationType.Subtraction)
            {
                return FirstOperand - SecondOperand;
            }
            else if (TypeOfOperation == OperationType.Multiplication)
            {
                return FirstOperand * SecondOperand;
            }
            else if (TypeOfOperation == OperationType.Division)
            {
                return FirstOperand / SecondOperand;
            }

            return 0;
        }
    }
}
