using System;

namespace View
{
    public class Parameter
    {
        public string name;
        public Type type;
        public bool nullable;

        public Parameter(string name, Type type, bool nullable)
        {
            this.name = name;
            this.type = type;
            this.nullable = nullable;
        }
    }
}