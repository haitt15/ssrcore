using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using Google.Apis.Sheets.v4.Data;
using ssrcore.Models;
using ssrcore.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ssrcore.Helpers
{
    public class RequestSheetUtils
    {
        static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
        static readonly string ApplicationName = "SSR Core";
        static readonly string sheet = "Sheet1";
        static readonly string SpreadsheetId = "11G2M1iKzud1fonZjUlfYS8-rE0lXLmQgBP49rQSbi_Y";
        static SheetsService service;

        public static void Init()
        {
            GoogleCredential credential;
            //Reading Credentials File...
            using (var stream = new FileStream("sheet project-206c3def15da.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream)
                    .CreateScoped(Scopes);
            }
            // Creating Google Sheets API service...
            service = new SheetsService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = ApplicationName,
            });
        }
        public static dynamic ReadSheet(string spreadSheetId)
        {
            // Specifying Column Range for reading...
            var range = $"{sheet}!A:Z";
            SpreadsheetsResource.ValuesResource.GetRequest request =
                    service.Spreadsheets.Values.Get(spreadSheetId, range);
            // Ecexuting Read Operation...
                var response = request.Execute();
            // Getting all records from Column A to E...
            IList<IList<object>> values = response.Values;
         
            return values;
        }

        //private static void AddProperties()
        //{
        //    ServiceRequestModel model = new ServiceRequestModel
        //    {
        //        TicketId = "Ticket Id",
        //        ServiceNm = "Service Name",
        //        Staff = "Staff",
        //        Status = "Status",
        //        User = "Student"
        //    };
        //    Add(model);
        //}

        public static void Add(ServiceRequestModel requestModel,string spreadSheetId)
        {
            // Specifying Column Range for reading...
            var range = $"{sheet}!A:F";
            var valueRange = new ValueRange();
            // Data for another Student...

            var oblist = new List<object>() { requestModel.TicketId,requestModel.FullName, requestModel.ServiceNm, requestModel.Status,
                requestModel.StaffNm, requestModel.DepartmentNm
            };
            valueRange.Values = new List<IList<object>> { oblist };
            // Append the above record...
            var appendRequest = service.Spreadsheets.Values.Append(valueRange, spreadSheetId, range);
            appendRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.AppendRequest.ValueInputOptionEnum.USERENTERED;
            var appendReponse = appendRequest.Execute();
            //BatchUpdateValuesRequest requestBody = new BatchUpdateValuesRequest()
            //{
            //    ValueInputOption = "USER_ENTERED",
            //    //Data = new List<ValueRange>() { new List<IList<object>> { oblist } }
            //};
            //SpreadsheetsResource.ValuesResource.BatchUpdateRequest request = service.Spreadsheets.Values.BatchUpdate(requestBody, SpreadsheetId);
            //BatchUpdateValuesResponse response = request.Execute();
        }



        public static void Update(ServiceRequestModel requestModel, string spreadSheetId)
        {
            int rowID = 0;
            var range = $"{sheet}!A:F";
            int j = 0;
            SpreadsheetsResource.ValuesResource.GetRequest request =
                    service.Spreadsheets.Values.Get(spreadSheetId, range);
            var response = request.Execute();
            IList<IList<object>> values = response.Values;
            if (values != null && values.Count > 0)
            {
                foreach (var user in values)
                {
                    j++;
                    if (j > 1)
                    {
                        string ticket = (string)user[0];
                        if (ticket.Equals(requestModel.TicketId))
                            rowID = j;
                    }

                }
            }

            var range2 = $"{sheet}!A{rowID}:F{rowID}";
            var valueRange = new ValueRange();
            var oblist = new List<object>() { requestModel.TicketId,requestModel.FullName, requestModel.ServiceNm, requestModel.Status,
                requestModel.StaffNm, requestModel.DepartmentNm
            };
            valueRange.Values = new List<IList<object>> { oblist };
            // Performing Update Operation...
            var updateRequest = service.Spreadsheets.Values.Update(valueRange, spreadSheetId, range2);
            updateRequest.ValueInputOption = SpreadsheetsResource.ValuesResource.UpdateRequest.ValueInputOptionEnum.USERENTERED;
            var appendReponse = updateRequest.Execute();
        }


        public static void ClearRow(int rowID, string spreadSheetId)
        {
            var range = $"{sheet}!A{rowID}:F{rowID}";
            var requestBody = new ClearValuesRequest();
            var deleteRequest = service.Spreadsheets.Values.Clear(requestBody, spreadSheetId, range);
            var deleteReponse = deleteRequest.Execute();
        }

        public static void ClearAllRow( string spreadSheetId)
        {
            var result = ReadSheet(spreadSheetId);
            if(result.Count > 1)
            {
                var range = $"{sheet}!A2:F{result.Count}";
                var requestBody = new ClearValuesRequest();
                var clearRequest = service.Spreadsheets.Values.Clear(requestBody, spreadSheetId, range);
                var clearReponse = clearRequest.Execute();
            }
        }
        public static void Delete(string ticketId, string spreadSheetId)
        {

            int row = 0;
            var range = $"{sheet}!A:F";
            int j = 0;
            SpreadsheetsResource.ValuesResource.GetRequest request =
                    service.Spreadsheets.Values.Get(spreadSheetId, range);
            var response = request.Execute();
            IList<IList<object>> values = response.Values;
            if (values != null && values.Count > 0)
            {
                foreach (var user in values)
                {
                    j++;
                    if (j > 1)
                    {
                        string ticket = (string)user[0];
                        if (ticket.Equals(ticketId)) row = j - 1;
                    }

                }
            }


            Request RequestBody = new Request()
            {
                DeleteDimension = new DeleteDimensionRequest()
                {
                    Range = new DimensionRange()
                    {
                        SheetId = 0,
                        Dimension = "ROWS",
                        StartIndex = row,
                        EndIndex = row + 1
                    }
                }
            };

            List<Request> RequestContainer = new List<Request>();
            RequestContainer.Add(RequestBody);

            BatchUpdateSpreadsheetRequest DeleteRequest = new BatchUpdateSpreadsheetRequest();
            DeleteRequest.Requests = RequestContainer;

            SpreadsheetsResource.BatchUpdateRequest Deletion = new SpreadsheetsResource.BatchUpdateRequest(service, DeleteRequest, spreadSheetId);
            Deletion.Execute();

        }
    }
}
