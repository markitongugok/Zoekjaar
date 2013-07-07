/// <reference path="../typings/jqueryui/jqueryui.d.ts" />
/// <reference path="../Utility.ts" />
/// <reference path="IView.ts" />

module Zoekjaar.Company {
	export class Job implements IView {
		constructor() {

		}
		init() {
			$("input.post-job").on("click", $.proxy(this.onPostJob, this));
		}
		destroy() {
		}
		onPostJob(e: JQueryEventObject) {
			var source = $(e.target);

			$.ajax({
				url: source.data("target-url"),
				data: source.closest("form").serialize(),
				type: "POST",
				success: this.onJobPosted
			});
			e.preventDefault();
		}
		onJobPosted(data: any) {
			$(".posted-jobs").append(data);
		}
	}
}

(function () {
	var view = new Zoekjaar.Company.Job();
	Zoekjaar.Utility.addToQueue(view);
})();