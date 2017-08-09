using System;
using System.Collections.Generic;
using System.Text;
using ePlus.MetaData.Core;

namespace ImportNovgorod
{
    class RequiredAttribute : Attribute
    {
        bool required = true;

        public bool Required
        {
            get { return required; }
        }

        public RequiredAttribute()
        {
        }

        public RequiredAttribute(bool required)
        {
            this.required = required;
        }

        public static bool IsEmpty(object value)
        {
            if (value == null)
                return true;
            if (value is string && string.IsNullOrEmpty(((string)value).Trim()))
                return true;
            if (value is decimal && (decimal)value == 0)
                return true;
            if (value is double && (double)value == 0)
                return true;
            if (value is int && (int)value == 0)
                return true;
            if (value is long && (long)value == 0)
                return true;
            if (value is DateTime && Utils.GetSqlDate((DateTime)value) == DateTime.MinValue)
                return true;
            if (value is Guid && (Guid)value == Guid.Empty)
                return true;
            return false;
        }

    }

    class FormatAttribute : Attribute
    {
        private string format;

        public FormatAttribute()
        {
            this.format = string.Empty;
        }

        private bool checkEmpty = true;

        public bool CheckEmpty
        {
            get { return checkEmpty; }
        }

        public FormatAttribute(string format, bool checkEmpty)
        {
            this.format = format;
            this.checkEmpty = checkEmpty;
        }

        public FormatAttribute(string format)
        {
            this.format = format;
        }

        public string Format(object value)
        {
            if (value == null)
                return string.Empty;
            if (checkEmpty && RequiredAttribute.IsEmpty(value))
                return string.Empty;
            if (value is IFormattable && !string.IsNullOrEmpty(format))
                return ((IFormattable)value).ToString(format, null);
            return value.ToString();
        }
    }

    interface IObject
    {
        Guid ID_GLOBAL
        {
            get;
        }
    }

    interface ISupportsErrorState
    {
        List<RowError> Errors
        {
            get;
        }
    }
    enum RowErrorLevel { Critical, Warning, None }

    class RowError
    {
        RowErrorLevel rowErrorLevel;
        string errorText = string.Empty;

        public RowErrorLevel RowErrorLevel
        {
            get { return rowErrorLevel; }
        }

        public string ErrorText
        {
            get { return errorText; }
        }

        public RowError(RowErrorLevel rowErrorLevel, string errorText)
        {
            this.rowErrorLevel = rowErrorLevel;
            this.errorText = errorText;
        }

        public static RowError None()
        {
            return new RowError(RowErrorLevel.None, string.Empty);
        }

        public static RowError Critical(string errorText)
        {
            return new RowError(RowErrorLevel.Critical, errorText);
        }

        public static RowError Warning(string errorText)
        {
            return new RowError(RowErrorLevel.Warning, errorText);
        }
    }

    class RowErrorLevelDescription
    {
        RowErrorLevel rowErrorLevel;
        string description = string.Empty;

        public RowErrorLevel RowErrorLevel
        {
            get { return rowErrorLevel; }
        }

        public string Description
        {
            get { return description; }
        }

        public RowErrorLevelDescription(RowErrorLevel rowErrorLevel)
        {
            this.rowErrorLevel = rowErrorLevel;
            switch (rowErrorLevel)
            {
                case RowErrorLevel.Critical:
                    description = "ньхайю";
                    break;
                case RowErrorLevel.Warning:
                    description = "опедсопефдемхе";
                    break;
                case RowErrorLevel.None:
                    break;
            }
        }

        public override string ToString()
        {
            return description;
        }
    }
}
