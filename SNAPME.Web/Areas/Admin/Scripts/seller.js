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
        $.post('/Admin/Seller/Create', data, function (response) {
            if ("url" in response) { document.location.href = response.url; }
        });
    } else {
        $('#frmSellerDetails .has-error .form-control').focus();
    }
}

function saveProduct() {
    if ($('#frmEditProduct').valid()) {
        var data = $('#frmEditProduct').serializeArray();
        data.push({ name: "seller_id", value: $('#GlobalSellerID').val() });
        $.post('/Admin/Seller/Product', data, function (response) { });
    } else {
        $('#frmSellerDetails .has-error .form-control').focus();
    }
}

function attachEvents() {
    $('a[data-toggle=modal][data-entity=product]').on('click', function () {
        $('#mdlProduct').data('remote', '/Admin/Seller/Product/' + $(this).data('id')).one("loaded.bs.modal", function () {
            $.validator.unobtrusive.parse($('form', this));

            $('input[type=file]').on('change', function () {
                if ($(this).val() !== "") {
                    var reader = new FileReader();
                    var idx = $(this).data('idx');
                    var $that = $(this);
                    reader.onload = function () {
                        $.post("/Admin/Seller/ProductImage", { id: $('#id').val(), image: reader.result, idx: idx }, function (response) {
                            $that.parent().css('background-image', 'url(' + reader.result + ')');
                        });
                    };
                    reader.readAsDataURL($(this)[0].files[0]);
                }
            });
        });
    });
}