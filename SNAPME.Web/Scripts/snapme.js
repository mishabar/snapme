function setActiveMenu(menu) { $("#" + menu).addClass("active"); }

function shareOnFacebook(url) {
    FB.ui({
        method: 'share',
        href: (url || document.location.href),
    }, function (response) {
        console.log(response);
    });
}

function sendFBInvitation(refid) {
    FB.ui({
        method: 'send',
        link: 'http://www.iisnap.com/welcome?ref=' + refid,
        redirect_uri: 'http://www.iisnap.com/welcome'
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
    $("[data-action=fb-share]").on("click", function () { shareOnFacebook($(this).data("url")); });
    $("[data-action=price-drop]").on("click", function () { dropPrice($(this).data("id")); });

    $('[data-action=like-product').on('click', function (event) { event.stopPropagation(); $.post('/api/v1/like/product', { id: $(this).data('id') }, function (r) { $('a[data-action=like-product][data-id=' + r.id + ']').toggleClass('active', r.liked); }); });
    $('[data-action=favor-product').on('click', function (event) { event.stopPropagation(); $.post('/api/v1/favorite/product', { id: $(this).data('id') }, function (r) { $('a[data-action=favor-product][data-id=' + r.id + ']').toggleClass('active', r.favored); }); });
    $('.btn-flip').on('click', function (event) { event.stopPropagation(); var _b = $(this).closest('.product-box').find('.back'); _b.fadeIn({ complete: function () { $('li.tab a.active', _b).click(); } }); });
    $('.btn-flip-back').on('click', function (event) { event.stopPropagation(); $(this).closest('.back').fadeOut(); });
    $('.product-box .face').on('click', function (event) { document.location.href = '/product/' + $(this).parent().data('id'); });
    $('.product-box .tabs.feed-tabs li a.full').on('click', function () {
        var $collection = $(this).closest('.back').find('.collection.all');
        $collection.html('').append($('<li/>').addClass('center-align').append($('<div class="preloader-wrapper small active"><div class="spinner-layer spinner-blue-only"><div class="circle-clipper left"><div class="circle"></div></div><div class="gap-patch"><div class="circle"></div></div><div class="circle-clipper right"><div class="circle"></div></div></div></div>')));
        $.post('/api/v1/product/feed', { id: $(this).closest('.product-box').data('id') }, function (response) {
            $collection.html('');
            $.each(response, function (i, item) { 
                var $item = $('<li class="collection-item avatar"><img src="" alt="" class="circle"><span class="title"></span><br/><span class="timestamp grey-text text-lighten-1"></span></li>');
                $item.find('img').attr('src', '/account/image/' + item.user_id).attr('alt', item.user_name);
                $item.find('span.title').text(item.action);
                $item.find('span.timestamp').text(new Date(item.ts).toLocaleDateString());
                $collection.append($item);
            });
            if (response.length == 0) {
                $collection.append($('<li class="collection-item center-align">').text('No activity yet.'));
            } else {
                var box = $collection.closest('.product-box');
                $collection.closest('.scroll-container').slimScroll({ height: box.height() - $('.social-header', box).height() - 40 + 'px' });
            }
        })
    });

    $('.product-box .tabs.feed-tabs li a.friends').on('click', function () {
        var $collection = $(this).closest('.back').find('.collection.friends');
        $collection.html('').append($('<li/>').addClass('center-align').append($('<div class="preloader-wrapper small active"><div class="spinner-layer spinner-blue-only"><div class="circle-clipper left"><div class="circle"></div></div><div class="gap-patch"><div class="circle"></div></div><div class="circle-clipper right"><div class="circle"></div></div></div></div>')));
        $.post('/api/v1/product/feed/friends', { id: $(this).closest('.product-box').data('id') }, function (response) {
            $collection.html('');
            $.each(response, function (i, item) {
                var $item = $('<li class="collection-item avatar"><img src="" alt="" class="circle"><span class="title"></span><br/><span class="timestamp grey-text text-lighten-1"></span></li>');
                $item.find('img').attr('src', '/account/image/' + item.user_id).attr('alt', item.user_name);
                $item.find('span.title').text(item.action);
                $item.find('span.timestamp').text(new Date(item.ts).toLocaleDateString());
                $collection.append($item);
            });
            if (response.length == 0) {
                $collection.append($('<li class="collection-item center-align">').text('No activity yet.'));
            }
            else {
                var box = $collection.closest('.product-box');
                $collection.closest('.scroll-container').slimScroll({ height: box.height() - $('.social-header', box).height() - 40 + 'px' });
            }
        });
    });
});

