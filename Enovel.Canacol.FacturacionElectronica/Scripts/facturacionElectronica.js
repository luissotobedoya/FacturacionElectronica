

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
    $('.datepicker').datepicker({
        dateFormat: "yy-mm-dd",
        timeFormat: "hh:mm:ss",
        changeMonth: true,
        language: 'es',
        changeYear: true,
    }); //Initialise any date pickers
});

$(function () {
    $(".form_datetime").datetimepicker({
        format: 'DD-MM-YYYY HH:mm:ss'
    });
});