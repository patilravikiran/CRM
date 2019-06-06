// JavaScript source code

function ContactLoad(executionContext) {
    //get Form Context
    var formContext = executionContext.getFormContext();

    //get Attribute Data
    var firstName = formContext.getAttribute("firstname").getValue();


    //set attrbute
    formContext.getAttribute("description").setValue("The FirstName is " + firstName);
}

function OnEmailChange(executionContext) {
    //get formContext
    var formContext = executionContext.getFormContext();

    //validate email
    var emailAddress = formContext.getAttribute("emailaddress1").getValue();

    if (emailAddress == '') {
        formContext.getControl('emailaddress1').setNotification('You have changed email', 'email_id');
    }
    else {
        formContext.getControl('emailaddress1').clearNotification('email_id');
    }
    
}


function ONSSNChange(executionContext) {
    //get formContext
    var formContext = executionContext.getFormContext();

    //validate email
    var SSN = formContext.getAttribute("pru_socialsecuritynumber").getValue();

    if (isNaN(SSN) || SSN.length > 5) {
        formContext.getControl('pru_socialsecuritynumber').setNotification('Please enter valid SSN Number less than 11 digits', 'ssn_id');
        formContext.ui.setFormNotification('Incorrect Data', 'WARNING', 'formmsg');
    }
    else {
        formContext.getControl('pru_socialsecuritynumber').clearNotification('ssn_id');
        formContext.ui.clearFormNotification('formmsg');
    }

}
    

