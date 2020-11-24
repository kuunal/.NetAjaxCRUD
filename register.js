function authenticate(e){
    e.preventDefault();
    console.log("Incisee")
    let name = $('#username').val()
    let email = $('#email-id').val()
    let password = $('#pass') .val()
    console.log(name, email, password)
    if (!password.match("^(?=.*[0-9])(?=.*[A-Z])(?=[a-zA-Z0-9]*[^a-zA-Z0-9][a-zA-Z0-9]*$).{8,}"))
        alert("Invalid format")
    if(password !=$('#confirm-pass').val())
        alert("passwords don't match");
    $.ajax({
        url:"http://localhost:3000/employee",
        method:"POST",
        data:{
            name:name,
            email:email,
            password:password
        },success: function(){
            console.log("Registered succrssfully")
        },error: function(err){
            console.log(err);
        }
    });
}

$(document).ready(function(){
    $(document).on("click","#submit",authenticate)
});