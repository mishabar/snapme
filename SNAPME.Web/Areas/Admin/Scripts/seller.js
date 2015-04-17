$(function () {
    $('#mnuSellers').addClass('active');
    $('#menuSeller > li:first-child() > a').trigger('click');

    $('#mdlProduct').on('hidden.bs.modal', function () { $(this).removeData('bs.modal').find('.modal-content').html(''); });
    $('#filter').on('click', filterList);
    $('#clear').on('click', function (event) { filterList(event, true); });
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

function saveDetails(redirect) {
    if ($('#frmSellerDetails').valid()) {
        var data = $('#frmSellerDetails').serializeArray();
        $.post('/api/v1/save/seller', data, function (response) {
            if (response.error) { alert(response.error); }
            else if (response.isNew) { document.location.href = '/Admin/Seller'; }
            else { alert('Seller details were saved'); $('.container > h2 > span').text($('#name').val()); }
        });
    } else {
        $('#frmSellerDetails .has-error .form-control').focus();
    }
}

function saveProduct() {
    if ($('#frmEditProduct').valid()) {
        var data = $('#frmEditProduct').serializeArray();
        data.push({ name: "seller_id", value: $('#GlobalSellerID').val() });
        $.post('/api/v1/save/product', data, function (response) {
            if ('error' in response) { }
            else { $('#mdlProduct').modal('hide'); $('#menuSeller li.active a').trigger('click'); }
        });
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

function createAccount(id) {
    $('#btnCreateAccount').addClass('disabled').find('i').removeClass('fa-lock').addClass('fa-spin fa-spinner disabled');
    $.post("/Admin/Seller/CreateAccount", { id: id, __RequestVerificationToken: $('input[name=__RequestVerificationToken]').val() }, function (r) {
        if (r.error) { alert(r.error); $('#btnCreateAccount').removeClass('disabled').find('i').addClass('fa-lock').removeClass('fa-spin fa-spinner'); }
        else {
            $('#btnCreateAccount').remove();
        }
    });
}

function archiveSeller(id) {
    $('#btnArchiveAccount').addClass('disabled').find('i').removeClass('fa-archive').addClass('fa-spin fa-spinner disabled');
    $.post("/Admin/Seller/Archive", { id: id, __RequestVerificationToken: $('input[name=__RequestVerificationToken]').val() }, function (r) {
        if (r.error) { alert(r.error); $('#btnArchiveAccount').removeClass('disabled').find('i').addClass('fa-archive').removeClass('fa-spin fa-spinner'); }
        else {
            $('#btnArchiveAccount').remove();
        }
    });
}

function filterList(event, clear) {
    clear = clear || false;
    var $query = $(event.target).closest('.input-group').find('input[type=text]');
    if (clear) { $query.val(''); }

    if ($query.val().trim() !== "") {
        var re = new RegExp($query.val().trim(), "gi");
        $('#sellersList tbody tr').each(function (idx, item) {
            $(item).toggleClass('hidden', !re.test($(item).text()));
        });
    } else {
        $('#sellersList tbody tr').removeClass('hidden');
    }
}