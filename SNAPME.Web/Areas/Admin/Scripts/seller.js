$(function () {
    $('#mnuSellers').addClass('active');
    $('#menuSeller > li:first-child() > a').trigger('click');

    $('#mdlProduct').on('hidden.bs.modal', function(){ $(this).removeData('bs.modal').find('.modal-content').html(''); });
});

$.validator.setDefaults({
    highlight: function (element) {
        $(element).closest('.form-group').addClass('has-error');
    },
    unhighlight: function (element) {
        $(element).closest('.form-group').removeClass('has-error');
    }
});

window.seller = { xhrCounter: 0 };

function getContent(content, url, midx) {
    $(content).html("").append($('<div class="text-center"><i class="fa fa-spin fa-spinner"></i></div>'));
    var id = ++window.seller.xhrCounter;
    $('#menuSeller > li').toggleClass('active', false);
    $('#menuSeller > li:nth-child(' + midx + ')').toggleClass('active', true);
    $.get(url, function (response) {
        if (id == window.seller.xhrCounter) {
            $(content).html(response);
            if ($(content + ' form').length > 0) {
                var form = $(content + ' form').removeData("validator").removeData("unobtrusiveValidation");
                $.validator.unobtrusive.parse(form);
            }
            attachEvents();
        }
    });
}

function saveDetails() {
    if ($('#frmSellerDetails').valid()) {
        var data = $('#frmSellerDetails').serializeArray();
        $.post('/Admin/Seller', data, function (response) { });
    } else {
        $('#frmSellerDetails .has-error .form-control').focus();
    }
}

function attachEvents() {
    $('a[data-toggle=modal][data-entity=product]').on('click', function () {
        $('#mdlProduct').data('remote', '/Admin/Seller/Product/' + $(this).data('id'));
    });
}