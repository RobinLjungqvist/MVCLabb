/// <reference path="sweetalert.min.js" />

var loginform = $('#loginform');

loginform.on('submit', function (e) {
    e.preventDefault();
    if (loginform.valid) {
        $.ajax({
            url: "Login",
            type: "post",
            data: loginform.serialize(),
            success: function (data) {
                var words = data.split(' ');
                if (words[0] == "Invalid") {
                    swal("Invalid!", "Invalid Email or Password", "error");
                    loginform[0].reset();
                }
                else {
                    swal("Success!", "Login was succesful!", "success");
                    setTimeout(function () {
                        window.location = data;

                    }, 1500);
                }
            },
            fail: function (e) {
                swal("Error", "Something went wrong at the server", "error");
            }
        });

    }
    else{
        swal("Error", "Form not filled in correctly", "error");
    }


});