/// <reference path="../typings/jquery.validation/jquery.validation.d.ts" />
/// <reference path="../Utility.ts" />
/// <reference path="IView.ts" />

module Zoekjaar.Graduate {
	export class EditProfile implements IView {
		constructor() {

		}
		init() {
			var editor: any = $('.wysiwyg-editor');
			editor.wysiwyg();

			$('.graduate-menu').on('click', $.proxy(this.onMenuClick, this));

			$(document).on('click', '.btn-save', $.proxy(this.onSubmit, this));
			$(document).on('click', '.btn-add', $.proxy(this.onAddDegreeClick, this));
			$(document).on('click', '.btn-edit', $.proxy(this.onEditDegreeClick, this));
			$(document).on('click', '.btn-delete', $.proxy(this.onDeleteDegreeClick, this));
		}
		destroy() {
			var uploader: any = $('#fileupload');
			uploader.fileupload('destroy');

			$(document).off('click', '.btn-save');
			$(document).off('click', '.btn-add');
			$(document).off('click', '.btn-edit');
			$(document).off('click', '.btn-delete');
		}
		onSubmit(e: JQueryEventObject) {
			var editor = $('.wysiwyg-editor');
			$(editor.data('wysiwyg-editor-target')).val(editor.html());
			var source = $(e.target);
			var form = source.closest('form');
			form.validate();
			if (form.valid()) {
				$.ajax({
					url: source.data('target-url'),
					data: form.serialize(),
					type: 'POST',
					success: $.proxy(this.viewLoaded, this)
				});
			}
			e.preventDefault();
		}
		onMenuClick(e: JQueryEventObject) {
			var source = $(e.target);
			$.ajax({
				url: source.data('target-url'),
				type: 'GET',
				success: $.proxy(this.viewLoaded, this)
			});
			e.preventDefault();
		}
		viewLoaded(data: any) {
			var container = $('.profile-container');
			container.html(data);

			var editor: any = $('.wysiwyg-editor');
			editor.wysiwyg();

			$(".date-picker").datepicker({
				format: 'mm/dd/yyyy'
			});

			var form = container.closest('form');
			form.removeData("validator");
			form.removeData("unobtrusiveValidation");
			var validator: any = $.validator;
			validator.unobtrusive.parse(form);
		}
		onAddDegreeClick(e: JQueryEventObject) {
			var container = $('.template-container');
			container.find('input[type="text"],select').val(null);
			container.toggle(true);
			e.preventDefault();
		}
		onEditDegreeClick(e: JQueryEventObject) {
			var source = $(e.target);
			var templateContainer = $('.template-container');
			var rowContainer = source.closest('div.row-container');

			rowContainer.find('[data-target-element]')
				.each(function (index: any, element: any) {
					var $element = $(element);
					if ($element.hasClass('select-value')) {
						templateContainer.find($element.data('target-element')).val($element.data('select-value'));
					}
					else {
						templateContainer.find($element.data('target-element')).val($element.val() || $element.text());
					}
				});

			rowContainer.append(templateContainer);
			rowContainer.find('.media-body').hide();
			templateContainer.show();
			e.preventDefault();
		}
		onDeleteDegreeClick(e: JQueryEventObject) {
			var source = $(e.target);
			if (source.is('i')) {
				source = source.closest('a');
			}
			$.ajax({
				url: source.data('target-url'),
				data: {
					id: source.data('target-id')
				},
				type: 'POST',
				success: $.proxy(this.viewLoaded, this)
			});
			e.preventDefault();
		}
	}
}

(function () {
	var view = new Zoekjaar.Graduate.EditProfile();
	Zoekjaar.Utility.addToQueue(view);
})();