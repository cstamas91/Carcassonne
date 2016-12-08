using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarcassonneUnitTest
{
    public abstract class BaseTest
    {
        protected const string providerInvariantName = @"Microsoft.VisualStudio.TestTools.DataSource.CSV";
        private TestContext testContextInstance;


        public TestContext TestContext
        {
            get { return testContextInstance; }
            set { testContextInstance = value; }
        }

        protected static T ReadEnum<T>(DataRow row, string column)
            where T : struct
        {
            string cellContent = row[column].ToString();
            return (T)Enum.Parse(typeof(T), cellContent);
        }

        protected static List<T> ReadEnumList<T>(DataRow row, string column)
        {
            string cellContent = row[column].ToString();
            if (string.IsNullOrEmpty(cellContent))
                return new List<T>();

            string[] enumValues = cellContent.Split(';');
            return enumValues.Select(str => (T)Enum.Parse(typeof(T), str)).ToList();
        }
    }
}
