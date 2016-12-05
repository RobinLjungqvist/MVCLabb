$('document').ready(function (e) {
    $('.backbtn').on("click", function (e) {
        e.preventDefault();
        history.go(-1);
    });
});
       
