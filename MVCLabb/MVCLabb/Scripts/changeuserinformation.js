
var newPW = $("#newPW");
var newPW2 = $("#newPW2");
var message = $("#message");
var compareresult = $("#compareresult");
var emailFormControl = $('#Email');
var userinfoForm = $('#userinfoform');
var oldemail = "";
$('document').ready(function (e) {

    oldemail = userinfoForm.find('#Email').val();
});

userinfoForm.submit(function (e) {
    e.preventDefault();
    userinfoForm.validate();
    $.post("/Account/MyPage/Index", userinfoForm.serialize(), function (data) {
        if (data === "The email is already registered.") {
            emailFormControl.val(oldemail);
            swal("Oh no!", data, "error");

        }
        else{
            oldemail = emailFormControl.val();
            swal("Yay!", data, "success");
            setTimeout(function () { location.reload() }, 1000);
        }
        if (data === "Couldn't update information") {
            swal("Oh no!", data, "error");
        }
    });


});




$("#btn_changepw").click(function (e) {
    e.preventDefault();
    $("ChangePassword").validate

        {
    $.post("/Account/MyPage/ChangePassword",
    $("#ChangePassword").serialize(),
    function (data) {
        message.html("<p>" + data + "</p>");
        if (data === "Update successful") {
            $("#password").val('');
            newPW.val('');
            newPW2.val('');
            swal("Success", data, "success");

        }
        else {
            swal("Error", data, "error");
        }
    });
    }


});

    newPW2.keyup(function (e) {
        if (ComparePassword(newPW, newPW2)) {
            compareresult.html('');

    }
    else {
        compareresult.text("Passwords must match");

    }

});


ComparePassword = function (pw, pw2) {
    if (pw.val() === pw2.val()) {
        return true;
    }
    else {
        return false;
    }

};