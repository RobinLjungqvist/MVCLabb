/// <reference path="sweetalert.min.js" />


$('document').ready(function (e) {
    addEventListeners();
    
    setInterval(reloadComments, 5000);
});
var commentsContainer = $('#commentcontainer');

var reloadComments = function(e){
    
    //commentsContainer.on('load',);
    $.ajax({
        type: "GET",
        url: "../../Comment/Comments?pictureID=" + $('#pictureID').val(), 
        success: function (data) {
            $('#commentcontainer').html(data);
        }
    });
      


};



var addEventListeners = function(e){
        
    commentsContainer.on('click','.deletecommentbtn', function (e) {
            e.preventDefault();
            var model = $(this).attr('id');
        //Sweet alert plugin <----
            swal({
                title: "Are you sure you want to delete this comment?",
                text: "You will not be able to recover this comment!",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes, delete it!",
                closeOnConfirm: false
            },
     function () {
         $.post("../../comment/delete",
                { commentID: model },
                function (data) {
                    reloadComments();
                });
         swal("Deleted!", "Your comment has been deleted.", "success");
     });

            //Original sättet.
            //if (window.confirm("Are you sure you want to delete this comment?")) {
            //    $.post("../../comment/delete",
            //    { commentID: model },
            //    function (data) {
            //        reloadComments();
            //    });


            //};
        });

        
};

var form = $('#newcommentform');

form.submit(function (e) {
    e.preventDefault();
});


$('#commentsubmitbtn').click(function (e) {
    e.preventDefault();

    if (form.valid()) {

        $.post("../../Comment/NewComment",
           form.serialize(), function (data) {
               swal("Comment posted!");
               reloadComments();

           });
        $('#Comment').val('');
        $('#Title').val('');
        

    }

});