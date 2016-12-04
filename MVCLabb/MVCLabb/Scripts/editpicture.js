/// <reference path="sweetalert.min.js" />
$('document').ready(function (e) {
    editform.validate();
});


var editform = $('#editform');

editform.on('submit', function (e) {
    e.preventDefault();
    if (editform.valid()) {

    swal({
        title: "Update Image!?",
        text: "Submit to update image.",
        type: "info",
        showCancelButton: true,
        closeOnConfirm: false,
        showLoaderOnConfirm: true,
    },
    function (e) {
        var formdata = new FormData(editform[0]);
        $.ajax({
            url: editform.attr('action'),
            method: "post",
            data: formdata,
            processData: false,
            contentType: false,
            success: function (data) {
                swal("Success!", "Image details has been updated!", "success");
                window.location = $('#BackToDetails').attr('href');
            },
            error: function (data) {
                swal("Error!", "Couldn't update image details", "error");
            }
        });

    });

    }
    else {
        swal("Warning!", "Form is not filled in correctly!", "warning");

    }
});