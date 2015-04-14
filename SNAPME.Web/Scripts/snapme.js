function setActiveMenu(menu) { $("#" + menu).addClass("active"); }

function shareOnFacebook(url) {
    FB.ui({
        method: 'share',
        href: (url || document.location.href),
    }, function (response) {
        console.log(response);
    });
}

function showWelcomeNewUser() {
    var $modal = $("<div/>").addClass("modal fade").attr("tabindex", "-1").attr("role", "dialog").data("remote", "/dialog/newuser").append($("<div/>").addClass("modal-dialog").append($("<div/>").addClass("modal-content")));
    $("body").append($modal);
    $modal.modal("show");
}

function showLoginDialog(error, message) {
    var $modal = $("<div/>").addClass("modal fade").attr("tabindex", "-1").attr("role", "dialog").data("remote", "/dialog/login?error=" + encodeURIComponent(error || "") + "&message=" + encodeURIComponent(message || "")).append($("<div/>").addClass("modal-dialog").append($("<div/>").addClass("modal-content")));
    $("body").append($modal);
    $modal.modal("show");
}

function dropPrice(id) {
    if (window.dns.au) {
        var $modal = $("<div/>").addClass("modal fade").attr("tabindex", "-1").attr("role", "dialog").data("remote", "/sale/drop/" + encodeURIComponent(id)).append($("<div/>").addClass("modal-dialog").append($("<div/>").addClass("modal-content")));
        $("body").append($modal);
        $modal.modal("show");
    } else {
        showLoginDialog("", "You must login in order to participate in a sale");
    }
}

$(function () {
    //$("footer .panel-heading").on("click", function () { $(this).parent().toggleClass("in"); });
    $("[data-action=fb-share]").on("click", function () { shareOnFacebook($(this).data("url")); });
    $("[data-action=price-drop]").on("click", function () { dropPrice($(this).data("id")); });

    $('[data-action=like-product').on('click', function () { $.post('/api/v1/like/product', { id: $(this).data('id') }, function (r) { $('a[data-action=like-product][data-id=' + r.id + ']').toggleClass('active', r.liked); }); });
    $('[data-action=favor-product').on('click', function () { $.post('/api/v1/favorite/product', { id: $(this).data('id') }, function (r) { $('a[data-action=favor-product][data-id=' + r.id + ']').toggleClass('active', r.favored); }); });

    $("img.avatar[data-src]").each(function (i, img) {
        $(img).attr("src", $(img).data("src") + "&width=" + $(img).width());
    });
    $("body").css("margin-bottom", $("footer").height() + 40 + "px");
});
