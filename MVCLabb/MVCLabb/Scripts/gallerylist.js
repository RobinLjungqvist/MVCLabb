/// <reference path="sweetalert.min.js" />
var galleryContainer = $("#gallerylistcontainer");

$('document').ready(function (e) { 

    addEventListeners();

    setInterval(reloadGalleries, 5000);

   


});

var addEventListeners = function(){
    deleteEvent();
    createEvent();
};

var reloadGalleries = function() {

    $.ajax({
        type: "GET",
        url: "../../Gallery/GalleryList",
        success: function (data) {
            $('#gallerylistcontainer').html(data);
        }
    });

};

var deleteEvent = function() {
    galleryContainer.on('click', '.gallerydeletebtn', function (e) {
        e.preventDefault();
        var model = $(this).attr('galleryID');
        //Sweet alert plugin <----
        swal({
            title: "Are you sure you want to delete this gallery?",
            text: "You will not be able to recover this gallery or the pictures in it!",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Yes, delete it!",
            closeOnConfirm: false
        },
            function () {
                $.post("../../gallery/delete",
                 { galleryID: model },
                 function (data) {
                     swal("Deleted!", data, "success");
                     reloadGalleries();
                 });

            });



    });
};

var createEvent = function () {

    $('#creategallerybtn').click(function (e) {
        e.preventDefault();

        var galleryModel = {
            id: "",
            GalleryName: "",
            DateCreated: "",
            UserID: ""
        };



        swal({
            title: "Create!",
            text: "Input Gallery Name",
            type: "input",
            showCancelButton: true,
            closeOnConfirm: false,
            animation: "slide-from-top",
            inputPlaceholder: "My Gallery Name..."
        },
function (inputValue) {
    if (inputValue === false) return false;

    if (inputValue === "" || inputValue.length < 5) {
        swal.showInputError("You need to write atleast 5 characters!");

        return false
    }
    galleryModel.GalleryName = inputValue;
    $.post("../../gallery/create",
                 galleryModel,
                 function (data) {
                     reloadGalleries();
                 });


    swal("Nice!", "You created: " + inputValue, "success");
});


    });

};