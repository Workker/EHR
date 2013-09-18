using System.Data;
using System.Data.OleDb;

namespace ImportTables.Repository
{
    public class ExcelRepository
    {
        public DataTable GetAllDataFrom(string path, string fileName, string sheetName)
        {
            var file = string.Format("{0}\\" + fileName, path);
            var connectionString = string.Format("Provider=Microsoft.Jet.OLEDB.4.0; data source={0}; Extended Properties=Excel 8.0;", file);

            var adapter = new OleDbDataAdapter("SELECT * FROM [" + sheetName + "$]", connectionString);
            var ds = new DataSet();

            adapter.Fill(ds, "Table");

            return ds.Tables["Table"];
        }
    }
}