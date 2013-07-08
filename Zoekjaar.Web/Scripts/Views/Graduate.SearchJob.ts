/// <reference path="../typings/jqueryui/jqueryui.d.ts" />
/// <reference path="../Utility.ts" />
/// <reference path="IView.ts" />

module Zoekjaar.Graduate {
	export class SearchJob implements IView {
		constructor() {

		}
		init() {
			$(".apply").on("click", $.proxy(this.onApply, this));
		}
		destroy() {
			$(".apply").off("click");
		}
		onApply(e: JQueryEventObject) {
			var target = $(e.target);
			var source = target.is('button') ? target : target.parent();

			$.ajax({
				url: source.data("target-url"),
				type: "POST",
				success: $.proxy(this.onApplied, source),
				cache: false
			});
			e.preventDefault();
		}
		onApplied(data: any) {
			if (data) {
				var source = $(this);
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