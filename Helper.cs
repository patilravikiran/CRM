using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Workflow;
using System.ServiceModel;
using System.Activities;

namespace HSBC.CME.CustomWorkflows
{
    public class Helper
    {
        public static string GetConfig(string name, IOrganizationService service)
        {
            QueryByAttribute query = new QueryByAttribute("pru_confi");
            query.AddAttributeValue("pru_name", name);
            query.ColumnSet = new ColumnSet(new string[] { "pru_value" });
            EntityCollection collection = service.RetrieveMultiple(query);

            return collection.Entities.FirstOrDefault().Attributes["pru_value"].ToString();
        }
    }
}
