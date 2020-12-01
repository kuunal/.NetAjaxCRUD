var token = localStorage.getItem("token");
function checkLoggedIn(){
    if(token){
        window.location="/html/index.html";
    }
}

function displayError(error) {
    $('.error').addClass("error-active");
    setTimeout(function () {
        $('.error').text(error);
    }, 250);
    setTimeout(function () {
        $('.error').removeClass("error-active").text('');
    }, 5000);
}

function authenticate(e){
    e.preventDefault();
    console.log("Incisee")
    let name = $('#username').val()
    let email = $('#email-id').val()
    let password = $('#pass') .val()
    let address = $('#address') .val()
    let phoneNumber = $('#pno').val()
    if (!email.match("^[a-zA-Z0-9]+[\\.\\-\\+\\_]?[a-zA-Z0-9]+@[a-zA-Z0-9]+[.]?[a-zA-Z]{2,4}[\\.]?([a-z]{2,4})?$")) {
        displayError("INVALID EMAIL FORMAT");
        return
    }
    if (!password.match("^(?=.*[0-9])(?=.*[A-Z])(?=[a-zA-Z0-9]*[^a-zA-Z0-9][a-zA-Z0-9]*$).{8,}")){
        displayError("PASSWORD SHOULD CONTAIN ATLEAST ONE LOWER, UPPER, SPECIAL AND DIGIT!")
        return
    }
    if(password !=$('#confirm-pass').val()){
        displayError("PASSWORD'S DON'T MATCH");
        return
    }
    if (phoneNumber.length != 10) {
        displayError("INVALID PHONE NUMBER");
        return
    }
    let employee = {
        Name:name,
        Email:email,
        Password:password,
        Address:address,
        PhoneNumber:phoneNumber
    }
    $.ajax({
        method:"POST",
        url:"/Employee",
        data:employee,
        success: function(){
            window.location="/html/login.html";
        },error: function(err){
            console.log(err);
        }
    });
}

$(document).ready(function(){
    checkLoggedIn();
    $(document).on("click","#submit",authenticate)
});