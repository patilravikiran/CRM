using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System.IO;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Query;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Enter Password here.....");
            //string password = Console.ReadLine();
            string connectString = "AuthType=Office365; Url=https://prudentia.crm8.dynamics.com; UserName=Ravikiran@Prudentia001.onmicrosoft.com; Password=Ravi@1234";
            CrmServiceClient service = new CrmServiceClient(connectString);


            Entity contact = new Entity("contact", new Guid("GUID"));

            //OrganizationRequest req = new OrganizationRequest("pru_LeadFollowupAction");
            //req.Parameters.Add("DueDate", DateTime.Now.AddDays(2));


            //Entity lead = new Entity("lead");
            //lead.Attributes.Add("subject", "New Enquiry");
            //lead.Attributes.Add("lastname", "Patil");
            //Guid guid = service.Create(lead);

            //req.Parameters.Add("Target", new EntityReference("lead", guid));

            //OrganizationResponse res = service.Execute(req);
            //Console.WriteLine(res.Results["Status"].ToString());
            
            //Using FetchXML

            //string query = @"<fetch version='1.0' output-format='xml-platform' mapping='logical' distinct='false'>
            //                  <entity name = 'contact'> 
            //                     <attribute name = 'fullname'/>  
            //                      <attribute name = 'telephone1'/>   
            //                       <attribute name = 'contactid'/>    
            //                        <order attribute = 'fullname' descending = 'false'/>       
            //                           <filter type = 'and' >        
            //                              <condition attribute = 'createdon' operator= 'today'/>           
            //                                 <condition attribute = 'statecode' operator= 'eq' value = '0'/>               
            //                                   </filter>               
            //                                 </entity> </fetch>";
            //EntityCollection col = service.RetrieveMultiple(new FetchExpression(query));

            //foreach (Entity contact in col.Entities)
            //{
            //    Console.WriteLine(contact.Attributes["fullname"].ToString());
            //}

            //Contact con = new Contact()
            //{
            //    FirstName = "John",
            //    LastName = "Smith",
            //    ParentCustomerId = new EntityReference()
            //};

            //pru_Confi conf= new pru_Confi();
            //conf.pru_Name = "Sales Tax";
            //conf.pru_Value = 20;


            //string[] lines = File.ReadAllLines(@"C:\Users\adm1\Desktop\Data.csv");

            //ExecuteMultipleRequest mulReq = new ExecuteMultipleRequest();
            //mulReq.Settings = new ExecuteMultipleSettings()
            //{
            //    ContinueOnError = true,
            //    ReturnResponses = false
            //}; 

            //CreateRequest req;
            //mulReq.Requests = new OrganizationRequestCollection();
            //foreach (string line in lines)
            //{
                

            //    string[] values = line.Split(',');
            //    Entity contact = new Entity("contact");
            //    contact.Attributes.Add("firstname", values[0]);
            //    contact.Attributes.Add("lastname", values[1]);

            //    req = new CreateRequest();
            //    req.Target = contact;
                
            //    mulReq.Requests.Add(req);

            //}

            //ExecuteMultipleResponse res = (ExecuteMultipleResponse)service.Execute(mulReq);


            Console.Read();
        }
    }
}
