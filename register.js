function authenticate(e){
    e.preventDefault();
    console.log("Incisee")
    let name = $('#username').val()
    let email = $('#email-id').val()
    let password = $('#pass') .val()
    let address = $('#address') .val()
    let phoneNumber = $('#pno') .val()
    console.log(name, email, password, (phoneNumber), address)
    if (!password.match("^(?=.*[0-9])(?=.*[A-Z])(?=[a-zA-Z0-9]*[^a-zA-Z0-9][a-zA-Z0-9]*$).{8,}")){
        alert("Invalid format")
        return
    }
    if(password !=$('#confirm-pass').val()){
        alert("passwords don't match");
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
        url:"http://localhost:5000/Employee",
        data:employee,
        success: function(){
            window.location="/login.html";
        },error: function(err){
            console.log(err);
        }
    });
}

$(document).ready(function(){
    $(document).on("click","#submit",authenticate)
});