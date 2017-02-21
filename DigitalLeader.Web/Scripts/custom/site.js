$(function () {
	var dtPickers = $('.datetimepicker');

	$.each(dtPickers, function (i, value) {
		var format = $(value).attr('date-format');
		$(value).datetimepicker({
			format: format
		});
	});

});