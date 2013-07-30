/// <reference path="../Utility.ts" />
/// <reference path="IView.ts" />

module Zoekjaar.Candidate {
	export class Index implements IView {
		constructor() {

		}
		init() {
			$('#save').on('click', $.proxy(this.onSubmit, this));
		}
		destroy() {
			$('#save').off('click');
		}
		onSubmit(e: JQueryEventObject) {
			var target = $(e.target);
			$.ajax({
				url: target.data('target-url'),
				data: {
					GraduateId: target.data('candidate-id'),
					JobId: target.data('job-id'),
					StatusId: $('#' + target.data('control-selector') + ' option:selected').val()
				},
				type: 'POST',
				success: $.proxy(this.onSubmitted, this)
			});
			e.preventDefault();
		}
		onSubmitted(data: any) {

		}
	}
}

(function () {
	var view = new Zoekjaar.Candidate.Index();
	Zoekjaar.Utility.addToQueue(view);
})();