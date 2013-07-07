/// <reference path="../typings/jquery.validation/jquery.validation.d.ts" />
/// <reference path="../typings/jqueryui/jqueryui.d.ts" />
/// <reference path="../Utility.ts" />
/// <reference path="IView.ts" />

module Zoekjaar.Account {
	export class CreateGraduateProfile implements IView {
		constructor() {

		}
		init() {
			var editor: any = $('#editor');
			editor.wysiwyg();
		}
		destroy() {
		}
	}
}

(function () {
	var view = new Zoekjaar.Account.CreateGraduateProfile();
	Zoekjaar.Utility.addToQueue(view);
})();