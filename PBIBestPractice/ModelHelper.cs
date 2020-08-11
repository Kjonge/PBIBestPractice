using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AnalysisServices.AdomdClient;
using Microsoft.AnalysisServices.Tabular;

namespace PBIBestPractice
{
    class ModelException : Exception
    {
        public ModelException(string message) : base(message)
        {
        }

    }

       class ModelHelper
    {
        // 
        // Connect to the local default instance of Analysis Services 
        // 
        private string _Server;
        private string _Model;
        private string _dateTable;
        private string _dateColumn;
        private int _tableCount;
        private int _relationshipCount;
        private int _biDirelationshipCount;


        public ModelHelper(string Server, string Model)
        {
            _Server = Server;
            _Model = Model;
        }

        public List<ModelInformation> getTableInfo()
        {
            List<ModelInformation> info = new List<ModelInformation>();
            try
            {
                string connectionstring = string.Format("Data Source={0};Catalog={1}", _Server, _Model);
                using (AdomdConnection conn = new AdomdConnection(connectionstring))
                {
                    conn.Open();

                    string commandText = @"SELECT
                                        DIMENSION_NAME AS TABLE_NAME, 
                                        COLUMN_ID AS COLUMN_ID, 
                                        ATTRIBUTE_NAME AS COLUMN_NAME, 
                                        DATATYPE AS [Data Type],
                                        DICTIONARY_SIZE AS DICTIONARY_SIZE_BYTES,
                                        COLUMN_ENCODING AS COLUMN_ENCODING_INT
                                    FROM  $SYSTEM.DISCOVER_STORAGE_TABLE_COLUMNS
                                    WHERE COLUMN_TYPE = 'BASIC_DATA'
                                    ORDER BY DICTIONARY_SIZE DESC";
                    AdomdCommand cmd = new AdomdCommand(commandText, conn);
                    AdomdDataReader dr = cmd.ExecuteReader();

                    ModelInformation info_rows = new ModelInformation
                    {
                        informationText = String.Format("Top 5 columns by size")
                    };
                    info.Add(info_rows);
                    int rowCount = 0;
                    while (dr.Read())
                    { 
                        ModelInformation info_toprow = new ModelInformation
                        {
                            informationText = String.Format("Table: {0}, Column: {1}, Size: {2}", dr.GetString(0), dr.GetString(2), dr.GetString(4))
                        };
                        info.Add(info_toprow);
                        rowCount++;
                        if (rowCount == 5)
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                //throw it again
                throw ex;
            }
            return info;
        }

        public List<ModelInformation> getBasicModelInfo()
        {
            List<ModelInformation> info = new List<ModelInformation>();
            try
            {
                using (Server server = new Server())
                {
                    server.Connect(_Server);
                    Model dekstopModel = server.Databases[_Model].Model;

                    //check ompat level if needed
                    int compatLevel = server.Databases[0].CompatibilityLevel;
                    ModelInformation info_compatlevel = new ModelInformation
                    {
                        informationText = String.Format("Compat level: {0}", compatLevel)
                    };
                    info.Add(info_compatlevel);


                    _tableCount = dekstopModel.Tables.Count;
                    ModelInformation info_table = new ModelInformation();
                    info_table.informationText = String.Format("Nr of tables: {0}", _tableCount);
                    info.Add(info_table);

                    _relationshipCount = dekstopModel.Relationships.Count;
                    ModelInformation info_relationships = new ModelInformation();
                    info_relationships.informationText = String.Format("Nr of relationships: {0}", _relationshipCount);
                    info.Add(info_relationships);


                    _biDirelationshipCount = 0;
                    foreach (Relationship rel in dekstopModel.Relationships)
                    {
                        if (rel.CrossFilteringBehavior == CrossFilteringBehavior.BothDirections)
                            _biDirelationshipCount++;
                    }
                    ModelInformation info_BiDi = new ModelInformation();
                    if (_biDirelationshipCount == 0)
                        info_BiDi.informationText = String.Format("No BiDi relationships detected");
                    else
                        info_BiDi.informationText = String.Format("{0} BiDi relationships detected", _biDirelationshipCount);

                    info.Add(info_BiDi);


                    bool _hasDatecolumn = false;
                    foreach (Table table in dekstopModel.Tables)
                    {

                        foreach (Column col in table.Columns)
                        {
                            if (col.DataType == DataType.DateTime && col.IsKey == true)
                            {
                                _dateColumn = col.Name;
                                _dateTable = table.Name;
                                _hasDatecolumn = true;
                                break;
                            }
                        }
                    }
                    ModelInformation info_Date = new ModelInformation();
                    if (_hasDatecolumn)
                        info_Date.informationText = String.Format("Date table detected: table {0}, date column {1}",_dateTable, _dateColumn);
                    else
                        info_Date.informationText = String.Format("No date table detected");

                    info.Add(info_Date);
                }

            }
            catch (Exception ex)
            {
                //throw it again
                throw ex;
            }
            return info;

        }

    }
}
