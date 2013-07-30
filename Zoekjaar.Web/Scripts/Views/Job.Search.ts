/// <reference path="../Utility.ts" />
/// <reference path="IView.ts" />

module Zoekjaar.Graduate {
	export class SearchJob implements IView {
		constructor() {

		}
		init() {
			$(".apply").on("click", $.proxy(this.confirmApplication, this));
			$('#confirm-application').on('click', $.proxy(this.onApply, this));
		}
		destroy() {
			$(".apply").off("click");
			$('#confirm-application').off('click');
		}
		confirmApplication(e: JQueryEventObject) {
			var modal = $('#confirm');
			var button = modal.find('#confirm-application');
			var target = $(e.target);
			var source = target.is('button') ? target : target.parent();
			source.addClass('application-trigger');
			button.attr('data-target-url', source.data('target-url'));
			
			var body = modal.find('.modal-body');
			body.html('<p>' + Zoekjaar.Utility.formatString(body.data('format-string'), '<strong>' + source.data('job-title') + '</strong>') + '</p>');

			(<any>modal).modal('show');

			e.preventDefault();
		}
		onApply(e: JQueryEventObject) {
			var target = $(e.target);
			$.ajax({
				url: target.data("target-url"),
				type: "POST",
				success: $.proxy(this.onApplied, this),
				cache: false
			});
			var modal = $('#confirm');
			(<any>modal).modal('hide');
			e.preventDefault();
		}
		onApplied(data: any) {
			if (data) {
				var source = $('.application-trigger');
				source.parent().append('<label class="label label-info">Applied</label>');
				source.remove();
			}
		}
	}
}

(function () {
	var view = new Zoekjaar.Graduate.SearchJob();
	Zoekjaar.Utility.addToQueue(view);
})();