using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Sheets.v4;
using ssrcore.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;

namespace ssrcore.Helpers
{
    public class ReadGoogleSheet
    {
        static readonly string[] Scopes = { SheetsService.Scope.Spreadsheets };
        static readonly string ApplicationName = "SSR Core";
        static readonly string sheet = "Sheet1";
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
            //String jsonString = "[";
            int count = 0;
            IList<object> rowTitle = null;
            string ticketId = "";
            List<JsonRequestModel> listJson = new List<JsonRequestModel>();
            if (values != null && values.Count > 0)
            {
                foreach (var row in values)
                {
                    JsonRequestModel jsonRequest = new JsonRequestModel();
                    if (count == 0)
                    {
                        rowTitle = row;
                        count++;
                    }
                    else
                    {
                        string jsonString = "{";
                        for (int i = 0; i < row.Count; i++)
                        {
                            if (i == 0 || i == 1)
                            {
                                if (rowTitle[i].Equals("Ticket ID"))
                                {
                                    ticketId = row[i] + "";
                                    jsonRequest.TicketId = row[i] + "";
                                }
                            }
                            else
                            {
                                jsonString += "\"" + rowTitle[i] + "\":" + "\"" + row[i] + "\",";
                            }
                        }
                        int lastIndex = jsonString.LastIndexOf(",");
                        jsonString = jsonString.Substring(0, lastIndex);
                        jsonString += "}";
                        jsonRequest.JsonInformation = jsonString;
                        listJson.Add(jsonRequest);
                    }
                    // Print columns A to F, which correspond to indices 0 and 4.
                }
                //int last = jsonString.LastIndexOf(",");
                //jsonString = jsonString.Substring(0, last);
                //jsonString += "]";
                return listJson;
            }

            return null;
        }
    }
}
