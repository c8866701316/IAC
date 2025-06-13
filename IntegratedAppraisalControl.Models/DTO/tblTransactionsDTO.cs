using System;
using System.Collections.Generic;
using System.Text;

namespace IntegratedAppraisalControl.Models.DTO
{
    public class tblTransactionsDTO
    {
        public string TableName { get; set; }
        public string TransactionType { get; set; }
        public string EntryNumber { get; set; }
        public string EntryDescription { get; set; }
        public string FieldName { get; set; }
        public string EntryOldValue { get; set; }
        public string EntryNewValue { get; set; }
        public string EntryChangeDate { get; set; }
        public string InventoryID { get; set; }
        public string Flr { get; set; }
        public string Building { get; set; }
        public string Department { get; set; }
        public string Room { get; set; }
        public string UpdateDate { get; set; }
    }
}
