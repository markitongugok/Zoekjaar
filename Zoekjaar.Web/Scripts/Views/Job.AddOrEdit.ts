/// <reference path="../typings/jquery.validation/jquery.validation.d.ts" />
/// <reference path="../Utility.ts" />
/// <reference path="IView.ts" />

module Zoekjaar.Job {
	export class AddOrEdit implements IView {
		constructor() {

		}
		init() {
			var editor: any = $('.wysiwyg-editor');
			editor.wysiwyg();

			$('#save').on('click', $.proxy(this.onChange, this));
		}
		destroy() {
			$('#save').off('click');
		}
		onChange(e: JQueryEventObject) {
			var source = $('.wysiwyg-editor');
			$(source.data('wysiwyg-editor-target')).val(source.html());
			source.closest('form').submit();
			e.preventDefault();
		}
	}
}

(function () {
	var view = new Zoekjaar.Job.AddOrEdit();
	Zoekjaar.Utility.addToQueue(view);
})();