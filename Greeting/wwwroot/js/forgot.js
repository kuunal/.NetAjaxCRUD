function displayError(error) {
    $('.error').addClass("error-active");
    setTimeout(function () {
        $('.error').text(error);
    }, 250);
    setTimeout(function () {
        $('.error').removeClass("error-active").text('');
    }, 5000);
}

$(document).ready(function () {
    $(document).on("click","#forgot-btn",function(e){
        e.preventDefault
        let email = $('#email-id').val();
        if (!email.match("^[a-zA-Z0-9]+[\\.\\-\\+\\_]?[a-zA-Z0-9]+@[a-zA-Z0-9]+[.]?[a-zA-Z]{2,4}[\\.]?([a-z]{2,4})?$")) {
            displayError("INVALID EMAIL FORMAT");
            return
        }
        $.ajax({
            url:"/forgot",
            method:"POST",
            data:{
                Email : email
            }, success: function(){
                displayError('Mail has been sent');
            }, error: function (err) {
                if (err.status == 400) {
                    displayError('Invalid Id or password');
                }
                else if (err.status >= 500)
                    $('.error').text('Server is down! ');
            }
        });
    })
});