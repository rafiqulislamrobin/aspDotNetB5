using DataImporter.Info.Business_Object;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.Areas.User.Models
{
    public class ConfirmFile
    {
       public Dictionary<int, Dictionary<string, string>> cont { get; set; }
       public List<string> headers { get; set; }
        internal object ConfirmFileUpload(string filepath)
        {
            cont = new();
            headers = new();

            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = System.IO.File.Open(filepath, FileMode.Open, FileAccess.Read))
            {
                IExcelDataReader reader;

                reader = ExcelDataReader.ExcelReaderFactory.CreateReader(stream);

                //// reader.IsFirstRowAsColumnNames
                var conf = new ExcelDataSetConfiguration
                {
                    ConfigureDataTable = _ => new ExcelDataTableConfiguration
                    {
                        UseHeaderRow = false
                    }
                };
                var dataSet = reader.AsDataSet(conf);



                // Now you can get data from each sheet by its index or its "name"
                var dataTable = dataSet.Tables[0];


                for (var i = 0; i < 1; i++)
                {
                    for (var j = 0; j < dataTable.Columns.Count; j++)
                    {
                        headers.Add(dataTable.Rows[i][j].ToString());

                    }

                }
                for (var i = 1; i < dataTable.Rows.Count; i++)
                {
                    Contact contacts = new();
                    for (var j = 0; j < dataTable.Columns.Count; j++)
                    {
                        var z = dataTable.Rows[i][j].ToString();
                        if (z != null && z != "")
                        {
                            contacts.Properties.Add(headers[j], dataTable.Rows[i][j].ToString());
                        }
                        else
                        {
                            contacts.Properties.Add(headers[j], "no value");
                        }
                    }
                    cont.Add(i, contacts.Properties);

                }

            }
            return (cont, headers);
        }
    }
}
