<<<<<<< HEAD
﻿

function alertInscripcionSucces(title, message) {
=======
﻿function alertSuccess(title, message, redirectPage) {
>>>>>>> master
    swal({
        title: title,
        text: message,
        type: "success"
    }, function () {
<<<<<<< HEAD
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
=======
        window.location = redirectPage;
    });
}

function alertError(title, message) {
    swal(title, message, "error");
}


function alertInfo(title, message, redirectPage) {
    swal({
        title: title,
        text: message,
        type: "info"
    }, function () {
        window.location = redirectPage;
    });
}

>>>>>>> master
