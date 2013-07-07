/// <reference path="../typings/jquery.validation/jquery.validation.d.ts" />
/// <reference path="../typings/jqueryui/jqueryui.d.ts" />
/// <reference path="../Utility.ts" />
/// <reference path="IView.ts" />

module Zoekjaar.Graduate {
	export class EditProfile implements IView {
		constructor() {

		}
		init() {
			$(".add").toggle(false);
			$(".add").on("click", $.proxy(this.onAddClicked, this));
			$(".save").on("click", $.proxy(this.onSaveClicked, this));
			$(".save-and-add").on("click", $.proxy(this.onSaveAndAddClicked, this));
		}
		destroy() {
		}
		addItem(target: JQuery, container: JQuery) {
			var count = target.find('.container-fluid').length;
			var item = container.clone();
			item.removeAttr("id");

			container.find(".element").each(function (index, element) {
				var e = $(element);
				if (e.is("input")) {
					e.val("");
				}
				if (e.is("textarea")) {
					var target = item.find('#' + e.attr('id'));
					target.val(e.val());
					target.removeAttr("id");
					e.val("");
				}
				if (e.is("select")) {
					var selectedValue = e.find('option:selected').val();
					var target = item.find('#' + e.attr('id'));
					target.val(selectedValue);
					target.remove("id");
				}
			});

			item.find(".element").each(function (index, element) {
				var e = $(element);
				var name;
				if (e.data("name") != undefined) {
					name = e.data("name");
					e.removeData("name");
				}
				else {
					name = e.attr("dataname");
					e.removeAttr("dataname");
				}
				e.attr("name", Zoekjaar.Utility.formatString(name, count));
				e.removeAttr("id");
				e.removeClass("element");
				var attribute = "readonly";
				e.attr(attribute, attribute);
			});

			var row = $("<div class='row-container'></div>'");

			row.append(item);
			target.append(row);
		}
		onAddClicked(e: JQueryEventObject) {
			var source = $(e.target);
			source.toggle(false);
			var group = source.data("group");
			var groupSelector = '[data-group="' + group + '"]';
			var container = source.data("container");

			$(".save, .save-and-add").filter(groupSelector).toggle(true);
			$('#' + container).toggle(true);
			e.preventDefault();
		}
		onSaveClicked(e: JQueryEventObject) {
			var source = $(e.target);
			var group = source.data("group");
			var groupSelector = '[data-group="' + group + '"]';
			var container = source.data("container");
			var target = source.data("target");

			this.addItem($("#" + target), $("#" + container));
			source.toggle(false);
			$('.add').filter(groupSelector).toggle(true);
			$('.save,.save-and-add').filter(groupSelector).toggle(false);
			$('#' + container).toggle(false);

			e.preventDefault();
		}
		onSaveAndAddClicked(e: JQueryEventObject) {
			var source = $(e.target);
			var container = source.data("container");
			var target = source.data("target");
			this.addItem($("#" + target), $("#" + container));
			e.preventDefault();
		}
	}
}

(function () {
	var view = new Zoekjaar.Graduate.EditProfile();
	Zoekjaar.Utility.addToQueue(view);
})();