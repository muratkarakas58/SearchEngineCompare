using Dapper;
using SearchEngineCompare.Constant;
using SearchEngineCompare.Entity;
using SearchEngineCompare.Models.Concrete;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SearchEngineCompare.DataAccess
{
    public static class SearchEngineDal
    {
        public static readonly string LocalDb = @"Server=(localdb)\MSSQLLocalDB; Database=Experiment; Trusted_Connection=true";

        public static Result<string> InsertSearch(Search search)
        {

            try
            {
                int numResults;
                using (var connection = new SqlConnection(LocalDb))
                {
                    string query = @"
                                   INSERT INTO [sec].[Searches]
                                               (
                                    		    [SearchId]
                                               ,[SearchCompareId]
                                               ,[SearchPhrase]
                                               ,[PhraseTypeId]
                                               ,[SearchEngineId]
                                               ,[MeasuredParameterId]
                                               ,[MeasuredParameterPerformance]
                                               ,[CreatedDate])
                                         VALUES
                                               (
                                    		    NEWID()
                                               ,@SearchCompareId
                                               ,@SearchPhrase
                                               ,@PhraseTypeId
                                               ,@SearchEngineId 
                                               ,@MeasuredParameterId
                                               ,@MeasuredParameterPerformance
                                               ,GETDATE()
                                    		   )                                    
                                    ";
                    numResults = connection.Execute(query, param: new
                    {

                        @SearchCompareId = search.SearchCompareId
           ,
                        @SearchPhrase = search.SearchPhrase
           ,
                        @PhraseTypeId = search.PhraseTypeId
           ,
                        @SearchEngineId = search.SearchEngineId
           ,
                        @MeasuredParameterId = search.MeasuredParameterId
           ,
                        @MeasuredParameterPerformance = search.MeasuredParameterPerformance
                    });
                }
                if (numResults > 0)
                {
                    return new Result<string>(true, Messages.Inserted, "");
                }
                else
                {
                    return new Result<string>(false, Messages.NotInserted, "");
                }

            }
            catch (Exception ex)
            {
                return new Result<string>(false, string.Format("{0} {1}", Messages.InsertedFail, ex.Message), null);
            }
        }

        public static Result<DataTable> GetDatatableTop10Search()
        {

            try
            {
                DataTable resultDataTable = new DataTable();
                using (var connection = new SqlConnection(LocalDb))
                {
                    connection.Open();
                    string query = @"
                                SELECT TOP 100 
                                    --SecS.[SearchId]
                                	 SecS.[SearchCompareId]
                                	,SecS.[SearchPhrase]
                                	,SecS.[PhraseTypeId]
                                	,PT.[PhraseTypeDescription]
                                	,SecS.[SearchEngineId]
                                	,SE.SearchEngineDescription
                                	,SecS.[MeasuredParameterId]
                                	,MP.[MeasuredParameterDescription]
                                	,SecS.[MeasuredParameterPerformance]
                                	,SecS.[CreatedDate]
                                	,(
                                		SELECT CASE 
                                				WHEN SecS_In.PhraseTypeId = 1 													
                                					AND SecS_In.MeasuredParameterPerformance < SecS.MeasuredParameterPerformance
                                					THEN SE_In.SearchEngineDescription
                                				WHEN SecS_In.PhraseTypeId = 1 													
                                					AND SecS_In.MeasuredParameterPerformance > SecS.MeasuredParameterPerformance
                                					THEN SE.SearchEngineDescription
                                				WHEN SecS_In.PhraseTypeId = 2 													
                                					AND SecS_In.MeasuredParameterPerformance > SecS.MeasuredParameterPerformance
                                					THEN SE_In.SearchEngineDescription
                                				WHEN SecS_In.PhraseTypeId = 2 													
                                					AND SecS_In.MeasuredParameterPerformance < SecS.MeasuredParameterPerformance
                                					THEN SE.SearchEngineDescription
                                				END
                                		FROM [Experiment].[sec].[Searches] AS SecS_In WITH (NOLOCK)
                                		INNER JOIN [Experiment].[sec].[SearchEngines] AS SE_In WITH (NOLOCK) ON SecS_In.SearchEngineId = SE_In.SearchEngineId
                                		WHERE SecS_In.SearchCompareId = SecS.SearchCompareId
                                			AND SecS_In.SearchEngineId != SecS.SearchEngineId
                                            AND SecS_In.MeasuredParameterId = SecS.MeasuredParameterId
                                		) AS WINNER
                                FROM [Experiment].[sec].[Searches] AS SecS WITH (NOLOCK)
                                INNER JOIN [Experiment].[sec].[PhraseTypes] AS PT WITH (NOLOCK) ON SecS.PhraseTypeId = PT.PhraseTypeId
                                INNER JOIN [Experiment].[sec].[SearchEngines] AS SE WITH (NOLOCK) ON SecS.SearchEngineId = SE.SearchEngineId
                                INNER JOIN [Experiment].[sec].[MeasuredParameters] AS MP WITH (NOLOCK) ON SecS.MeasuredParameterId = MP.MeasuredParameterId
                                WHERE 1 = 1
                                ORDER BY Convert(date,CreatedDate) DESC, DATEPART(HOUR,CreatedDate) desc, DATEPART(MINUTE,CreatedDate) desc, SearchCompareId, MeasuredParameterId
                                ";
                    var reader = connection.ExecuteReader(sql: query, commandTimeout: 1000);
                    resultDataTable.Load(reader);
                    connection.Close();
                }

                return new Result<DataTable>(true, string.Format("{0}", Messages.Successful), resultDataTable);
            }
            catch (Exception ex)
            {
                return new Result<DataTable>(false, string.Format("{0} {1}", Messages.DatabaseQueryError, ex.Message), null);
            }
        }

        public static Result<string> InsertTempTest(string description)
        {

            try
            {
                int numResults;
                using (var connection = new SqlConnection(LocalDb))
                {
                    string query = @"
                                   INSERT INTO [sec].[TempTest]
           (
		   
           [Description]
           
		   )
     VALUES
           (
		   
           @Description
           
		   )                                
                                    ";
                    numResults = connection.Execute(query, param: new
                    {

                        @Description = description

                    });
                }
                if (numResults > 0)
                {
                    return new Result<string>(true, Messages.Inserted, "");
                }
                else
                {
                    return new Result<string>(false, Messages.NotInserted, "");
                }

            }
            catch (Exception ex)
            {
                return new Result<string>(false, string.Format("{0} {1}", Messages.InsertedFail, ex.Message), null);
            }
        }


        public static Result<DataTable> GetDatatableFromSearchTopChoiceBySearchEngineIdAndMeasuredParameterId(int searchEngineId , int measuredParameterId, int showRowCount) 
        {
             
            try
            {
                DataTable resultDataTable = new DataTable();
                using (var connection = new SqlConnection(LocalDb))
                {
                    connection.Open();
                    string query = @"
                                SELECT TOP (@ShowRowCount)
                                	   [SearchId]
                                      ,[SearchCompareId]
                                      ,[SearchPhrase]
                                      ,[PhraseTypeId]
                                      ,[SearchEngineId]
                                      ,[MeasuredParameterId]
                                      ,[MeasuredParameterPerformance]
                                      ,[CreatedDate]
                                  FROM [Experiment].[sec].[Searches] AS SecS WITH(NOLOCK)
                                  where 1=1
                                  and SecS.SearchEngineId=@SearchEngineId
                                  and SecS.MeasuredParameterId=@MeasuredParameterId
                                  order by CreatedDate desc
                                ";
                    var reader = connection.ExecuteReader(sql: query, param:new { @SearchEngineId=searchEngineId , @MeasuredParameterId= measuredParameterId, @ShowRowCount=showRowCount }, commandTimeout: 1000); 
                    resultDataTable.Load(reader);
                    connection.Close();
                }

                return new Result<DataTable>(true, string.Format("{0}", Messages.Successful), resultDataTable);
            }
            catch (Exception ex)
            {
                return new Result<DataTable>(false, string.Format("{0} {1}", Messages.DatabaseQueryError, ex.Message), null);
            }
        }


    }
}
