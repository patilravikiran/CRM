using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FakeItEasy;
using FakeXrmEasy;
using Microsoft.Xrm.Sdk;
using HSBC.CME.Plugins;
using Microsoft.Xrm.Sdk.Query;

namespace HSBC.CME.UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void AccountPostCreateTest()
        {
            var fakedContext = new XrmFakedContext();

            Entity account = new Entity("account");
            account.Attributes.Add("name", "Unit Test");
            account.Id = Guid.NewGuid();

            var fakedService = fakedContext.GetOrganizationService();

            fakedContext.ExecutePluginWithTarget<AccountPostCreate>(account, "Create", 40);


            QueryExpression query = new QueryExpression("task");
            query.ColumnSet.AddColumn("subject");

            EntityCollection colletion = fakedService.RetrieveMultiple(query);

            Assert.IsTrue(colletion.Entities.Count == 1);

        }
    }
}
