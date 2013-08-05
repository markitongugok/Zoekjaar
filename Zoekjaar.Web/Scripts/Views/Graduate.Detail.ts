/// <reference path="../typings/jquery.validation/jquery.validation.d.ts" />
/// <reference path="../Utility.ts" />
/// <reference path="IView.ts" />

module Zoekjaar.Graduate {
	export class Details implements IView {
		constructor() {

		}
		init() {
			var editor: any = $('.wysiwyg-editor');
			editor.wysiwyg();

			var uploader: any = $('#fileupload');

			uploader.fileupload({
				dataType: 'json',
				done: function (e, data) {
					$('#profile-picture').prop('src', data.result.file);
				}
			});

			$(document).on('click', '.btn-save', $.proxy(this.onSubmit, this));
			$(document).on('click', '.btn-add', $.proxy(this.onAddClick, this));
			$(document).on('click', '.btn-edit', $.proxy(this.onEditClick, this));
			$(document).on('click', '.btn-delete', $.proxy(this.onDeleteClick, this));
			$(document).on('click', '.btn-cancel', $.proxy(this.onCancel, this));
		}
		destroy() {
			$(document).off('click', '.btn-save');
			$(document).off('click', '.btn-add');
			$(document).off('click', '.btn-edit');
			$(document).off('click', '.btn-delete');
		}
		onCancel(e: JQueryEventObject) {
			var target = $(e.target);
			$(target.data('detail-selector')).show();
			target.closest('.template-container').hide();
			$('.media-body').show();

			this.showLinks(target.closest('.container'));

			e.preventDefault();
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
		viewLoaded(data: any) {
			var container = $('.view-container-target');
			container.html(data);
			container.removeClass('.view-container-target');

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
		onAddClick(e: JQueryEventObject) {
			var target = $(e.target);
			target = target.is('a') ? target : target.parent();
			target.closest('.view-container').addClass('view-container-target');

			var container = $(target.data('container-selector'));
			container.insertAfter(target.closest('.headline'));
			container.find('input[type="text"],select').val(null);
			container.toggle(true);

			this.hideLinks(target.closest('.container'));

			e.preventDefault();
		}
		onEditClick(e: JQueryEventObject) {
			var target = $(e.target);
			target = target.is('a') ? target : target.parent();
			target.closest('.view-container').addClass('view-container-target');

			var templateContainer = $(target.data('container-selector'));
			//var rowContainer = target.closest('div.row-container');
			var rowContainer = this.getRowContainer(target);
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

			this.hideLinks(target.closest('.container'));

			e.preventDefault();
		}
		getRowContainer(button: JQuery): JQuery {
			if (button.hasClass('btn-edit-in-headline')) {
				var rowContainer = button.closest('.headline').siblings('.blog-page').find('div.row-container');
				if (rowContainer.length > 0) {
					return $(rowContainer[0]);
				}
			}
			return button.closest('div.row-container');
		}
		hideLinks(parent: JQuery) {
			parent.find('a,i').hide();
		}
		showLinks(parent: JQuery) {
			parent.find('a,i').show();
		}
		onDeleteClick(e: JQueryEventObject) {
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
	var view = new Zoekjaar.Graduate.Details();
	Zoekjaar.Utility.addToQueue(view);
})();