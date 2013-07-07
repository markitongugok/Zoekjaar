var Zoekjaar;
(function (Zoekjaar) {
    (function (Home) {
        var Index = (function () {
            function Index() {
            }
            Index.prototype.init = function () {
            };
            Index.prototype.destroy = function () {
            };
            return Index;
        })();
        Home.Index = Index;
    })(Zoekjaar.Home || (Zoekjaar.Home = {}));
    var Home = Zoekjaar.Home;
})(Zoekjaar || (Zoekjaar = {}));

(function () {
    var view = new Zoekjaar.Home.Index();
    Zoekjaar.Utility.addToQueue(view);
})();
