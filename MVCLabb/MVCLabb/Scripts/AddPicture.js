/// <reference path="sweetalert.min.js" />


var form = $('#createform');

form.submit(function (e) {

    e.preventDefault();
    if (form.valid()) {
        swal({
            title: "Image upload",
            text: "Submit to upload images",
            type: "info",
            showCancelButton: true,
            closeOnConfirm: false,
            showLoaderOnConfirm: true,
        },
            function () {
                $.ajax({
                    method: "POST",
                    url: "/Picture/Create",

                    data: new FormData(form[0]),
                    success: function (data) {
                        if (data.match("^Added")) {
                            uploadMoreImagesPrompt();
                        }
                        else {
                            swal("Oh no!", data, "error")
                        }
                    },
                    error: function (e) {
                        swal("Oh no!", "Something went wrong at the server!", "error")
                    },
                    processData: false,
                    contentType: false
                });
            });

    }
});

var uploadMoreImagesPrompt = function() {

    swal({
        title: "Success!?",
        text: "Upload more pictures?",
        type: "success",
        showCancelButton: true,
        confirmButtonColor: "#779b79",
        confirmButtonText: "Yes",
        cancelButtonText: "No",
        closeOnConfirm: true,
        closeOnCancel: true
    },
function (isConfirm) {
    if (isConfirm) {
        form[0].reset();
    } else {
        window.location = $('#backbtn').attr('href');
    }
});




};