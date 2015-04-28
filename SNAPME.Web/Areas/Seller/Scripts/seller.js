$(function () {
    $('#mnuSellers').addClass('active');
    $('#menuSeller > li:first-child() > a').trigger('click');

    $('#mdlProduct').on('hidden.bs.modal', function () {
        $(this).removeData('bs.modal').find('.modal-content').html('');
    });
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
                $.validator.unobtrusive.parse(form); form.validate().resetForm();
            }
            $(content).find('.modal').on('hidden.bs.modal', function () {
                $(this).removeData('bs.modal').find('.modal-content').html('');
            });
            attachEvents();
        }
    });
}

function saveDetails(redirect) {
    if ($('#frmSellerDetails').valid()) {
        var data = $('#frmSellerDetails').serializeArray();
        $.post('/api/v1/save/seller', data, function (response) {
            if (response.error) { showAlert('danger', 'One or more errors occured while trying to save your data. Please try again!'); }
            else { showAlert('success', 'Seller details were saved'); $('.container > h2 > span').text($('#name').val()); }
        });
    } else {
        $('#frmSellerDetails .has-error .form-control').focus();
        showAlert('danger', 'Please enter the mandatory information and try again!');
    }
}

function saveProduct() {
    if ($('#frmEditProduct').valid()) {
        var data = $('#frmEditProduct').serializeArray();
        data.push({ name: "seller_id", value: $('#GlobalSellerID').val() });
        $.post('/api/v1/save/product', data, function (response) {
            if ('error' in response) { showAlert('danger', 'One or more errors occured while trying to save your data. Please try again!', true); }
            else { $('#mdlProduct').modal('hide'); $('#menuSeller li.active a').trigger('click'); showAlert('success', 'Product details were saved'); }
        });
    } else {
        $('#frmSellerDetails .has-error .form-control').focus();
        showAlert('danger', 'Please enter the mandatory information and try again!', true);
    }
}

function removeGhost() {
    var data = { id: $('#id').val() };
    $.post('/api/v1/check/product', data, function (response) { });
}

function attachEvents() {
    $('a[data-toggle=modal][data-entity=product]').on('click', function () {
        $('#mdlProduct').data('remote', '/Seller/Home/Product/' + $(this).data('id')).one("loaded.bs.modal", function () {
            $.validator.unobtrusive.parse($('form', this));

            $('input[type=file]').on('change', function () {
                if ($(this).val() !== "") {
                    var reader = new FileReader();
                    var idx = $(this).data('idx');
                    var $that = $(this);
                    reader.onload = function () {
                        $.post("/api/v1/product/image", { id: $('#id').val(), image: reader.result, idx: idx }, function (response) {
                            $that.parent().css('background-image', 'url(' + reader.result + ')');
                        });
                    };
                    reader.readAsDataURL($(this)[0].files[0]);
                }
            });
        });
    });
}