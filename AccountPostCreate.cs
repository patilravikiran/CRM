using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;

namespace HSBC.CME.Plugins
{
    public class AccountPostCreate : IPlugin
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

                // Obtain the organization service reference which you will need for  
                // web service calls.  
                IOrganizationServiceFactory serviceFactory =
                    (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
                IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);

                try
                {

                    tracingService.Trace("AccountPreCreate Plugin is starting....");
                    //Create KYC for Account
                    //Create task record
                    Entity taskRecord = new Entity("task");
                    taskRecord.Attributes.Add("subject", "Start KYC for customer");
                    taskRecord.Attributes.Add("scheduledend", DateTime.Now.AddDays(2));
                    taskRecord.Attributes.Add("description", "Start KYC for customer");
                    taskRecord.Attributes.Add("prioritycode", new OptionSetValue(2));
                    taskRecord.Attributes.Add("regardingobjectid", accountRecord.ToEntityReference());

                    Guid taskRecordId = service.Create(taskRecord);

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
