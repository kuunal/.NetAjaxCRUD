$(document).ready(function(){

    $(document).on("click","#forgot-btn",function(e){
    console.log("saddddddddddddddddddddddddddddddddddd")
    e.preventDefault();
        let email = $('#email-id').val();
        $.ajax({
            url:"http://localhost:5000/forgot",
            method:"POST",
            data:{
                Email : email
            }, success: function(){
                alert("Please check mail");
            },error: function(){
                alert("Wrong emal id")
            }
        });
    })
});