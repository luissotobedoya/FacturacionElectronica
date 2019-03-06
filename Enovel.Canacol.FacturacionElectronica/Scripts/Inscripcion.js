$(document).ready(function () {
    $(".data-general").hide();
    hoverPassword();
    hoverConfirmPassword();
});

function hideDataGeneral() {
    $(".data-general").hide();
}

function showDataGeneral() {
    $(".data-general").show();
}

function hoverPassword() {
    $("#showPassword").hover(
        function functionName() {
            $("#Password").attr("type", "text");
        },
        function () {
            $("#Password").attr("type", "password");
        }
    );
}

function hoverConfirmPassword() {
    $("#showConfirmPassword").hover(
        function functionName() {
            $("#ConfirmarPassword").attr("type", "text");
        },
        function () {
            $("#ConfirmarPassword").attr("type", "password");
        }
    );
}


