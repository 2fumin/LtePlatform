using System;
using System.Collections.Generic;
using System.Linq;

namespace Lte.Domain.LinqToExcel.Entities
{   
    public enum ExcelDatabaseEngine
    {
        Jet,
        Ace
    }

    /// <summary>
    /// Class property and worksheet mapping enforcemment type.
    /// </summary>
    public enum StrictMappingType
    {
        /// <summary>
        /// All worksheet columns must map to a class property; all class properties must map to a worksheet columm.
        /// </summary>
        Both,

        /// <summary>
        /// All class properties must map to a worksheet column; other worksheet columns are ignored.
        /// </summary>
        ClassStrict,

        /// <summary>
        /// No checks are made to enforce worksheet column or class property mappings.
        /// </summary>
        None,

        /// <summary>
        /// All worksheet columns must map to a class property; other class properties are ignored.
        /// </summary>
        WorksheetStrict
    }

    /// <summary>
    /// Indicates how to treat leading and trailing spaces in string values.
    /// </summary>
    public enum TrimSpacesType
    {
        /// <summary>
        /// Do not perform any trimming.
        /// </summary>
        None,

        /// <summary>
        /// Trim leading spaces from strings.
        /// </summary>
        Start,

        /// <summary>
        /// Trim trailing spaces from strings.
        /// </summary>
        End,

        /// <summary>
        /// Trim leading and trailing spaces from strings. 
        /// </summary>
        Both
    }
}
