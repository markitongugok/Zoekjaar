var Zoekjaar;
(function (Zoekjaar) {
    var Utility = (function () {
        function Utility() {
        }
        Utility.addToQueue = function (view) {
            Zoekjaar.Utility.queue.push(view);
        };
        Utility.removeFromQueue = function (view) {
            Zoekjaar.Utility.queue.splice(Zoekjaar.Utility.queue.indexOf(view), 1);
        };
        Utility.executeInitQueue = function () {
            for (var i = 0; i < Zoekjaar.Utility.queue.length; i++) {
                var view = Zoekjaar.Utility.queue[i];
                view.init.call(view);
            }
        };
        Utility.load = function () {
            Zoekjaar.Utility.executeInitQueue();
            var validator = $('form').data('validator');
            validator.settings.errorClass = 'error';
            validator.settings.validClass = '';

            validator.settings.highlight = function (element, errorClass, validClass) {
                $(element).closest('.control-group').addClass('error');
            };
            validator.settings.unhighlight = function (element, errorClass, validClass) {
                $(element).closest('.control-group').removeClass('error');
            };
            $(".date-picker").datepicker();
        };
        Utility.unload = function () {
            for (var i = 0; i < Zoekjaar.Utility.queue.length; i++) {
                var view = Zoekjaar.Utility.queue[i];
                view.destroy.call(view);
            }
        };
        Utility.formatString = function () {
            var a = [];
            for (var _i = 0; _i < (arguments.length - 0); _i++) {
                a[_i] = arguments[_i + 0];
            }
            var s = a[0];
            for (var i = 0; i < a.length - 1; i++) {
                var reg = new RegExp("\\{" + i + "\\}", "gm");
                s = s.replace(reg, a[i + 1]);
            }

            return s;
        };
        Utility.queue = [];
        return Utility;
    })();
    Zoekjaar.Utility = Utility;
})(Zoekjaar || (Zoekjaar = {}));

(function () {
    $(function () {
        $(document).ready(Zoekjaar.Utility.load);
        $(window).unload(Zoekjaar.Utility.unload);
    });
})();
