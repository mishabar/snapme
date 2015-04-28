$.ajaxSetup({ cache: false });

function showAlert(type, text, leaveModal) {
    var buttons = {};
    buttons[type] = { label: 'OK', className: 'btn-' + type };
    if (leaveModal || false) { buttons[type].callback = function () { setTimeout(function () { $('body').addClass('modal-open'); }, 800); }; }
    bootbox.dialog({
        message: text,
        title: "iiSnap",
        buttons: buttons
    });
}