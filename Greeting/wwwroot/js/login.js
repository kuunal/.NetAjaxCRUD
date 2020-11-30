var token = localStorage.getItem("token");
function checkLoggedIn(){
    if(token){
        window.location="/html/index.html";
    }
}
$(document).ready(function(){
    checkLoggedIn();
   $(document).on("click", "#sign-in",function(e){
    e.preventDefault();
    let email = $('#email-id').val();
    let password = $('#pass').val();
    $.ajax({
        url:"/api/Login",
        method:"POST",
        data:{
            Email:email,
            Password: password
        },
        success: function(data){
            console.log(data);
            if (data.success == true){
                console.log(data);
                localStorage.setItem("token",data.message);
                window.location="./index.html";  
            }},
        error:function(err){
                alert("Incorrect Id or Password!", err);
            }
        });
   })
});
