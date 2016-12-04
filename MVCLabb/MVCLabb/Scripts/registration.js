/// <reference path="sweetalert.min.js" />
$('document').ready(function (e) {
    regform.validate();
});

var regform = $('#registrationform');

regform.on('submit', function (e) {
    e.preventDefault();
    if (regform.valid) {
        $.ajax({
            url: "Registration",
            type: "POST",
            data: regform.serialize(),
            success: function (data) {
                var msg = data.split(" ");
                if (msg[0] != "Invalid") {
                    swal("Success!", "Registration was succesful!", "success");
                    setTimeout(function (e) {
                        window.location = data;
                    });
                }
                else {
                    swal("Error", data, "error");   
                }
                
            },
            error: function (e) {
                swal("Error", "Something went wrong at the server", "error");
            }
        });

    }
    else {
        swal("Invalid!", "Form not filled in correctly!", "warning");
    }
});