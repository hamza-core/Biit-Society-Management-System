using System;
using System.Collections.Generic;
using System.Data;
using project.Models;

namespace project.DataHandlers
{
    public class BudgetRequestViewModel
    {
        public bool AddBudgetRequest(BudgetRequest request)
        {
            string query = @"INSERT INTO BudgetRequests 
                            (EventID, RequestedAmount, ApprovedAmount, Status, DecisionBy, Comments, IsDeleted) 
                            VALUES 
                            (@EventID, @RequestedAmount, @ApprovedAmount, @Status, @DecisionBy, @Comments, 0)";

            var parameters = new Dictionary<string, object>
            {
                { "@EventID", request.EventID },
                { "@RequestedAmount", request.RequestedAmount },
                { "@ApprovedAmount", request.ApprovedAmount },
                { "@Status", request.Status },
                { "@DecisionBy", request.DecisionBy },
                { "@Comments", request.Comments }
            };

            return DatabaseHelper.ExecuteInsert(query, parameters) > 0;
        }

        public List<BudgetRequest> GetAllRequests()
        {
            string query = "SELECT * FROM BudgetRequests WHERE IsDeleted = 0";
            List<DataRow> dt = DatabaseHelper.ExecuteSelect(query, null);

            List<BudgetRequest> requests = new List<BudgetRequest>();

            foreach (DataRow row in dt)
            {
                requests.Add(new BudgetRequest
                {
                    BudgetRequestID = Convert.ToInt32(row["BudgetRequestID"]),
                    EventID = Convert.ToInt32(row["EventID"]),
                    RequestedAmount = Convert.ToDecimal(row["RequestedAmount"]),
                    ApprovedAmount = Convert.ToDecimal(row["ApprovedAmount"]),
                    Status = row["Status"].ToString(),
                    DecisionBy = Convert.ToInt32(row["DecisionBy"]),
                    Comments = row["Comments"].ToString(),
                    IsDeleted = Convert.ToBoolean(row["IsDeleted"])
                });
            }

            return requests;
        }
        public List<BudgetRequest> GetPendingRequests()
        {
            string query = "SELECT * FROM BudgetRequests WHERE status= Pending AND  IsDeleted = 0";
            List<DataRow> dt = DatabaseHelper.ExecuteSelect(query, null);

            List<BudgetRequest> requests = new List<BudgetRequest>();

            foreach (DataRow row in dt)
            {
                requests.Add(new BudgetRequest
                {
                    BudgetRequestID = Convert.ToInt32(row["BudgetRequestID"]),
                    EventID = Convert.ToInt32(row["EventID"]),
                    RequestedAmount = Convert.ToDecimal(row["RequestedAmount"]),
                    ApprovedAmount = Convert.ToDecimal(row["ApprovedAmount"]),
                    Status = row["Status"].ToString(),
                    DecisionBy = Convert.ToInt32(row["DecisionBy"]),
                    Comments = row["Comments"].ToString(),
                    IsDeleted = Convert.ToBoolean(row["IsDeleted"])
                });
            }

            return requests;
        }

        public bool UpdateStatus(int requestId, string status, decimal approvedAmount, int decisionBy, string comments)
        {
            string query = @"UPDATE BudgetRequests 
                             SET Status = @Status, ApprovedAmount = @ApprovedAmount, 
                                 DecisionBy = @DecisionBy, Comments = @Comments 
                             WHERE BudgetRequestID = @BudgetRequestID";

            var parameters = new Dictionary<string, object>
            {
                { "@BudgetRequestID", requestId },
                { "@Status", status },
                { "@ApprovedAmount", approvedAmount },
                { "@DecisionBy", decisionBy },
                { "@Comments", comments }
            };

            return DatabaseHelper.ExecuteUpdate(query, parameters) > 0;
        }

        public bool DeleteRequest(int requestId)
        {
            string query = "UPDATE BudgetRequests SET IsDeleted = 1 WHERE BudgetRequestID = @BudgetRequestID";
            var parameters = new Dictionary<string, object>
            {
                { "@BudgetRequestID", requestId }
            };

            return DatabaseHelper.ExecuteDelete(query, parameters) > 0;
        }

        public List<BudgetRequest> GetByEvent(int eventId)
        {
            string query = "SELECT * FROM BudgetRequests WHERE EventID = @EventID AND IsDeleted = 0";
            var parameters = new Dictionary<string, object> { { "@EventID", eventId } };

            List<DataRow> dt = DatabaseHelper.ExecuteSelect(query, parameters);
            List<BudgetRequest> requests = new List<BudgetRequest>();

            foreach (DataRow row in dt)
            {
                requests.Add(new BudgetRequest
                {
                    BudgetRequestID = Convert.ToInt32(row["BudgetRequestID"]),
                    EventID = Convert.ToInt32(row["EventID"]),
                    RequestedAmount = Convert.ToDecimal(row["RequestedAmount"]),
                    ApprovedAmount = Convert.ToDecimal(row["ApprovedAmount"]),
                    Status = row["Status"].ToString(),
                    DecisionBy = Convert.ToInt32(row["DecisionBy"]),
                    Comments = row["Comments"].ToString(),
                    IsDeleted = Convert.ToBoolean(row["IsDeleted"])
                });
            }

            return requests;
        }
    }
}
