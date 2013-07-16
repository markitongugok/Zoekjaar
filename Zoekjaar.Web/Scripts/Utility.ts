/// <reference path="typings/bootstrap.datepicker/bootstrap.datepicker.d.ts" />
/// <reference path="typings/jquery.validation/jquery.validation.d.ts" />
/// <reference path="typings/jquery/jquery.d.ts" />
/// <reference path="Views/IView.ts" />

var App = App || {};
module Zoekjaar {
	export class Utility {
		static queue: any[] = [];
		static addToQueue(view: IView): void {
			Zoekjaar.Utility.queue.push(view);
		}
		static removeFromQueue(view: IView): void {
			Zoekjaar.Utility.queue.splice(Zoekjaar.Utility.queue.indexOf(view), 1);
		}
		static executeInitQueue(): void {
			for (var i = 0; i < Zoekjaar.Utility.queue.length; i++) {
				var view = Zoekjaar.Utility.queue[i];
				view.init.call(view);
			}
		}
		static load(): void {
			Zoekjaar.Utility.executeInitQueue();
			
			var validator: any = $('form').data('validator');
			validator.settings.errorClass = 'error';
			validator.settings.validClass = '';

			validator.settings.highlight = function (element, errorClass, validClass) {
				$(element).closest('.controls').addClass('error');
			};
			validator.settings.unhighlight = function (element, errorClass, validClass) {
				$(element).closest('.controls').removeClass('error');
			}
			$(".date-picker").datepicker({
				format: 'mm/dd/yyyy'
			});
			App.init();
			App.initSliders();
		}
		static unload(): void {
			for (var i = 0; i < Zoekjaar.Utility.queue.length; i++) {
				var view = Zoekjaar.Utility.queue[i];
				view.destroy.call(view);
			}
		}
		static formatString(...a: any[]): string {
			var s = a[0];
			for (var i = 0; i < a.length - 1; i++) {
				var reg = new RegExp("\\{" + i + "\\}", "gm");
				s = s.replace(reg, a[i + 1]);
			}

			return s;
		}
	}
}

(function () {
	$(function () {
		$(document).ready(Zoekjaar.Utility.load);
		$(window).unload(Zoekjaar.Utility.unload);
	});
})();