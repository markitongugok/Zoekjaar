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
			$(document).on('click', '.add-degree', $.proxy(this.onAddDegreeClick, this));
			$(document).on('click', '.edit-degree', $.proxy(this.onEditDegreeClick, this));
			$(document).on('click', '.delete-degree', $.proxy(this.onDeleteDegreeClick, this));
		}
		destroy() {
			$('.graduate-menu').off('click');

			$(document).off('click', '.btn-save');
			$(document).off('click', '.add-degree');
			$(document).off('click', '.edit-degree');
			$(document).off('click', '.delete-degree');
		}
		onSubmit(e: JQueryEventObject) {
			var editor = $('.wysiwyg-editor');
			$(editor.data('wysiwyg-editor-target')).val(editor.html());
			var source = $(e.target);
			$.ajax({
				url: source.data('target-url'),
				data: source.closest('form').serialize(),
				type: 'POST',
				success: $.proxy(this.viewLoaded, this)
			});
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
			$('.profile-container').html(data);
		}
		onAddDegreeClick(e: JQueryEventObject) {
			$('.template-container').toggle(true);
			e.preventDefault();
		}
		onEditDegreeClick(e: JQueryEventObject) {
			var source = $(e.target);
			var templateContainer = $('.template-container');
			var rowContainer = source.closest('div.row-container');

			rowContainer.find('[data-target-element]')
				.each(function (index: any, element: any) {
					var $element = $(element);
					$('.template-container').find($element.data('target-element')).val($element.val() || $element.text());
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