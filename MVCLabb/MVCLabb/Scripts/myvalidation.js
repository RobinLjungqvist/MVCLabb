
var newPW = $("#newPW");
var newPW2 = $("#newPW2");
var message = $("#message");
var compareresult = $("#compareresult");






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