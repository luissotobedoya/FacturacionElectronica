

function alertInscripcionSucces(title, message) {
    swal({
        title: title,
        text: message,
        type: "success"
    }, function () {
        window.location = "/";
    });
}

function alertError(title, mesagge) {
    swal(title, mesagge, "error");
}


function alertinfo(title, mesagge) {
    swal(title, mesagge, "info");
}

$(function () { // will trigger when the document is ready
    $('.datepicker').datepicker(
        {
            changeMonth: true,
            changeYear: true,
        }); //Initialise any date pickers
});