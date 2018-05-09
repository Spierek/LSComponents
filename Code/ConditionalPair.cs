namespace LSTools
{
    public struct ConditionalPair
    {
        public string VariableA;
        public bool ATrue;
        public string VariableB;
        public bool BTrue;
        public ELogicOperator Operator;

        public ConditionalPair(string varA, string varB)
        {
            VariableA = varA;
            ATrue = true;
            VariableB = varB;
            BTrue = true;
            Operator = ELogicOperator.AND;
        }

        public ConditionalPair(string varA, string varB, ELogicOperator op)
        {
            VariableA = varA;
            ATrue = true;
            VariableB = varB;
            BTrue = true;
            Operator = op;
        }

        public ConditionalPair(string varA, bool a, string varB, bool b)
        {
            VariableA = varA;
            ATrue = a;
            VariableB = varB;
            BTrue = b;
            Operator = ELogicOperator.AND;
        }

        public ConditionalPair(string varA, bool a, string varB, bool b, ELogicOperator op)
        {
            VariableA = varA;
            ATrue = a;
            VariableB = varB;
            BTrue = b;
            Operator = op;
        }
    }
}