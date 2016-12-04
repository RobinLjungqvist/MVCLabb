/// <reference path="sweetalert.min.js" />

var deleteform = $('#deletepictureform');

deleteform.on('submit', function (e) {
    e.preventDefault();
    swal({
        title: "Delete Image!?",
        text: "Submit to delete image.",
        type: "warning",
        showCancelButton: true,
        closeOnConfirm: false,
        showLoaderOnConfirm: true,
    },
        function () {
            $.ajax({
                url: "/Picture/Delete",
                type: "post",
                data: deleteform.serialize(),
                success: function (data) {
                    swal("Success", "The Image has been deleted.", "success");
                    window.location.href = data;

                },
                error:function (e) {
                    swal("Error", "Couldn't Delete the Image", "error");
                }
            });

            //$.post("/Picture/Delete", deleteform.serialize(), function (data) {
                

            //});
                
        });
    });