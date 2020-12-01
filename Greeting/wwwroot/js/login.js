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

function displayError(error) {
    $('.error').addClass("error-active");
    setTimeout(function () {
        $('.error').text(error);
    }, 250);
    setTimeout(function () {
        $('.error').removeClass("error-active").text('');
    }, 5000);
}

$(document).ready(function(){
    checkLoggedIn();
    $(document).on("click", "#sign-in", function (e) {
        e.preventDefault();
        let email = $('#email-id').val();
        let password = $('#pass').val();
        if (!email.match("^[a-zA-Z0-9]+[\\.\\-\\+\\_]?[a-zA-Z0-9]+@[a-zA-Z0-9]+[.]?[a-zA-Z]{2,4}[\\.]?([a-z]{2,4})?$")) {
            displayError("INVALID EMAIL FORMAT");
            return
        }
        $.ajax({
            url: "/api/Login",
            method: "POST",
            data: {
                Email: email,
                Password: password
            },
            success: function (data) {
                console.log(data.statusCode);
                if (data.statusCode == 200) {
                    console.log(data);
                    localStorage.setItem("token", data.message);
                    window.location = "./index.html";
                }
            },
            error: function (err) {
                if (err.status == 400) {
                    displayError('Invalid Id or password');
                }
                else if (err.status >= 500)
                    $('.error').text('Server is down! ');
            }
        })
    })
});
