using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;

namespace HSBC.CME.Plugins
{
    public class ContactUpdate : IPlugin
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
                Entity contactRecord = (Entity)context.InputParameters["Target"];
                Entity contactPreRecord = (Entity)context.PreEntityImages["PreImage"];

                // Obtain the organization service reference which you will need for  
                // web service calls.  
                IOrganizationServiceFactory serviceFactory =
                    (IOrganizationServiceFactory)serviceProvider.GetService(typeof(IOrganizationServiceFactory));
                IOrganizationService service = serviceFactory.CreateOrganizationService(context.UserId);

                try
                {

                    tracingService.Trace("AccountPreCreate Plugin is starting....");


                    if (contactRecord.Attributes.Contains("creditlimit"))
                    {
                        decimal oldCreditLimit = 1;
                        if (contactPreRecord.Attributes.Contains("creditlimit"))
                        {
                            oldCreditLimit = ((Money)contactPreRecord.Attributes["creditlimit"]).Value;
                        }

                        decimal newCreditLimit = ((Money)contactRecord.Attributes["creditlimit"]).Value;

                        decimal hike = ((newCreditLimit - oldCreditLimit) * 100) / oldCreditLimit;
                        contactRecord.Attributes.Add("description", "The hike percentage is ...." +hike);

                        if (hike >= 10)
                        {
                            contactRecord.Attributes.Add("address1_shippingmethodcode", new OptionSetValue(2));
                        }
                        else
                        {
                            contactRecord.Attributes.Add("address1_shippingmethodcode", new OptionSetValue(1));
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
