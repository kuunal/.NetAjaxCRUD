$(document).ready(function(){
    $(document).on('click','#reset-btn', function(e){
        e.preventDefault();
        let password = $('#password').val();
        let url = window.location.href;
        let token = url.split("?")[1];
        $.ajax({
            url:"http://localhost:5000/Reset",
            method:"POST",
            data:{
                password,
                token
            }, success: function(){
                alert("Password changed successfully");
            }, error: function(){
                alert("Session expired!");
            }
        });
    });
});