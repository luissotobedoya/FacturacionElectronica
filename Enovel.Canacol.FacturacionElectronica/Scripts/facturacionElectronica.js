function alertSuccess(title, message, redirectPage) {
    swal({
        title: title,
        text: message,
        type: "success"
    }, function () {
        window.location = redirectPage;
    });
}

function alertError(title, message) {
    swal({
        title: title,
        text: message,
        type: "error"
    });
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

