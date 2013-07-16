/// <reference path="../typings/jquery.validation/jquery.validation.d.ts" />
/// <reference path="../Utility.ts" />
/// <reference path="IView.ts" />

module Zoekjaar.Graduate {
	export class Edit implements IView {
		constructor() {

		}
		init() {
			var editor: any = $('.wysiwyg-editor');
			editor.wysiwyg();

			$('.collect-wysiwyg').on('click', $.proxy(this.onSubmit, this));

			var uploader: any = $('#fileupload');
			
			uploader.fileupload({
				dataType: 'json',
				done: function (e, data) {
					$('#company-logo').prop('src', data.result.file);
				}
			});

		}
		destroy() {
			$('.collect-wysiwyg').off('click');
			var uploader: any = $('#fileupload');
			uploader.fileupload('destroy');
		}
		onSubmit(e: JQueryEventObject) {
			var source = $('.wysiwyg-editor');
			$(source.data('wysiwyg-editor-target')).val(source.html());
		}
	}
}

(function () {
	var view = new Zoekjaar.Graduate.Edit();
	Zoekjaar.Utility.addToQueue(view);
})();