using System;
using System.Globalization;
using Lte.Domain.Common;
using Lte.Domain.LinqToCsv;
using Lte.Domain.LinqToCsv.Test;
using NUnit.Framework;

namespace Lte.Domain.Test.LinqToCsv.Product
{
    public class ProductData : IAssertable<ProductData>
    {
        [CsvColumn(FieldIndex = 1)]
        public string Name { get; set; }

        // OutputFormat uses the same codes as the standard ToString method (search MSDN).

        [CsvColumn(FieldIndex = 2, OutputFormat = "d")]
        public DateTime StartDate { get; set; }


        [CsvColumn(FieldIndex = 3, OutputFormat = "dd MMM HH:mm:ss")]
        public DateTime LaunchTime { get; set; }

        // Can use both fields and properties
        [CsvColumn(FieldIndex = 4, CanBeNull = false, OutputFormat = "#,000.000")]
        public double Weight { get; set; }

        // Following field has no CsvColumn attribute.
        // So will be ignored when library is told to only use data with CsvColumn attribute.

        public int NbrAvailable { get; set; }

        // Ok to have gaps in FieldIndex order

        [CsvColumn(FieldIndex = 10)]
        public string ShopsAvailable { get; set; }

        // Override field name, so in data files, this field is known as "code" instead of "hexProductCode"
        // This field contains hexadecimal numbers, without leading 0x.
        // This requires setting the NumberStyle property to NumberStyles.HexNumber.
        // Don't forget to import the namespace System.Globalization

        [CsvColumn(Name = "Code", FieldIndex = 11, NumberStyle = NumberStyles.HexNumber)]
        public int HexProductCode { get; set; }

        public string UnusedField { get; set; }

        // FieldIndex order higher then that of next field.
        // So this field will come AFTER next field in the actual data file

        [CsvColumn(FieldIndex = 16)]
        public bool Onsale { get; set; }

        // Override field name, so in data files, this field is known as "price" instead of "retailPrice"
        // OutputFormat uses the same codes as the standard ToString method (search MSDN). Format "C" is for currency.
        [CsvColumn(Name = "Price", FieldIndex = 14, CanBeNull = false, OutputFormat = "C")]
        public decimal RetailPrice { get; set; }


        [CsvColumn(FieldIndex = 30)]
        public string Description { get; set; }

        public void AssertEqual(ProductData other)
        {
            Assert.AreNotEqual(other, null);

            Assert.AreEqual(other.Name, Name, "name");
            Assert.AreEqual(other.StartDate, StartDate, "startDate");
            Assert.AreEqual(other.LaunchTime, LaunchTime, "launchTime");
            Assert.AreEqual(other.Weight, Weight, "weight");
            Assert.AreEqual(other.NbrAvailable, NbrAvailable, "nbrAvailable");
            Assert.AreEqual(Util.NormalizeString(other.ShopsAvailable), Util.NormalizeString(ShopsAvailable), "shopsAvailable");
            Assert.AreEqual(other.HexProductCode, HexProductCode, "hexProductCode");
            Assert.AreEqual(other.Onsale, Onsale, "onsale");
            Assert.AreEqual(other.RetailPrice, RetailPrice, "retailPrice");
            Assert.AreEqual(Util.NormalizeString(other.Description), Util.NormalizeString(Description), "description");
        }
    }
}
