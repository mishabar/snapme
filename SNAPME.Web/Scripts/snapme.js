function setActiveMenu(menu) { $("#" + menu).addClass("active"); }

$(function () {
    $("footer .panel-heading").on("click", function () { $(this).parent().toggleClass("in"); });
});