using KellermanSoftware.CompareNetObjects;
using System.Collections.Generic;

namespace EHR.Infrastructure.Util
{
    public static class CompareTwoObjects
    {
        public static List<Difference> Compare<T>(T object1, T object2)
        {
            var list = new List<Difference>();
            var compareObjects = new CompareObjects();

            if (!compareObjects.Compare(object1, object2))
                foreach (var s in compareObjects.Differences)
                {
                    var diference = new Difference(s.PropertyName, s.Object1Value, s.Object2Value);

                    list.Add(diference);
                }


            return list;
        }
    }

    public class Difference
    {
        public string PropertyName { get; set; }
        public string Expected { get; set; }
        public string Actual { get; set; }

        public Difference(string propertyName, string expected, string actual)
        {
            PropertyName = propertyName;
            Expected = expected;
            Actual = actual;
        }
    }
}
