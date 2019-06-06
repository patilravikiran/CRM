// JavaScript source code
var sdk = window.sdk || {};
(function () {
    this.onCompanyLoad = function (executionContext) {
        Xrm.WebApi.retrieveMultipleRecords("pru_country", "?$select=pru_name").then(
            function success(result) {
                var data = '';
                for (var i = 0; i < result.entities.length; i++) {
                    data = data + ' ' + result.entities[i].pru_name;
                    var formContext = executionContext.getFormContext();
                    formContext.getAttribute("description").setValue(data);
                }
                // perform additional operations on retrieved records
            },
            function (error) {
                console.log(error.message);
                // handle error conditions
            }
        );
    }   
}
).call(sdk);
