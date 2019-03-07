$(document).ready(function () {
    hoverCurrentPassword();
    hoverNewPassword();
    hoverConfirmPassword();
});

function hoverCurrentPassword() {
    $("#showCurrentPassword").hover(
        function functionName() {
            $("#PasswordActual").attr("type", "text");
        },
        function () {
            $("#PasswordActual").attr("type", "password");
        }
    );
}

function hoverNewPassword() {
    $("#showNewPassword").hover(
        function functionName() {
            $("#NuevaPassword").attr("type", "text");
        },
        function () {
            $("#NuevaPassword").attr("type", "password");
        }
    );
}

function hoverConfirmPassword() {
    $("#showConfirmPassword").hover(
        function functionName() {
            $("#ConfirmarNuevaPassword").attr("type", "text");
        },
        function () {
            $("#ConfirmarNuevaPassword").attr("type", "password");
        }
    );
}