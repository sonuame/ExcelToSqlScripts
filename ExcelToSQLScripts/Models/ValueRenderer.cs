﻿using System;
using System.Linq;

namespace ExcelToSQLScripts.Models
{
    public class ValueRenderer
    {
        private readonly string[] _nullReplacements;

        public ValueRenderer(string[] nullReplacements)
        {
            _nullReplacements = nullReplacements;
        }

        public string Render(Value value)
        {
            if (string.IsNullOrEmpty(value.StringValue) 
                || string.Equals(value.StringValue, Constants.NULL, StringComparison.OrdinalIgnoreCase)
                || _nullReplacements.Any(x => x.Equals(value.StringValue, StringComparison.OrdinalIgnoreCase)))
                return Constants.NULL;

            switch (value.Column.DataType)
            {
                case DataType.Number:
                    return value.StringValue;

                case DataType.String:
                    return $"'{value.StringValue.Replace("'", "''")}'";

                default:
                    throw new NotImplementedException();
            }
        }
    }
}