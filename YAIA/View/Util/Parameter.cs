using System;

namespace View.Util
{
    public class Parameter
    {
        public readonly string Name;
        public readonly Type Type;
        public readonly bool Nullable;
        public string Value { get; set; }

        public Parameter(string name, Type type, bool nullable)
        {
            Name = name;
            Type = type;
            Nullable = nullable;
        }
    }
}