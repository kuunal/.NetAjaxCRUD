$(document).ready(function(){
    $(document).on('click','#reset-btn', function(e){
        e.preventDefault();
        let password = $('#password').val();
        let url = window.location.href;
        let token = url.split("?")[1];
        $.ajax({
            url:"/Reset",
            method:"POST",
            data:{
                password,
                token
            }, success: function(){
                alert("Password changed successfully");
                window.location = "./login.html";
            }, error: function(){
                alert("Session expired!");
            }
        });
    });
});