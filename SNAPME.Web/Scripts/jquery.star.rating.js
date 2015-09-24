(function ($) {
    $.fn.addRating = function (options) {
        var obj = this;
        var settings = $.extend({
            max: 5,
            half: true,
            fieldName: 'rating',
            fieldId: 'rating',
            icon: 'star',
            color: '#ffde00'

        }, options);
        this.settings = settings;

        // create the stars
        for (var i = 1 ; i <= settings.max ; i++) {
            var star = $('<i/>').addClass('material-icons').html(this.settings.icon + '_border').data('rating', i).appendTo(this).click(
				function () {
				    obj.setRating($(this).data('rating'));
				}
			).hover(
				function (e) {
				    obj.showRating($(this).data('rating'), false);
				}, function () {
				    obj.showRating(0, false);
				}
			);
        }
        $(this).append('<input type="hidden" name="' + settings.fieldName + '" id="' + settings.fieldId + '" />');
    };

    $.fn.setRating = function (numRating) {
        var obj = this;
        $('#' + obj.settings.fieldId).val(numRating);
        obj.showRating(numRating, true);
    };

    $.fn.showRating = function (numRating, force) {
        var obj = this;
        if ($('#' + obj.settings.fieldId).val() == '' || force) {
            $(obj).find('i').each(function () {
                var icon = obj.settings.icon + '_border';
                $(this).css('color', 'inherit');
                if ($(this).data('rating') <= numRating) {
                    icon = obj.settings.icon;
                    $(this).css('color', obj.settings.color);
                }
                $(this).html(icon);
            })
        }
    }

}(jQuery));