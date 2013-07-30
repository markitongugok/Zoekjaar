/// <reference path="../Utility.ts" />
/// <reference path="IView.ts" />

module Zoekjaar.Job {
	export class Index implements IView {
		constructor() {

		}
		init() {
			var tooltip: any = $('.tooltip-trigger');
			tooltip.tooltip();
		}
		destroy() {
		}
	}
}

(function () {
	var view = new Zoekjaar.Job.Index();
	Zoekjaar.Utility.addToQueue(view);
})();