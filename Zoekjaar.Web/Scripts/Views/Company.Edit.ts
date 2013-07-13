/// <reference path="../typings/jquery.validation/jquery.validation.d.ts" />
/// <reference path="../Utility.ts" />
/// <reference path="IView.ts" />

module Zoekjaar.Graduate {
	export class Edit implements IView {
		constructor() {

		}
		init() {
			$('.collect-wysiwyg').on('click', $.proxy(this.onSubmit, this));
		}
		destroy() {
			$('.collect-wysiwyg').off('click');
		}
		onSubmit(e: JQueryEventObject) {
			var source = $('.wysiwyg-editor');
			$(source.data('wysiwyg-editor-target')).val(source.html());
			//source.closest('form').submit();
			//e.preventDefault();
		}
	}
}

(function () {
	var view = new Zoekjaar.Graduate.Edit();
	Zoekjaar.Utility.addToQueue(view);
})();