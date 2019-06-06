using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;

namespace HSBC.CME.Plugins
{
    public class AccountPostUpdate : IPlugin
    {
        public void Execute(IServiceProvider serviceProvider)
        {
            // Obtain the tracing service
            ITracingService tracingService = (ITracingService)serviceProvider.GetService(typeof(ITracingService));

            // Obtain the execution context from the service provider.  
            IPluginExecutionContext context = (IPluginExecutionContext)serviceProvider.GetService(typeof(IPluginExecutionContext));

            // The InputParameters collection contains all the data passed in the message request.  
            if (context.InputParameters.Contains("Target") && context.InputParameters["Target"] is Entity)
            {
                // Obtain the target entity from the input parameters.  
                Entity accountRecord = (Entity)context.InputParameters["Target"];
                Entity preAccountRecord = (Entity)context.PreEntityImages["PreImage"];

                // Obtain the organization service reference which you will need for  
                // web service calls.  
                IOrganizationServiceFactory serviceFactory =
                    (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
                IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);

                try
                {

                    tracingService.Trace("AccountPreCreate Plugin is starting....");
                    //Create KYC for Account

                    string line1 = string.Empty;
                    string line2 = string.Empty;
                    string city = string.Empty;

                    if (accountRecord.Attributes.Contains("address1_line1"))
                    {
                        line1 = accountRecord.Attributes["address1_line1"].ToString();
                    }
                    else
                    {
                        line1 = preAccountRecord.Attributes["address1_line1"].ToString();
                    }

                    if (accountRecord.Attributes.Contains("address1_line2"))
                    {
                        line2 = accountRecord.Attributes["address1_line2"].ToString();
                    }
                    else
                    {
                        line2 = preAccountRecord.Attributes["address1_line2"].ToString();
                    }

                    if (accountRecord.Attributes.Contains("address1_city"))
                    {
                        city = accountRecord.Attributes["address1_city"].ToString();
                    }
                    else
                    {
                        city = preAccountRecord.Attributes["address1_city"].ToString();
                    }



                    QueryExpression query = new QueryExpression("contact");
                    query.ColumnSet.AddColumn("firstname");
                    query.Criteria.AddCondition("parentcustomerid", ConditionOperator.Equal, accountRecord.Id);

                    EntityCollection collection = service.RetrieveMultiple(query);

                    if (collection.Entities.Count > 1)
                    {
                        foreach (Entity contactRecord in collection.Entities)
                        {
                            contactRecord.Attributes.Add("address1_line1", line1);
                            contactRecord.Attributes.Add("address1_line2", line2);
                            contactRecord.Attributes.Add("address1_city", city);
                            service.Update(contactRecord);
                        }
                    }
                }

                catch (FaultException<OrganizationServiceFault> ex)
                {
                    throw new InvalidPluginExecutionException("An error occurred in FollowUpPlugin.", ex);
                }

                catch (Exception ex)
                {
                    tracingService.Trace("FollowUpPlugin: {0}", ex.ToString());
                    throw;
                }
            }
        }
    }
}
